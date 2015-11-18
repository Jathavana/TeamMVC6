using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TeamMVC6.Models
{
    public class Choice
    {
        [HiddenInput]
        [ScaffoldColumn(false)]
        public int ChoiceId { get; set; }

        //No dropdown of yearTerm - use constructor to retrieve default Term
        [ScaffoldColumn(false)]
        public int YearTermId { get; set; }

        [Required]
        [Display(Name = "Student ID")]
        [RegularExpression("[Aa]00\\d{6}", ErrorMessage = "Invalid Student ID, A00123456 is the expected format")]
        public string StudentId { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Student First Name")]
        public string StudentFirstName { get; set; }

        [Required]
        [MaxLength(40)]
        [Display(Name = "Student Last Name")]
        public string StudentLastName { get; set; }


        [Display(Name = "First Option")]
        public int? FirstChoiceOptionId { get; set; }

        [Display(Name = "Second Option")]
        public int? SecondChoiceOptionId { get; set; }

        [Display(Name = "Third Option")]
        public int? ThirdChoiceOptionId { get; set; }

        [Display(Name = "Fourth Option")]
        public int? FourthChoiceOptionId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime SelectionDate { get; set; }

        public YearTerm YearTerm { get; set; }

        [Display(Name = "First Option")]
        public Option FirstChoiceOption { get; set; }

        [Display(Name = "Second Option")]
        public Option SecondChoiceOption { get; set; }

        [Display(Name = "Third Option")]
        public Option ThirdChoiceOption { get; set; }

        [Display(Name = "Fourth Option")]
        public Option FourthChoiceOption { get; set; }
    }
}
