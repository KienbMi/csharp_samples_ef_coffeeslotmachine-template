using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeSlotMachine.Core.Entities;

namespace CoffeeSlotMachine.Persistence.Migrations
{
    public partial class SeedDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                context.Coins.Add(new Coin { CoinValue = 5, Amount = 3 });
                context.Coins.Add(new Coin { CoinValue = 10, Amount = 3 });
                context.Coins.Add(new Coin { CoinValue = 20, Amount = 3 });
                context.Coins.Add(new Coin { CoinValue = 50, Amount = 3 });
                context.Coins.Add(new Coin { CoinValue = 100, Amount = 3 });
                context.Coins.Add(new Coin { CoinValue = 200, Amount = 3 });
                context.Products.Add(new Product { Name = "Cappuccino", PriceInCents = 65 });
                context.Products.Add(new Product { Name = "Doppio", PriceInCents = 80 });
                context.Products.Add(new Product { Name = "Espresso", PriceInCents = 50 });
                context.Products.Add(new Product { Name = "Kaffee Crema", PriceInCents = 70 });
                context.Products.Add(new Product { Name = "Latte", PriceInCents = 50 });
                context.Products.Add(new Product { Name = "Lungo", PriceInCents = 65 });
                context.Products.Add(new Product { Name = "Machiato", PriceInCents = 75 });
                context.Products.Add(new Product { Name = "Mocca", PriceInCents = 55 });
                context.Products.Add(new Product { Name = "Ristretto", PriceInCents = 60 });
                context.SaveChanges();
            }
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var coins = context.Coins.ToList();
                context.RemoveRange(coins);
                context.RemoveRange(context.Products.ToList());
                context.SaveChanges();
            }
        }
    }
}
