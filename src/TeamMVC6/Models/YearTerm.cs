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
        [Range(2015, 3000)]
        public int Year { get; set; }
        [UIHint("_SeasonDropDown")]
        public int Term { get; set; }

        [Display(Name = "Default")]
        public Boolean IsDefault { get; set; }
    }
}

