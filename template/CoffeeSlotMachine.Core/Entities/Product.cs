using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Entities
{
    /// <summary>
    /// Produkt mit Namen und Preis
    /// </summary>
    public class Product : EntityObject
    {
        /// <summary>
        /// Produktbezeichnung
        /// </summary>
        public string Name { get; set; }
        public int PriceInCents { get; set; }

        /// <summary>
        /// Bild wird als Byte[] direkt in Datenbank gespeichert
        /// </summary>
        public byte[] Image { get; set; }
        public ICollection<Order> Orders { get; set; }

        public Product()
        {
            Orders = new List<Order>();
        }
    }
}
