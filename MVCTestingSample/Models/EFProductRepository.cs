﻿using Microsoft.EntityFrameworkCore;
using MVCTestingSample.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCTestingSample.Models
{
    public class EFProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public EFProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a product to the data store
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public Task AddProductAsync(Product p)
        {
            _context.Add(p);
            // If you dont await this it will return as Task
            // In this case we want it to return as Task
            return _context.SaveChangesAsync();
        }

        public Task DeleteProductAsync(Product p)
        {
            _context.Remove(p);
            return _context.SaveChangesAsync();
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            return await _context.Products.OrderBy(p => p.Name).ToListAsync();
        }

        /// <summary>
        /// Returns the product by ID, or null if no products matches
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products.Where(p => p.ProductId == id).SingleOrDefaultAsync();
        }

        public Task UpdateProductAsync(Product p)
        {
            _context.Entry(p).State = EntityState.Modified;
            return _context.SaveChangesAsync();
        }
    }
}
