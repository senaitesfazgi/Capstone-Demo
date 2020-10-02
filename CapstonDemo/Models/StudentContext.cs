using CapstonDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CapstonDemo.Models
{
    public partial class StudentContext : DbContext
    {
        public StudentContext()
        {
        }

        public StudentContext(DbContextOptions<StudentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<School> Schools { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=(local)\\sqlexpress;Database=MVCAuthDB;Trusted_Connection=True;MultipleActiveResultSets=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.FirstName)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.LastName)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

               entity.Property(e => e.Address)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.Email)
                        .HasCharSet("utf8mb4")
                        .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.PhoneNumber)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

              /*  entity.HasData(
                    new Student()
                    {
                        ID = -1,
                        FirstName = "John",
                        LastName = "Doe"
                    },
                    new Student()
                    {
                        ID = -2,
                        FirstName = "Jane",
                        LastName = "Doe"
                    },
                    new Student()
                    {
                        ID = -3,
                        FirstName = "Todd",
                        LastName = "Smith"
                    },
                    new Student()
                    {
                        ID = -4,
                        FirstName = "Sue",
                        LastName = "Smith"
                    },
                    new Student()
                    {
                        ID = -5,
                        FirstName = "Joe",
                        LastName = "Smithserson"
                    }
                );*/
            });

            modelBuilder.Entity<School>(entity =>
            {
                entity.HasIndex(e => e.StudentID)
                    .HasName("FK_" + nameof(School) + "_" + nameof(Student));

                entity.Property(e => e.SchoolName)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");

                entity.Property(e => e.SchoolAddress)
                    .HasCharSet("utf8mb4")
                    .HasCollation("utf8mb4_general_ci");


                // Always in the one with the foreign key.
                entity.HasOne(child => child.Student)
                    .WithMany(parent => parent.Schools)
                    .HasForeignKey(child => child.StudentID)
                    // When we delete a record, we can modify the behaviour of the case where there are child records.
                    // Restrict: Throw an exception if we try to orphan a child record.
                    // Cascade: Remove any child records that would be orphaned by a removed parent.
                    // SetNull: Set the foreign key field to null on any orphaned child records.
                    // NoAction: Don't commit any deletions of parents which would orphan a child.
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_"+nameof(School)+"_"+nameof(Student));

               /* entity.HasData(
                        new PhoneNumber()
                        {
                            ID = -1,
                            Number = "800-234-4567",
                            PersonID = -1
                        },
                        new PhoneNumber()
                        {
                            ID = -2,
                            Number = "800-234-4567",
                            PersonID = -2
                        },
                        new PhoneNumber()
                        {
                            ID = -3,
                            Number = "800-345-5678",
                            PersonID = -2
                        },
                        new PhoneNumber()
                        {
                            ID = -4,
                            Number = "800-456-6789",
                            PersonID = -3
                        },
                        new PhoneNumber()
                        {
                            ID = -5,
                            Number = "800-987-7654",
                            PersonID = -4
                        },
                        new PhoneNumber()
                        {
                            ID = -6,
                            Number = "800-876-6543",
                            PersonID = -5
                        },
                        new PhoneNumber()
                        {
                            ID = -7,
                            Number = "800-765-5432",
                            PersonID = -5
                        }
                    );*/
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
