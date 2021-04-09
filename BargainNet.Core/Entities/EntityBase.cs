using BargainNet.Core.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public abstract class EntityBase : IEntityBase
    {
        public Guid Id { get; set; }
    }
}
