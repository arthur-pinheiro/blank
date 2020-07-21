using ApplicationCore.Entities;
using ApplicationCore.Interfaces.Db;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class EfRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        protected readonly ClientContext _dbContext;

        public EfRepository(ClientContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        //public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = await ApplySpecification(spec);
        //    return await specificationResult.ToListAsync();
        //}

        //public async Task<int> CountAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = await ApplySpecification(spec);
        //    return await specificationResult.CountAsync();
        //}

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        //public async Task<T> FirstAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = await ApplySpecification(spec);
        //    return await specificationResult.FirstAsync();
        //}

        //public async Task<T> FirstOrDefaultAsync(ISpecification<T> spec)
        //{
        //    var specificationResult = await ApplySpecification(spec);
        //    return await specificationResult.FirstOrDefaultAsync();
        //}

        //private async Task<IQueryable<T>> ApplySpecification(ISpecification<T> spec)
        //{
        //    return await EfSpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        //}
    }
}
