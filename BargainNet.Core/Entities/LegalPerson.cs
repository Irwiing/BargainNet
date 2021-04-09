using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class LegalPerson : UserProfile
    {
        public LegalPerson()
        {
            TotalSlotsAd = Constants.LegalPersonFreeSlots;
        }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public string DocumentPic { get; set; }
    }
}
