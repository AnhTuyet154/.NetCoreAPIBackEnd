﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIServices.Data;
using WebAPIServices.Dto.Category;
using WebAPIServices.Mappers;

namespace WebAPIServices.Services.CategoryServices
{
    public class CategoryService : ICategoryrService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<CategoryDto> AddCategoryAsync(CreateCategoryDto category)
        {
            var newCategory = category.ToCategoryFromCreateDTO();
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();

            return newCategory.ToCategoryDto();
        }

        public async Task<List<CategoryDto>> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return await GetAllCategoriesAsync();
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            return categories.Select(c => c.ToCategoryDto()).ToList();
        }

        public async Task<CategoryDto> GetSingleCategoryAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            return category?.ToCategoryDto();
        }

        public async Task<CategoryDto> UpdateCategoryAsync(int id, UpdateCategoryDto category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory != null)
            {
                existingCategory = category.ToCategoryFromUpdateDTO(existingCategory);
                await _context.SaveChangesAsync();
                return existingCategory.ToCategoryDto();
            }
            return null;
        }
    }
}
