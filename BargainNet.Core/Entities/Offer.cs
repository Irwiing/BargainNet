using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class Offer : EntityBase
    {
        public decimal Value { get; set; }
        public virtual User User { get; set; }
    }
}
