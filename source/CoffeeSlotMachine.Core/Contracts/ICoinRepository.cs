using CoffeeSlotMachine.Core.Entities;
using System.Collections.Generic;

namespace CoffeeSlotMachine.Core.Contracts
{
    public interface ICoinRepository
    {
        Coin[] GetAllCoins();
        void AddCoins(Coin[] coins);
        void RemoveCoins(Coin[] coins);
    }
}