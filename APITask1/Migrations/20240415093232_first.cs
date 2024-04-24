using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APITask1.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    //__EFMigrationsHistory
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            //Insert data
            //migrationBuilder.InsertData(
            //    table: "Persons",
            //    columns: new[] { "Id", "Name", "Type", "Phone", "Username", "Password", "Email" },
            //    values: new object[,]
            //    {
            //    { Guid.NewGuid(), "Admin", "Admin", "1234567890", "Admin2", " ", "admin12345@gmail.com" }
            //{ Guid.NewGuid(), "User", "User", "9876543210", "User1", " ", "user456@gmail.com" }

            //});


            migrationBuilder.Sql("INSERT INTO Persons (Id,Name,Type,Username,Password,Email) VALUES ( 'Guid.NewGuid()','Admin','Admin','1234567890','Admin123',' ','harpreetbhatia1917@gmail.com' )");
            //migrationBuilder.Sql("INSERT INTO Persons (Id, Name, Type, Username, Password, Email) " +
            //         $"VALUES ('{Guid.NewGuid()}', 'Admin', 'Admin', '1234567890', 'Admin123', 'harpreetbhatia1917@gmail.com')");
            //migrationBuilder.Sql("INSERT INTO Persons (Email, Password) VALUES ('harpreet19@gmail.com', 'harpreet1234')");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
