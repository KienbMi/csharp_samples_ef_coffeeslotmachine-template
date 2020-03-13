using CoffeeSlotMachine.Core.Contracts;
using CoffeeSlotMachine.Core.Entities;
using CoffeeSlotMachine.Persistence;
using System;
using System.Linq;
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
            return _productRepository.GetAllProducts()
                .OrderBy(p => p.Name);
        }

        /// <summary>
        /// Eine Bestellung wird für das Produkt angelegt.
        /// </summary>
        /// <param name="product"></param>
        public Order OrderCoffee(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            return new Order()
            {
                Time = DateTime.Now,
                ProductId = product.Id,
                Product = product
            };
        }

        /// <summary>
        /// Münze einwerfen. 
        /// Wurde zumindest der Produktpreis eingeworfen, Münzen in Depot übernehmen
        /// und für Order Retourgeld festlegen. Bestellug abschließen.
        /// </summary>
        /// <returns>true, wenn die Bestellung abgeschlossen ist</returns>
        public bool InsertCoin(Order order, int coinValue)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }

            bool finished = order.InsertCoin(coinValue);
            if (finished)
            {
                List<Coin> coins = new List<Coin>();

                string[] data = order.ThrownInCoinValues?.Split(';');

                if (data != null && data.Length > 0)
                {
                    for (int i = 0; i < data.Length; i++)
                    {
                        if (int.TryParse(data[i], out int value))
                        {
                            Coin coin = coins.Find(c => c.CoinValue == value);
                            if (coin == null)
                            {
                                coins.Add(new Coin
                                    {
                                        Amount = 1,
                                        CoinValue = value
                                    }
                                );
                            }
                            else
                            {
                                coin.Amount++;
                            }
                        }
                    }
                }
                _coinRepository.AddCoins(coins.ToArray());

                int returnCents = order.ThrownInCents - order.Product.PriceInCents;
                Coin[] coinsInDepot = GetCoinDepot().ToArray();

                List<Coin> returnCoins = new List<Coin>();
                foreach (Coin coin in coinsInDepot)
                {
                    if (coin.CoinValue <= returnCents)
                    {
                        int amount = returnCents / coin.CoinValue;
                        amount = Math.Min(amount, coin.Amount);
                        returnCents -= amount * coin.CoinValue;

                        returnCoins.Add(new Coin{
                            Amount = amount,
                            CoinValue = coin.CoinValue
                        });
                    }
                }
                _coinRepository.RemoveCoins(returnCoins.ToArray());

                order.FinishPayment(returnCoins);
                _orderRepository.AddOrder(order);
            }

            return finished;
        }

        /// <summary>
        /// Gibt den aktuellen Inhalt der Kasse, sortiert nach Münzwert absteigend zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Coin> GetCoinDepot()
        {
            return _coinRepository.GetAllCoins()
                .OrderByDescending(c => c.CoinValue);
        }


        /// <summary>
        /// Gibt den Inhalt des Münzdepots als String zurück
        /// </summary>
        /// <returns></returns>
        public string GetCoinDepotString()
        {
            Coin[] coins = GetCoinDepot().ToArray();

            //"3*200 + 4*100 + 3*50 + 2*20 + 2*10 + 2*5
            string result = $"{coins[0].Amount}*{coins[0].CoinValue}";
            for (int i = 1; i < coins.Length; i++)
            {
                result += $" + {coins[i].Amount}*{coins[i].CoinValue}";
            }
            return result;
        }

        /// <summary>
        /// Liefert alle Orders inkl. der Produkte zurück
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAllOrdersWithProduct()
        {
            return _orderRepository.GetAllWithProduct();
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
