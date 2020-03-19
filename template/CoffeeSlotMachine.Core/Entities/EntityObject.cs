using CoffeeSlotMachine.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace CoffeeSlotMachine.Core.Entities
{
    public class EntityObject : IEntityObject
    {
        #region IEnityObject Members

        [Key]
        public virtual int Id { get; set; }

        // Concurreny in Entity Framework Core:
        // https://www.learnentityframeworkcore.com/concurrency
        [Timestamp]
        public virtual byte[] RowVersion
        {
            get;
            set;
        }

        #endregion
    }
}
