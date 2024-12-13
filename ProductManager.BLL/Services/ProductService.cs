
using Microsoft.EntityFrameworkCore;
using ProductManager.BLL.Interfaces;
using ProductManager.DAL;
using ProductManager.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace ProductManager.BLL.Services;

public class ProductService : IProductService
{
    private readonly AppDbContext _context;

    public ProductService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync(string? searchQuery, int pageNumber, int pageSize)
    {
        var query = _context.Products.AsQueryable();

        // Apply search filter
        if (!string.IsNullOrEmpty(searchQuery))
        {
            query = query.Where(p => p.Name.Contains(searchQuery) || p.Description.Contains(searchQuery));
        }

        // Apply pagination
        query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

        return await query.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }

    //public async Task AddProductAsync(Product product)
    //{
    //    _context.Products.Add(product);
    //    await _context.SaveChangesAsync();
    //}

    public async Task AddProductAsync(Product product)
    {
        // Validate the product
        var validator = new ProductValidator();
        var validationResult = await validator.ValidateAsync(product);

        if (!validationResult.IsValid)
        {
            throw new ValidationException("One of More Product field is not valid! Check it and try again");
        }

        // Add the product
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
    }



    public async Task UpdateProductAsync(Product product)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }
    }
}
