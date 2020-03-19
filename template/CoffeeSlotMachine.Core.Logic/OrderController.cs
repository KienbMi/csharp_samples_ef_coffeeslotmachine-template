using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using CoffeeSlotMachine.Persistence;
using System;
using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Logic
{
    /// <summary>
    /// Verwaltet einen Bestellablauf. 
    /// </summary>
    public class OrderController : IDisposable
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICoinRepository _coinRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;

        public OrderController()
        {
            _dbContext = new ApplicationDbContext();

            _coinRepository = new CoinRepository(_dbContext);
            _orderRepository = new OrderRepository(_dbContext);
            _productRepository = new ProductRepository(_dbContext);
        }


        /// <summary>
        /// Gibt alle Produkte sortiert nach Namen zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Product> GetProducts()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Eine Bestellung wird für das Produkt angelegt.
        /// </summary>
        /// <param name="product"></param>
        public Order OrderCoffee(Product product)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Münze einwerfen. 
        /// Wurde zumindest der Produktpreis eingeworfen, Münzen in Depot übernehmen
        /// und für Order Retourgeld festlegen. Bestellug abschließen.
        /// </summary>
        /// <returns>true, wenn die Bestellung abgeschlossen ist</returns>
        public bool InsertCoin(Order order, int coinValue)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gibt den aktuellen Inhalt der Kasse, sortiert nach Münzwert absteigend zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Coin> GetCoinDepot()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Gibt den Inhalt des Münzdepots als String zurück
        /// </summary>
        /// <returns></returns>
        public string GetCoinDepotString()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Liefert alle Orders inkl. der Produkte zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrdersWithProduct()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// IDisposable:
        ///
        /// - Zusammenräumen (zB. des ApplicationDbContext).
        /// </summary>
        public void Dispose()
        {
            _dbContext?.Dispose();
        }
    }
}
