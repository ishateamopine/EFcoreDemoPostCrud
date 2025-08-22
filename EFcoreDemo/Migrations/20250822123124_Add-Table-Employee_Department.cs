using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFcoreDemo.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEmployee_Department : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "posts");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmpSalary = table.Column<int>(type: "int", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: false),
                    DepartmentDeptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmpId);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentDeptId",
                        column: x => x.DepartmentDeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentDeptId",
                table: "Employees",
                column: "DepartmentDeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "posts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "posts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "posts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "posts",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
