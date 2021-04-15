using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class AdAcutionSettings : EntityBase
    {
        public AdAcutionSettings()
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.UtcNow.AddDays(Constants.PackageExpirationTimeDays);
            Status = Status.Active;     
        }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Status Status { get; set; }
    }
}
