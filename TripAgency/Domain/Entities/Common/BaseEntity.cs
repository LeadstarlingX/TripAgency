using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Common
{
    public abstract class BaseEntity : IEquatable<BaseEntity>
    {
        public int Id { get; set; }

        public static bool operator ==(BaseEntity? first, BaseEntity? second)
        {
            return first is not null && second is not null && first.Equals(second);    
        }

        public static bool operator !=(BaseEntity? first, BaseEntity? second)
        {
            return !(first ==  second);
        }


        public bool Equals(BaseEntity? other)
        {
            if (other is null)
                return false;

            if (other.GetType() != GetType())
                return false;

            return other.Id == Id;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) 
                return false;

            if(obj.GetType() != GetType()) 
                return false;

            if (obj is not BaseEntity entity)
                return false;

            return entity.Id == Id;
        }

        public override int GetHashCode()
        {
           return  Id.GetHashCode();
            
        }
    }
}
