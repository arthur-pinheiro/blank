using ApplicationCore.Domain.Interfaces.Cache;
using ApplicationCore.Domain.Interfaces.Db;
using ApplicationCore.Domain.Interfaces.Serializers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationCore.Application.Services.Tests.Queries
{
    [Serializable]
    public class T_ServicoVeiculo
    {
        public long Id { get; set; }
        public long FKEmpresa { get; set; }
        public string CodigoCatalogo { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CodigoCatalogoVersaoERP { get; set; }
        public string CodigoCatalogoVersaoFipe { get; set; }
        public long? FKVeiculoVersao { get; set; }
        public string VeiculoVersao { get; set; }
        public long? FKRevisao { get; set; }
        public string CodigoCatalogoRevisao { get; set; }
        public long? FKServicoAgregado { get; set; }
        public string CodigoCatalogoServicoAgregado { get; set; }
        public string NomeServicoAgregado { get; set; }
        public string DescricaoServicoAgregado { get; set; }
        public int? TempoExecucaoServicoAgregado { get; set; }
        public long? FKPacote { get; set; }
        public string CodigoCatalogoPacote { get; set; }
        public string Pacote { get; set; }
        public int? OrdemPacote { get; set; }
        public string CorPacote { get; set; }
        public string CordaFontePacote { get; set; }
        public bool ServicoAgregado { get; set; }
        public decimal ValorProdutos { get; set; }
        public long? QtdProdutos { get; set; }
        public decimal ValorMOs { get; set; }
        public long? QtdMOs { get; set; }
        public decimal ValorProdutosAgregado { get; set; }
        public decimal ValorMOAgregado { get; set; }
        public bool? Obrigatorio { get; set; }
        public string CodigoCatalogoMaoDeObraAgregado { get; set; }
        public decimal ValorMinutoMOAgregado { get; set; }
        public string MaoDeObraAgregado { get; set; }
        public string DescricaoMaoDeObraAgregado { get; set; }
        public int? OrdemServico { get; set; }
    }

    [Serializable]
    public class FunctionVm
    {
        public IEnumerable<T_ServicoVeiculo> Data { get; set; }
    }

    public class GetDataFromFunctionQuery : IRequest<FunctionVm>
    {
        public long pempresa { get; set; }
        public string previsao { get; set; }
        public string pveiculoversao { get; set; }
    }

    public class GetDataFromFunctionQueryHandler : IRequestHandler<GetDataFromFunctionQuery, FunctionVm>
    {
        private IApplicationDbContext _applicationDbContext;
        private ICacheHandler _cacheHandler;
        private IJsonSerializer<FunctionVm> _jsonSerializer;

        public GetDataFromFunctionQueryHandler(IApplicationDbContext applicationDbContext, ICacheHandler cacheHandler, IJsonSerializer<FunctionVm> jsonSerializer)
        {
            _applicationDbContext = applicationDbContext;
            _cacheHandler = cacheHandler;
            _jsonSerializer = jsonSerializer;
        }

        public async Task<FunctionVm> Handle(GetDataFromFunctionQuery request, CancellationToken cancellationToken)
        {
            FunctionVm functionVm = new FunctionVm();

            string key = $@"getservicosveiculosobrigatorios-{request.pempresa}-${request.previsao}-${request.pveiculoversao}";
            var cacheData = await _cacheHandler
                .GetCacheValueAsync(key);

            // TODO: should implement a way to detect object value changes
            if (cacheData == null || cacheData.Length <= 0)
            {
                IEnumerable<T_ServicoVeiculo> functionResult = await _applicationDbContext
                    .GetData(request.pempresa, request.previsao, request.pveiculoversao);

                functionVm.Data = functionResult;

                byte[] serializedFunctionVm = _jsonSerializer
                    .ObjectToByteArray(functionVm);

                _cacheHandler
                    .SetCacheValueAsync(key, serializedFunctionVm);

                return functionVm;
            }

            byte[] byteArrayCacheData = await _cacheHandler
                .GetCacheValueAsync(key);

            string cacheDataJsonString = _jsonSerializer
                .GetJsonStringFromByteArray(byteArrayCacheData);

            return _jsonSerializer
                .GetDeserializedObject(cacheDataJsonString);
        }
    }
}