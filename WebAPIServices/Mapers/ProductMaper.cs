﻿using WebAPIServices.Dto.Product;
using WebAPIServices.Models;

namespace WebAPIServices.Mapers
{
    public static class ProductMaper
    {
        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Color = product.Color,
                Image = product.Image,
                Description = product.Description

            };
        }

        public static Product ToProductFromCreate(this CreateProductDto productDto, int categoryId)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = categoryId,
                Color = productDto.Color,
                Image = productDto.Image,
                Description = productDto.Description
            };
        }

        public static Product ToProductFromUpdate(this UpdateProductDto productDto, int categoryId)
        {
            return new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                CategoryId = categoryId,            
                Color = productDto.Color,
                Image = productDto.Image,
                Description = productDto.Description
            };
        }

    }
}