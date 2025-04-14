using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CSharpAspNetCoreExample.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MuscleExercise");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "RoutineExercises");

            migrationBuilder.RenameColumn(
                name: "RoutineId",
                table: "SetLog",
                newName: "RoutineLogId");

            migrationBuilder.RenameColumn(
                name: "RoutineId",
                table: "RoutineExercises",
                newName: "RoutinesId");

            migrationBuilder.RenameColumn(
                name: "ExerciseId",
                table: "RoutineExercises",
                newName: "ExercisesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises",
                columns: new[] { "ExercisesId", "RoutinesId" });

            migrationBuilder.CreateTable(
                name: "MuscleExercises",
                columns: table => new
                {
                    ExercisesId = table.Column<int>(type: "INTEGER", nullable: false),
                    MusclesId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercises", x => new { x.ExercisesId, x.MusclesId });
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Exercise_ExercisesId",
                        column: x => x.ExercisesId,
                        principalTable: "Exercise",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MuscleExercises_Muscle_MusclesId",
                        column: x => x.MusclesId,
                        principalTable: "Muscle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SetLog_ExerciseId",
                table: "SetLog",
                column: "ExerciseId");

            migrationBuilder.CreateIndex(
                name: "IX_SetLog_RoutineLogId",
                table: "SetLog",
                column: "RoutineLogId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineLog_RoutineId",
                table: "RoutineLog",
                column: "RoutineId");

            migrationBuilder.CreateIndex(
                name: "IX_RoutineExercises_RoutinesId",
                table: "RoutineExercises",
                column: "RoutinesId");

            migrationBuilder.CreateIndex(
                name: "IX_Muscle_GroupId",
                table: "Muscle",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_MuscleExercises_MusclesId",
                table: "MuscleExercises",
                column: "MusclesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Muscle_MuscleGroup_GroupId",
                table: "Muscle",
                column: "GroupId",
                principalTable: "MuscleGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_Exercise_ExercisesId",
                table: "RoutineExercises",
                column: "ExercisesId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineExercises_Routine_RoutinesId",
                table: "RoutineExercises",
                column: "RoutinesId",
                principalTable: "Routine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutineLog_Routine_RoutineId",
                table: "RoutineLog",
                column: "RoutineId",
                principalTable: "Routine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetLog_Exercise_ExerciseId",
                table: "SetLog",
                column: "ExerciseId",
                principalTable: "Exercise",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SetLog_RoutineLog_RoutineLogId",
                table: "SetLog",
                column: "RoutineLogId",
                principalTable: "RoutineLog",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Muscle_MuscleGroup_GroupId",
                table: "Muscle");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercises_Exercise_ExercisesId",
                table: "RoutineExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineExercises_Routine_RoutinesId",
                table: "RoutineExercises");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutineLog_Routine_RoutineId",
                table: "RoutineLog");

            migrationBuilder.DropForeignKey(
                name: "FK_SetLog_Exercise_ExerciseId",
                table: "SetLog");

            migrationBuilder.DropForeignKey(
                name: "FK_SetLog_RoutineLog_RoutineLogId",
                table: "SetLog");

            migrationBuilder.DropTable(
                name: "MuscleExercises");

            migrationBuilder.DropIndex(
                name: "IX_SetLog_ExerciseId",
                table: "SetLog");

            migrationBuilder.DropIndex(
                name: "IX_SetLog_RoutineLogId",
                table: "SetLog");

            migrationBuilder.DropIndex(
                name: "IX_RoutineLog_RoutineId",
                table: "RoutineLog");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises");

            migrationBuilder.DropIndex(
                name: "IX_RoutineExercises_RoutinesId",
                table: "RoutineExercises");

            migrationBuilder.DropIndex(
                name: "IX_Muscle_GroupId",
                table: "Muscle");

            migrationBuilder.RenameColumn(
                name: "RoutineLogId",
                table: "SetLog",
                newName: "RoutineId");

            migrationBuilder.RenameColumn(
                name: "RoutinesId",
                table: "RoutineExercises",
                newName: "RoutineId");

            migrationBuilder.RenameColumn(
                name: "ExercisesId",
                table: "RoutineExercises",
                newName: "ExerciseId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "RoutineExercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoutineExercises",
                table: "RoutineExercises",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "MuscleExercise",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ExerciseId = table.Column<int>(type: "INTEGER", nullable: false),
                    MuscleId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MuscleExercise", x => x.Id);
                });
        }
    }
}
