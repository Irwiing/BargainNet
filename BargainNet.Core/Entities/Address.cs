using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class Address : EntityBase
    {
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Complement { get; set; }
        public string Number { get; set; }
    }
}
