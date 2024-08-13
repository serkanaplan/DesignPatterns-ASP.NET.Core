﻿using CQRSDesignPattern.CQRSPattern.Commands;
using CQRSDesignPattern.DAL;

namespace CQRSDesignPattern.CQRSPattern.Handlers;

public class CreateProductCommandHandler(Context context)
{
    private readonly Context _context = context;

    public void Handle(CreateProductCommand command)
    {
        _context.Products.Add(new Product
        {
            Description = command.Description,
            Name = command.Name,
            Price = command.Price,
            Status = true,
            Stock = command.Stock
        });
        _context.SaveChanges();
    }
}
