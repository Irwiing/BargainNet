using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class PaydPackage : EntityBase
    {
        public PaydPackage()
        {
            PurchaseDate = DateTime.UtcNow;
            ExpirationDate = DateTime.UtcNow.AddDays(Constants.PackageExpirationTimeDays);
        }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get;}
        public DateTime ExpirationDate { get;}
    }
}
