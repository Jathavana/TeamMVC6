using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeamMVC6.Models;

namespace TeamMVC6.Migrations.Seed
{
    public static class SeedData
    {
        public static void Initialize(OptionsContext context)
        {

            if (!context.Options.Any())
            {
                context.Options.Add(new Option { Title = "Data Communications", IsActive = true });
                context.Options.Add(new Option { Title = "Client Server", IsActive = true });
                context.Options.Add(new Option { Title = "Digital Processing", IsActive = true });
                context.Options.Add(new Option { Title = "Information Systems", IsActive = true});
                context.Options.Add(new Option { Title = "Database", IsActive = false });
                context.Options.Add(new Option { Title = "Web & Mobile", IsActive = true });
                context.Options.Add(new Option { Title = "Tech Pro", IsActive = false });


                context.SaveChanges();
            }

            if (!context.YearTerms.Any())
            {
                context.YearTerms.Add(new YearTerm { Year = 2015, Term = 10, IsDefault = false  });
                context.YearTerms.Add(new YearTerm { Year = 2015, Term = 20, IsDefault = false });
                context.YearTerms.Add(new YearTerm { Year = 2015, Term = 30, IsDefault = false });
                context.YearTerms.Add(new YearTerm { Year = 2016, Term = 10, IsDefault = false });
                context.YearTerms.Add(new YearTerm { Year = 2016, Term = 20, IsDefault = true });

                context.SaveChanges();
            }

            if (!context.Choices.Any())
            {
                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112221",
                    StudentFirstName = "Jim",
                    StudentLastName = "Raynor",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112222",
                    StudentFirstName = "Bill",
                    StudentLastName = "Hates",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 6,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112223",
                    StudentFirstName = "Steve",
                    StudentLastName = "Hobbs",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 4,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112224",
                    StudentFirstName = "Kelsey",
                    StudentLastName = "Smith",
                    FirstChoiceOptionId = 3,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 5,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112225",
                    StudentFirstName = "Sally",
                    StudentLastName = "Tran",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112226",
                    StudentFirstName = "Seetha",
                    StudentLastName = "Thiru",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 5,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 2,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112227",
                    StudentFirstName = "Meera",
                    StudentLastName = "Thiru",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112228",
                    StudentFirstName = "Tychus",
                    StudentLastName = "Fyndlay",
                    FirstChoiceOptionId = 5,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 2,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112229",
                    StudentFirstName = "Tassadar",
                    StudentLastName = "Adun",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112220",
                    StudentFirstName = "Sarah",
                    StudentLastName = "Kerrigan",
                    FirstChoiceOptionId = 2,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });






                //Second set of Students
                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112231",
                    StudentFirstName = "Harry",
                    StudentLastName = "Potter",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112232",
                    StudentFirstName = "Harry",
                    StudentLastName = "Dresden",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 6,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112233",
                    StudentFirstName = "Aleris",
                    StudentLastName = "Valerian",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 1,
                    FourthChoiceOptionId = 2,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112234",
                    StudentFirstName = "John",
                    StudentLastName = "Snow",
                    FirstChoiceOptionId = 5,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112235",
                    StudentFirstName = "Charles",
                    StudentLastName = "Nancy",
                    FirstChoiceOptionId = 3,
                    SecondChoiceOptionId = 5,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 3,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112236",
                    StudentFirstName = "Marcus",
                    StudentLastName = "Wester",
                    FirstChoiceOptionId = 1,
                    SecondChoiceOptionId = 5,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 2,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112237",
                    StudentFirstName = "Captain",
                    StudentLastName = "Shepard",
                    FirstChoiceOptionId = 5,
                    SecondChoiceOptionId = 3,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 5,
                    StudentId = "A00112238",
                    StudentFirstName = "Mordin",
                    StudentLastName = "Solus",
                    FirstChoiceOptionId = 3,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 2,
                    FourthChoiceOptionId = 5,
                    SelectionDate = DateTime.Now
                });

                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112229",
                    StudentFirstName = "Urdnot",
                    StudentLastName = "Wrex",
                    FirstChoiceOptionId = 5,
                    SecondChoiceOptionId = 2,
                    ThirdChoiceOptionId = 4,
                    FourthChoiceOptionId = 1,
                    SelectionDate = DateTime.Now
                });


                context.Choices.Add(new Choice
                {
                    YearTermId = 4,
                    StudentId = "A00112220",
                    StudentFirstName = "Martin",
                    StudentLastName = "Sheen",
                    FirstChoiceOptionId = 4,
                    SecondChoiceOptionId = 1,
                    ThirdChoiceOptionId = 3,
                    FourthChoiceOptionId = 2,
                    SelectionDate = DateTime.Now
                });

                context.SaveChanges();
            }
        }
    }
}
