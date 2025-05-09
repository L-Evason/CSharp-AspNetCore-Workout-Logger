﻿// <auto-generated />
using System;
using CSharpAspNetCoreExample.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CSharpAspNetCoreExample.Migrations
{
    [DbContext(typeof(MvcWorkoutContext))]
    partial class MvcWorkoutContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("ExerciseMuscle", b =>
                {
                    b.Property<int>("ExercisesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MusclesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExercisesId", "MusclesId");

                    b.HasIndex("MusclesId");

                    b.ToTable("MuscleExercises", (string)null);
                });

            modelBuilder.Entity("ExerciseRoutine", b =>
                {
                    b.Property<int>("ExercisesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoutinesId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ExercisesId", "RoutinesId");

                    b.HasIndex("RoutinesId");

                    b.ToTable("RoutineExercises", (string)null);
                });

            modelBuilder.Entity("ExerciseRoutine.Models.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Exercise");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.Muscle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("GroupId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Muscle");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.MuscleGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MuscleGroup");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.Routine", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Routine");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.RoutineLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateLogged")
                        .HasColumnType("TEXT");

                    b.Property<int>("RoutineId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RoutineId");

                    b.ToTable("RoutineLog");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.SetLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Reps")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RoutineLogId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("SetTimestamp")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Weight")
                        .HasColumnType("decimal(5, 2)");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("RoutineLogId");

                    b.ToTable("SetLog");
                });

            modelBuilder.Entity("ExerciseMuscle", b =>
                {
                    b.HasOne("ExerciseRoutine.Models.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExerciseRoutine.Models.Muscle", null)
                        .WithMany()
                        .HasForeignKey("MusclesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExerciseRoutine", b =>
                {
                    b.HasOne("ExerciseRoutine.Models.Exercise", null)
                        .WithMany()
                        .HasForeignKey("ExercisesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExerciseRoutine.Models.Routine", null)
                        .WithMany()
                        .HasForeignKey("RoutinesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExerciseRoutine.Models.Muscle", b =>
                {
                    b.HasOne("ExerciseRoutine.Models.MuscleGroup", "Group")
                        .WithMany("Muscles")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.RoutineLog", b =>
                {
                    b.HasOne("ExerciseRoutine.Models.Routine", "Routine")
                        .WithMany("RoutineLogs")
                        .HasForeignKey("RoutineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Routine");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.SetLog", b =>
                {
                    b.HasOne("ExerciseRoutine.Models.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExerciseRoutine.Models.RoutineLog", "RoutineLog")
                        .WithMany("SetLogs")
                        .HasForeignKey("RoutineLogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exercise");

                    b.Navigation("RoutineLog");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.MuscleGroup", b =>
                {
                    b.Navigation("Muscles");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.Routine", b =>
                {
                    b.Navigation("RoutineLogs");
                });

            modelBuilder.Entity("ExerciseRoutine.Models.RoutineLog", b =>
                {
                    b.Navigation("SetLogs");
                });
#pragma warning restore 612, 618
        }
    }
}
