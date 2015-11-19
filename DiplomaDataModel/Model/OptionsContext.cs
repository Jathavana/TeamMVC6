using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DiplomaDataModel.Model
{
    class OptionsContext: DbContext
    {
        public OptionsContext() : base("DefaultConnection") { }
        public DbSet<Option> Options { get; set; }
        public DbSet<YearTerm> YearTerms { get; set; }
        public DbSet<Choice> Choices { get; set; }

    }

}
