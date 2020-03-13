using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Contracts
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllWithProduct();
        public void AddOrder(Order order);
    }
}