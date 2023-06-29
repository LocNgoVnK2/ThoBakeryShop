using Infrastructure.Entities;
using Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Service
{
    public interface ICategoryService
    {
        Task<IQueryable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task InsertCategory(Category category);
        Task UpdateCategory(Category category);
        Task DeleteCategory(Category category);
    }
    public class CategoryService : ICategoryService
    {
        ICategoryRepository categoryRepo;
        public CategoryService(ICategoryRepository categoryRepo) { 
            this.categoryRepo = categoryRepo;
        }
        public async Task DeleteCategory(Category category)
        {
           await categoryRepo.DeleteAsync(category);
        }

        public async Task<IQueryable<Category>> GetCategories()
        {
            return await Task.FromResult(categoryRepo.GetAll());
        }

        public async Task<Category> GetCategory(int id)
        {
            return await categoryRepo.GetByIdAsync(id);
        }

        public async Task InsertCategory(Category category)
        {
            await categoryRepo.InsertAsync(category);
        }

        public async Task UpdateCategory(Category category)
        {
           await categoryRepo.UpdateAsync(category);
        }
    }
}
