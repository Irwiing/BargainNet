using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BargainNet.Core.Entities
{
    public class User : IdentityUser
    {
        public virtual UserProfile UserProfile { get; set; }
        
    }
}
