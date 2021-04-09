﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BargainNet.Core.Entities
{
    public class NaturalPerson : UserProfile
    {
        public NaturalPerson()
        {
            TotalSlotsAd = Constants.NaturalPersonFreeSlots;
        }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
