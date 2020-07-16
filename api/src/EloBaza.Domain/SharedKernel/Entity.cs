using System;
using System.Collections.Generic;
using System.Reflection;

namespace EloBaza.Domain.SharedKernel
{
    public abstract class Entity
    {
        private int Id { get; set; }

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
            MarkAsDeleted(userId, DateTime.UtcNow);
        }

        private void MarkAsDeleted(int userId, DateTime now)
        {
            IsDeleted = true;
            DeletedAt = now;
            DeletedBy = userId;

            foreach (var prop in GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (prop.GetValue(this) is Entity)
                {
                    var entity = prop.GetValue(this) as Entity;
                    if (!(entity?.IsDeleted ?? true))
                        entity.MarkAsDeleted(userId, now);
                }
                else if (prop.GetValue(this) is IEnumerable<Entity>)
                {
                    var entities = prop.GetValue(this) as IEnumerable<Entity> ?? Array.Empty<Entity>();
                    foreach (var entity in entities)
                    {
                        if (!(entity.IsDeleted))
                            entity.MarkAsDeleted(userId, now);
                    }
                }
            }
        }

        public bool IsTransient()
        {
            return Id == 0;
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
