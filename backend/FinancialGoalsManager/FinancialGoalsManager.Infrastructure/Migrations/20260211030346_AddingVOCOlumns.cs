using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinancialGoalsManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingVOCOlumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacoes_Goals_GoalId",
                table: "Transacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes");

            migrationBuilder.RenameTable(
                name: "Transacoes",
                newName: "Transactions");

            migrationBuilder.RenameColumn(
                name: "IsDeleted",
                table: "Goals",
                newName: "isDeleted");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Goals",
                newName: "creation_date");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Goals",
                newName: "IdealMonthlyDepositValue");

            migrationBuilder.RenameColumn(
                name: "IdealMonthlyDeposit",
                table: "Goals",
                newName: "AmountValue");

            migrationBuilder.RenameColumn(
                name: "AmountGoal",
                table: "Goals",
                newName: "AmountGoalValue");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Transactions",
                newName: "AmountValue");

            migrationBuilder.RenameIndex(
                name: "IX_Transacoes_GoalId",
                table: "Transactions",
                newName: "IX_Transactions_GoalId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goals",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "AmountGoal_Currency",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Amount_Currency",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "IdealMonthlyDeposit_Currency",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AmountCurrency",
                table: "Transactions",
                type: "nvarchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_date",
                table: "Transactions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Goals_GoalId",
                table: "Transactions",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Goals_GoalId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AmountGoal_Currency",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "Amount_Currency",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "IdealMonthlyDeposit_Currency",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "AmountCurrency",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "creation_date",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transacoes");

            migrationBuilder.RenameColumn(
                name: "isDeleted",
                table: "Goals",
                newName: "IsDeleted");

            migrationBuilder.RenameColumn(
                name: "creation_date",
                table: "Goals",
                newName: "CreationDate");

            migrationBuilder.RenameColumn(
                name: "IdealMonthlyDepositValue",
                table: "Goals",
                newName: "Amount");

            migrationBuilder.RenameColumn(
                name: "AmountValue",
                table: "Goals",
                newName: "IdealMonthlyDeposit");

            migrationBuilder.RenameColumn(
                name: "AmountGoalValue",
                table: "Goals",
                newName: "AmountGoal");

            migrationBuilder.RenameColumn(
                name: "AmountValue",
                table: "Transacoes",
                newName: "Amount");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_GoalId",
                table: "Transacoes",
                newName: "IX_Transacoes_GoalId");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Goals",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transacoes",
                table: "Transacoes",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacoes_Goals_GoalId",
                table: "Transacoes",
                column: "GoalId",
                principalTable: "Goals",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
