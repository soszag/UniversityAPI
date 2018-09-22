using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UniversityAPI.Models
{
    public partial class EduDataContext : DbContext
    {
        public virtual DbSet<Claims> Claims { get; set; }
        public virtual DbSet<Classes> Classes { get; set; }
        public virtual DbSet<Events> Events { get; set; }
        public virtual DbSet<GradeDefinitions> GradeDefinitions { get; set; }
        public virtual DbSet<Grades> Grades { get; set; }
        public virtual DbSet<GradesHistory> GradesHistory { get; set; }
        public virtual DbSet<LoginHistory> LoginHistory { get; set; }
        public virtual DbSet<Parents> Parents { get; set; }
        public virtual DbSet<ParentStudent> ParentStudent { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Subjects> Subjects { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubject { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<UsersClaims> UsersClaims { get; set; }

        public EduDataContext(DbContextOptions<EduDataContext> options)
           : base(options)
        {
            //Database.Migrate();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-LR8PJSS;Initial Catalog=EduData;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Claims>(entity =>
            {
                entity.HasKey(e => e.ClaimId);

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.Property(e => e.ClaimName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.ClaimParameters).HasMaxLength(100);
            });

            modelBuilder.Entity<Classes>(entity =>
            {
                entity.HasKey(e => e.ClassId);

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Events>(entity =>
            {
                entity.HasKey(e => e.EventId);

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.DateWhen).HasColumnType("date");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_EventClass");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_EventSubject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Events)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EventTeacher");
            });

            modelBuilder.Entity<GradeDefinitions>(entity =>
            {
                entity.HasKey(e => e.GradeDefinitionId);

                entity.Property(e => e.GradeDefinitionId).HasColumnName("GradeDefinitionID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(6);

                entity.Property(e => e.Tag)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Value).HasColumnType("numeric(4, 2)");
            });

            modelBuilder.Entity<Grades>(entity =>
            {
                entity.HasKey(e => e.GradeId);

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.DateWhen).HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.GradeDefinitionId).HasColumnName("GradeDefinitionID");

                entity.Property(e => e.Percents).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.Points).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Value).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.Weight).HasColumnType("numeric(4, 2)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_GradeEvent");

                entity.HasOne(d => d.GradeDefinition)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.GradeDefinitionId)
                    .HasConstraintName("FK_GradeGradeDefinition");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeStudent");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_GradeSubject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Grades)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeTeacher");
            });

            modelBuilder.Entity<GradesHistory>(entity =>
            {
                entity.HasKey(e => e.GradeHistoryId);

                entity.Property(e => e.GradeHistoryId).HasColumnName("GradeHistoryID");

                entity.Property(e => e.DateWhen).HasColumnType("datetime");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.GradeDefinitionId).HasColumnName("GradeDefinitionID");

                entity.Property(e => e.GradeId).HasColumnName("GradeID");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Percents).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.Points).HasColumnType("numeric(4, 2)");

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Weight).HasColumnType("numeric(4, 2)");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.EventId)
                    .HasConstraintName("FK_GradeHistoryEvent");

                entity.HasOne(d => d.GradeDefinition)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.GradeDefinitionId)
                    .HasConstraintName("FK_GradeHistoryGradeDefinition");

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeHistoryGrade");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeHistoryStudent");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.SubjectId)
                    .HasConstraintName("FK_GradeHistorySubject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.GradesHistory)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeHistoryTeacher");
            });

            modelBuilder.Entity<LoginHistory>(entity =>
            {
                entity.Property(e => e.LoginHistoryId).HasColumnName("LoginHistoryID");

                entity.Property(e => e.LogIn).HasColumnType("datetime");

                entity.Property(e => e.LogOutDate).HasColumnType("datetime");

                entity.Property(e => e.LogoutReason).HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.LoginHistory)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LoginHistoryUser");
            });

            modelBuilder.Entity<Parents>(entity =>
            {
                entity.HasKey(e => e.ParentId);

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Adress).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(10);
            });

            modelBuilder.Entity<ParentStudent>(entity =>
            {
                entity.Property(e => e.ParentStudentId).HasColumnName("ParentStudentID");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.ParentStudent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_PS_Parent");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ParentStudent)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_PS_Student");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.StudentId);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.Adress).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(25);

                entity.Property(e => e.PostalCode).HasMaxLength(10);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK_StudentClass");
            });

            modelBuilder.Entity<Subjects>(entity =>
            {
                entity.HasKey(e => e.SubjectId);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ShortName)
                    .IsRequired()
                    .HasMaxLength(7);
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.HasKey(e => e.TeacherId);

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.Adress).HasMaxLength(100);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.City).HasMaxLength(100);

                entity.Property(e => e.Country).HasMaxLength(30);

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(100);

                entity.Property(e => e.LastName).HasMaxLength(100);

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.PostalCode).HasMaxLength(10);
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.HasKey(e => new { e.SubjectId, e.TeacherId, e.ClassId })
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.SubjectId).HasColumnName("SubjectID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.ClassId).HasColumnName("ClassID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.ClassId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubjectClass");

                entity.HasOne(d => d.Subject)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.SubjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubjectSubject");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TeacherSubjectTeacher");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreationDate).HasColumnType("datetime");

                entity.Property(e => e.LoggedTime).HasColumnType("datetime");

                entity.Property(e => e.ModificationDate).HasColumnType("datetime");

                entity.Property(e => e.ParentId).HasColumnName("ParentID");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SessionId)
                    .HasColumnName("SessionID")
                    .HasMaxLength(200);

                entity.Property(e => e.StudentId).HasColumnName("StudentID");

                entity.Property(e => e.TeacherId).HasColumnName("TeacherID");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_UserParent");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_UserStudent");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.TeacherId)
                    .HasConstraintName("FK_UserTeacher");
            });

            modelBuilder.Entity<UsersClaims>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ClaimId })
                    .ForSqlServerIsClustered(false);

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ClaimId).HasColumnName("ClaimID");

                entity.HasOne(d => d.Claim)
                    .WithMany(p => p.UsersClaims)
                    .HasForeignKey(d => d.ClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_ClaimUserClaim");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersClaims)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("PK_ClaimUserUser");
            });
        }
    }
}
