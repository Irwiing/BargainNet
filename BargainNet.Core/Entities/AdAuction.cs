using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class AdAuction : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string AdPic { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public virtual Category Category { get; set; }
        public virtual AdAcutionSettings AdAcutionSettings { get; set; }
        public virtual List<Offer> Offers{ get; set; }
    }
}