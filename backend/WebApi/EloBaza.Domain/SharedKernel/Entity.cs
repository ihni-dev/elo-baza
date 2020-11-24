using System;

namespace EloBaza.Domain.SharedKernel
{
    public abstract class Entity
    {
        protected int Id { get; private set; }

        public Guid Key { get; protected set; }

        public DateTime CreatedAt { get; private set; }
        public int CreatedBy { get; private set; }

        public DateTime LastModifiedAt { get; private set; }
        public int LastModifiedBy { get; private set; }

        public bool IsDeleted { get; private set; }
        public DateTime? DeletedAt { get; private set; }
        public int? DeletedBy { get; private set; }

        internal void SetCreationData(int userId)
        {
            CreatedAt = LastModifiedAt = DateTime.UtcNow;
            CreatedBy = LastModifiedBy = userId;
        }

        internal void SetModificationData(int userId)
        {
            LastModifiedAt = DateTime.UtcNow;
            LastModifiedBy = userId;
        }

        internal void MarkAsDeleted(int userId)
        {
            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = userId;
        }

        internal void MarkAsNotDeleted(int userId)
        {
            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;
            SetModificationData(userId);
        }

        public bool IsTransient()
        {
            return Id == 0;
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity other)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (IsTransient() || other.IsTransient())
                return false;

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode(StringComparison.InvariantCulture);
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
