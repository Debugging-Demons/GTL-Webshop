using System.ComponentModel.DataAnnotations;

namespace Webshop.Order.Domain.Common
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime Created { get; set; }

        /// <summary>
        /// Entities have both referential equality as well as identifier equality
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }
            if (ReferenceEquals(this, other))
            {
                return true;
            }
            if (GetType() != other.GetType())
            {
                return false;
            }
            if (Id == default || other.Id == default) //has not been set yet, thus they cannot be equal
            {
                return false;
            }
            return Id == other.Id; //identifier equality            
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }
    }
}
