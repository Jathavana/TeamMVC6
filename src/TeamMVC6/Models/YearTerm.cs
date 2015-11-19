﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMVC6.Models
{
    public class YearTerm
    {
        public int YearTermId { get; set; }
        public int Year { get; set; }
        [RegularExpression(@"(30)|(20)|(10)$", ErrorMessage = "Please enter a valid Term")]
        public int Term { get; set; }
        public Boolean IsDefault { get; set; }
    }
}

