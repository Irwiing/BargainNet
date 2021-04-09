using BargainNet.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Contracts.Entities
{
    public interface IUserProfile : IEntityBase
    {
        string Document { get; set; }
        Address Address { get; set; }
        string ProfilePic { get; set; }
        int BarganhaPoints { get; set; }
        int TotalSlotsAd { get; set; }
        Status Status { get; set; }
        Category Interests { get; set; }
        IEnumerable<PaydPackage> PaydPackages { get; set; }
        IEnumerable<AdAuction> AdAuctions { get; set; }
    }
}
