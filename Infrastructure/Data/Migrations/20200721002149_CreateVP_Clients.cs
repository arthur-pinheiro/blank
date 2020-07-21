using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Data.Migrations
{
    public partial class CreateVP_Clients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // TODO: Não sei pq ele não estava encontrando a tabela: clients e não clientes.
            string createViewCommand = @"
                CREATE VIEW VP_Clients AS 
                SELECT  a.nome as name,
                        a.cpfcnpj as cpf,
                        'teste' as lastcontact
                FROM public.clientes a;
            ";
            migrationBuilder.Sql(createViewCommand);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP VIEW IF EXISTS VP_Clients;");
        }
    }
}
