using System;

namespace SuiteAccount.Domain.Shared.Abstracts
{
    public abstract class ValueBase : IValueBase
    {
        public Guid Id { get; protected set; }

        protected ValueBase()
        { }

        protected ValueBase(Guid id)
        {
            this.Id = id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as ValueBase);
        }

        public virtual bool Equals(ValueBase other)
        {
            return (null != other) && (this.GetType() == other.GetType()) && (other.Id == this.Id);
        }

        public static bool operator ==(ValueBase entity1, ValueBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
                return true;

            if ((object)entity1 == null || (object)entity2 == null)
                return false;

            return ((entity1.GetType() == entity2.GetType()) && (entity1.Id == entity2.Id));
        }

        public static bool operator !=(ValueBase entity1, ValueBase entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
