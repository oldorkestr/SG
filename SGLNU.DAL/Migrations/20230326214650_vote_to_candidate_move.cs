using Microsoft.EntityFrameworkCore.Migrations;

namespace SGLNU.DAL.Migrations
{
    public partial class vote_to_candidate_move : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Votings_VotingId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "VotingId",
                table: "Votes",
                newName: "CandidateId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_VotingId",
                table: "Votes",
                newName: "IX_Votes_CandidateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Candidates_CandidateId",
                table: "Votes");

            migrationBuilder.RenameColumn(
                name: "CandidateId",
                table: "Votes",
                newName: "VotingId");

            migrationBuilder.RenameIndex(
                name: "IX_Votes_CandidateId",
                table: "Votes",
                newName: "IX_Votes_VotingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Votings_VotingId",
                table: "Votes",
                column: "VotingId",
                principalTable: "Votings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
