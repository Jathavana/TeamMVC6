using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaDataModel.Model
{
    public class Option
    {
        public int OptionsId { get; set; }

        [Required]
        [MaxLength(50), MinLength(3)]
        public string Title { get; set; }

        public bool isActive { get; set; }
    }
}
