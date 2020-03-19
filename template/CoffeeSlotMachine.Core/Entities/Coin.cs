namespace CoffeeSlotMachine.Core.Entities
{
    /// <summary>
    /// Münzwerte mit der Anzahl des Münzwerts im Münzdepot
    /// </summary>
    public class Coin : EntityObject
    {
        /// <summary>
        /// Wert der Münze 5 Cent - 200 Cent
        /// </summary>
        public int CoinValue { get; set; }

        /// <summary>
        /// Anzahl dieses Münzwerts im Depot
        /// </summary>
        public int Amount { get; set; }

    }
}
