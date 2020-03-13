using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Contracts
{
    public interface IProductRepository
    {
        public Product[] GetAllProducts();
        public Product GetProductById(int id);
    }
}