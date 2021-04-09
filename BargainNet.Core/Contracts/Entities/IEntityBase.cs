using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Entities
{
    public interface IEntityBase
    {
        Guid Id { get; set; }
    }
}
