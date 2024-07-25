using System;

namespace Framework.Domain
{
    public class EntityBase
    {
        public long Id { get;protected set; }
        public DateTime CreationDateTime { get; private set; }

        public EntityBase()
        {
            CreationDateTime = DateTime.Now;
        }
    }
}
