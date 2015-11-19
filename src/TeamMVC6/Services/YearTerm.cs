using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DiplomaDataModel.Model
{
    public class YearTerm
    {
        public int YearTermId { get; set; }
        [DataType(DataType.Text)]
        public int Year { get; set; }
        [DataType(DataType.Text)]
        [RegularExpression("(10|20|30)", ErrorMessage ="Invalid Term.")]
        public int Term { get; set; }
        public bool isDefault { get; set; }
    }
}