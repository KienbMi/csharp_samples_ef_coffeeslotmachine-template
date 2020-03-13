using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeSlotMachine.Core.Entities
{
    /// <summary>
    /// Bestellung verwaltet das bestellte Produkt, die eingeworfenen Münzen und
    /// die Münzen die zurückgegeben werden.
    /// </summary>
    public class Order : EntityObject
    {
        /// <summary>
        /// Datum und Uhrzeit der Bestellung
        /// </summary>
        public DateTime Time { get; set; }

        /// <summary>
        /// Werte der eingeworfenen Münzen als Text. Die einzelnen 
        /// Münzwerte sind durch ; getrennt (z.B. "10;20;10;50")
        /// </summary>
        public String ThrownInCoinValues { get; set; }

        /// <summary>
        /// Zurückgegebene Münzwerte mit ; getrennt
        /// </summary>
        public String ReturnCoinValues { get; set; }

        /// <summary>
        /// Summe der eingeworfenen Cents.
        /// </summary>
        public int ThrownInCents => ConvertNumbersTextToInt(ThrownInCoinValues);

        /// <summary>
        /// Summe der Cents die zurückgegeben werden
        /// </summary>
        public int ReturnCents => ConvertNumbersTextToInt(ReturnCoinValues);


        public int ProductId { get; set; }

        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }

        /// <summary>
        /// Kann der Automat mangels Kleingeld nicht
        /// mehr herausgeben, wird der Rest als Spende verbucht
        /// </summary>
        public int DonationCents => ThrownInCents - (Product.PriceInCents + ReturnCents);

        /// <summary>
        /// Münze wird eingenommen.
        /// </summary>
        /// <param name="coinValue"></param>
        /// <returns>isFinished ist true, wenn der Produktpreis zumindest erreicht wurde</returns>
        public bool InsertCoin(int coinValue)
        {
            ThrownInCoinValues = AddIntToNumbersText(ThrownInCoinValues, coinValue);
            return ThrownInCents >= Product.PriceInCents ? true : false;
        }

        /// <summary>
        /// Übernahme des Einwurfs in das Münzdepot.
        /// Rückgabe des Retourgeldes aus der Kasse. Staffelung des Retourgeldes
        /// hängt vom Inhalt der Kasse ab.
        /// </summary>
        /// <param name="coins">Aktueller Zustand des Münzdepots</param>
        public void FinishPayment(IEnumerable<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                while (coin.Amount > 0)
                {
                    coin.Amount--;
                    ReturnCoinValues = AddIntToNumbersText(ReturnCoinValues, coin.CoinValue);
                }
            }
        }

        private string AddIntToNumbersText(string numbersText, int coinValue)
        {
            if (numbersText == null)
                return $"{coinValue}";
            else
                return $"{numbersText};{coinValue}";
        }

        private int ConvertNumbersTextToInt(string numbersText)
        {
            int result = -1;

            string[] data = numbersText?.Split(';');

            if (data != null && data.Length > 0)
            {
                result = 0;
                for (int i = 0; i < data.Length; i++)
                {
                    if (int.TryParse(data[i], out int coinValue))
                    {
                        result += coinValue;
                    }
                }
            }
            return result;
        }
    }
}
