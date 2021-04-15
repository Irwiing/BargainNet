using BargainNet.Core.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BargainNet.WebApp.ModelView
{
    public class CreateProfileView
    {
        public string Document { get; set; }
        public IFormFile ProfilePic { get; set; }
        public Address Address { get; set; }
        public List<Category> Categories { get; set; }
        public string CompanyName { get; set; }
        public string TradeName { get; set; }
        public string DocumentPic { get; set; }
        public string FullName { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
