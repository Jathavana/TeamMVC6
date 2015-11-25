using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using TeamMVC6.Models;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace TeamMVC6.Migrations
{
    [DbContext(typeof(OptionsContext))]
    partial class MyFirstMigration
    {
        public override string Id
        {
            get { return "20151120222623_MyFirstMigration"; }
        }

        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Annotation("ProductVersion", "7.0.0-beta7-15540")
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn);

            modelBuilder.Entity("TeamMVC6.Models.Choice", b =>
                {
                    b.Property<int>("ChoiceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("FirstChoiceOptionId");

                    b.Property<int?>("FourthChoiceOptionId");

                    b.Property<int?>("SecondChoiceOptionId");

                    b.Property<DateTime>("SelectionDate");

                    b.Property<string>("StudentFirstName")
                        .Required()
                        .Annotation("MaxLength", 40);

                    b.Property<string>("StudentId")
                        .Required();

                    b.Property<string>("StudentLastName")
                        .Required()
                        .Annotation("MaxLength", 40);

                    b.Property<int?>("ThirdChoiceOptionId");

                    b.Property<int>("YearTermId");

                    b.Key("ChoiceId");
                });

            modelBuilder.Entity("TeamMVC6.Models.Option", b =>
                {
                    b.Property<int>("OptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsActive");

                    b.Property<string>("Title")
                        .Required()
                        .Annotation("MaxLength", 50);

                    b.Key("OptionId");
                });

            modelBuilder.Entity("TeamMVC6.Models.YearTerm", b =>
                {
                    b.Property<int>("YearTermId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsDefault");

                    b.Property<int>("Term");

                    b.Property<int>("Year");

                    b.Key("YearTermId");
                });

            modelBuilder.Entity("TeamMVC6.Models.Choice", b =>
                {
                    b.Reference("TeamMVC6.Models.Option")
                        .InverseCollection()
                        .ForeignKey("FirstChoiceOptionId");

                    b.Reference("TeamMVC6.Models.Option")
                        .InverseCollection()
                        .ForeignKey("FourthChoiceOptionId");

                    b.Reference("TeamMVC6.Models.Option")
                        .InverseCollection()
                        .ForeignKey("SecondChoiceOptionId");

                    b.Reference("TeamMVC6.Models.Option")
                        .InverseCollection()
                        .ForeignKey("ThirdChoiceOptionId");

                    b.Reference("TeamMVC6.Models.YearTerm")
                        .InverseCollection()
                        .ForeignKey("YearTermId");
                });
        }
    }
}
