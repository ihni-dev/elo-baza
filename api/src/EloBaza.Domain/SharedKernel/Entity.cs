using System;

namespace EloBaza.Domain.SharedKernel
{
    public abstract class Entity
    {
        private int _id;
        
        public Guid Key { get; set; }

        public DateTime CreatedAt { get; private set; }
        public int CreatedBy { get; private set; }

        public DateTime LastModifiedAt { get; private set; }
        public int LastModifiedBy { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime DeletedAt { get; private set; }
        public int? DeletedBy { get; private set; }

        public void SetCreationData(int userId)
        {
            CreatedAt = LastModifiedAt = DateTime.UtcNow;
            CreatedBy = LastModifiedBy = userId;
        }

        public void SetModificationData(int userId)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = userId;
        }

        public void Delete(int userId)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }

        public bool IsTransient()
        {
            return _id == 0;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (IsTransient() || other.IsTransient())
                return false;

            return _id == other._id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + _id).GetHashCode(StringComparison.InvariantCulture);
        }

        public static bool operator ==(Entity left, Entity right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Entity left, Entity right)
        {
            return !(left == right);
        }
    }
}
