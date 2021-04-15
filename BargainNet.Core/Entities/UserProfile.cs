using BargainNet.Core.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class UserProfile : EntityBase
    {
        public string Document { get; set; }
        public virtual Address Address { get; set; }
        public string ProfilePic { get; set; }
        public int BarganhaPoints { get; set; }
        public int TotalSlotsAd { get; set; }
        public Status Status { get; set; }
        public virtual IEnumerable<Category> Interests { get; set; }
        public virtual IEnumerable<PaydPackage> PaydPackages { get; set; }
        public virtual List<AdAuction> AdAuctions { get; set; }
        public virtual LegalPerson LegalPerson { get; set; }
        public virtual NaturalPerson NaturalPerson { get; set; }

    }
}
