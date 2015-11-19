using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using Microsoft.Data.Entity.SqlServer.Metadata;

namespace TeamMVC6.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    OptionId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    IsActive = table.Column<bool>(isNullable: false),
                    Title = table.Column<string>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.OptionId);
                });
            migrationBuilder.CreateTable(
                name: "YearTerm",
                columns: table => new
                {
                    YearTermId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    IsDefault = table.Column<bool>(isNullable: false),
                    Term = table.Column<int>(isNullable: false),
                    Year = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YearTerm", x => x.YearTermId);
                });
            migrationBuilder.CreateTable(
                name: "Choice",
                columns: table => new
                {
                    ChoiceId = table.Column<int>(isNullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerIdentityStrategy.IdentityColumn),
                    FirstChoiceOptionId = table.Column<int>(isNullable: true),
                    FourthChoiceOptionId = table.Column<int>(isNullable: true),
                    SecondChoiceOptionId = table.Column<int>(isNullable: true),
                    SelectionDate = table.Column<DateTime>(isNullable: false),
                    StudentFirstName = table.Column<string>(isNullable: false),
                    StudentId = table.Column<string>(isNullable: false),
                    StudentLastName = table.Column<string>(isNullable: false),
                    ThirdChoiceOptionId = table.Column<int>(isNullable: true),
                    YearTermId = table.Column<int>(isNullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Choice", x => x.ChoiceId);
                    table.ForeignKey(
                        name: "FK_Choice_Option_FirstChoiceOptionId",
                        column: x => x.FirstChoiceOptionId,
                        principalTable: "Option",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_Choice_Option_FourthChoiceOptionId",
                        column: x => x.FourthChoiceOptionId,
                        principalTable: "Option",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_Choice_Option_SecondChoiceOptionId",
                        column: x => x.SecondChoiceOptionId,
                        principalTable: "Option",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_Choice_Option_ThirdChoiceOptionId",
                        column: x => x.ThirdChoiceOptionId,
                        principalTable: "Option",
                        principalColumn: "OptionId");
                    table.ForeignKey(
                        name: "FK_Choice_YearTerm_YearTermId",
                        column: x => x.YearTermId,
                        principalTable: "YearTerm",
                        principalColumn: "YearTermId");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Choice");
            migrationBuilder.DropTable("Option");
            migrationBuilder.DropTable("YearTerm");
        }
    }
}
