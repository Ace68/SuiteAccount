using System;

namespace SuiteAccount.QueryModel.Dtos
{
    public class DtoBase
    {
        public Guid Id { get; set; }

        protected DtoBase()
        { }

        protected DtoBase(Guid id)
        {
            this.Id = id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as DtoBase);
        }

        public virtual bool Equals(DtoBase other)
        {
            return (null != other) && (this.GetType() == other.GetType()) && (other.Id == this.Id);
        }

        public static bool operator ==(DtoBase entity1, DtoBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
                return true;

            if ((object)entity1 == null || (object)entity2 == null)
                return false;

            return ((entity1.GetType() == entity2.GetType()) && (entity1.Id == entity2.Id));
        }

        public static bool operator !=(DtoBase entity1, DtoBase entity2)
        {
            return (!(entity1 == entity2));
        }
    }
}
