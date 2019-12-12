using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using mxm.biz.Entities;

namespace mxm.dal.DBContext
{
    public partial class DbMxmContext : DbContext
    {
        public DbMxmContext()
        {
        }

        public DbMxmContext(DbContextOptions<DbMxmContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Company> Companies { get; set; }
        public virtual DbSet<Connection> Connections { get; set; }
        public virtual DbSet<Content> Contents { get; set; }
        public virtual DbSet<ContentDetail> ContentDetails { get; set; }
        public virtual DbSet<ContentText> ContentTexts { get; set; }
        public virtual DbSet<ContentType> ContentTypes { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<EvaluationCourse> EvaluationCourses { get; set; }
        public virtual DbSet<EvaluationCourseQuestion> EvaluationCourseQuestions { get; set; }
        public virtual DbSet<EvaluationMatter> EvaluationMatters { get; set; }
        public virtual DbSet<EvaluationMatterQuestion> EvaluationMatterQuestions { get; set; }
        public virtual DbSet<EvaluationStudentCourse> EvaluationStudentCourses { get; set; }
        public virtual DbSet<EvaluationStudentMatter> EvaluationStudentMatters { get; set; }
        public virtual DbSet<Hierarchy> Hierarchies { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Matter> Matters { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<PermissionType> PermissionTypes { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<Reason> Reasons { get; set; }
        public virtual DbSet<Rol> Rols { get; set; }
        public virtual DbSet<Schedule> Schedules { get; set; }
        public virtual DbSet<Screen> Screens { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<StudentContent> StudentContents { get; set; }
        public virtual DbSet<StudentContentDetail> StudentContentDetails { get; set; }
        public virtual DbSet<StudentCourse> StudentCourses { get; set; }
        public virtual DbSet<StudentMatter> StudentMatters { get; set; }
        public virtual DbSet<StudentSubTopic> StudentSubTopics { get; set; }
        public virtual DbSet<StudentTopic> StudentTopics { get; set; }
        public virtual DbSet<SubTopic> SubTopics { get; set; }
        public virtual DbSet<Token> Tokens { get; set; }
        public virtual DbSet<Topic> Topics { get; set; }
        public virtual DbSet<Tutorial> Tutorials { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRole> UserRoles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Category");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.HasIndex(e => e.StateId)
                    .HasName("IX_City_Sate");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_State");
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("Company");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Connection>(entity =>
            {
                entity.ToTable("Connection");

                entity.HasIndex(e => e.Date)
                    .HasName("IX_Conection_Date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Connections)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conection_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Connections)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Conection_Student");
            });

            modelBuilder.Entity<Content>(entity =>
            {
                entity.ToTable("Content");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.SubTopic)
                    .WithMany(p => p.Contents)
                    .HasForeignKey(d => d.SubTopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Content_SubTopic");
            });

            modelBuilder.Entity<ContentDetail>(entity =>
            {
                entity.ToTable("ContentDetail");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url).HasMaxLength(100);

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.ContentDetails)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentDetail_Content");

                entity.HasOne(d => d.ContentType)
                    .WithMany(p => p.ContentDetails)
                    .HasForeignKey(d => d.ContentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentDetail_ContentType");
            });

            modelBuilder.Entity<ContentText>(entity =>
            {
                entity.ToTable("ContentText");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.ContentDetail)
                    .WithMany(p => p.ContentTexts)
                    .HasForeignKey(d => d.ContentDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ContentText_ContentDetail");
            });

            modelBuilder.Entity<ContentType>(entity =>
            {
                entity.ToTable("ContentType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Project)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.ProjectId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_Project");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.HasIndex(e => e.CityId)
                    .HasName("IX_District_City");

                entity.HasIndex(e => e.ZipCode);

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ZipCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_District_City");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.ToTable("Document");

                entity.Property(e => e.Resource)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_DocumentType");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Document_Student");
            });

            modelBuilder.Entity<DocumentType>(entity =>
            {
                entity.ToTable("DocumentType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<EvaluationCourse>(entity =>
            {
                entity.ToTable("EvaluationCourse");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.EvaluationCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationCourse_Course");
            });

            modelBuilder.Entity<EvaluationCourseQuestion>(entity =>
            {
                entity.ToTable("EvaluationCourseQuestion");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EvaluationCourse)
                    .WithMany(p => p.EvaluationCourseQuestions)
                    .HasForeignKey(d => d.EvaluationCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationCourseQuestion_EvaluationCourse");
            });

            modelBuilder.Entity<EvaluationMatter>(entity =>
            {
                entity.ToTable("EvaluationMatter");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Matter)
                    .WithMany(p => p.EvaluationMatters)
                    .HasForeignKey(d => d.MatterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationMatter_Matter");
            });

            modelBuilder.Entity<EvaluationMatterQuestion>(entity =>
            {
                entity.ToTable("EvaluationMatterQuestion");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Question)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.EvaluationMatter)
                    .WithMany(p => p.EvaluationMatterQuestions)
                    .HasForeignKey(d => d.EvaluationMatterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationMatterQuestion_EvaluationMatter");
            });

            modelBuilder.Entity<EvaluationStudentCourse>(entity =>
            {
                entity.ToTable("EvaluationStudentCourse");

                entity.HasIndex(e => e.Date)
                    .HasName("IX_ESC_Date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.EvaluationStudentCourses)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentCourse_Category");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.EvaluationStudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.EvaluationStudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentCourse_Student");
            });

            modelBuilder.Entity<EvaluationStudentMatter>(entity =>
            {
                entity.ToTable("EvaluationStudentMatter");

                entity.HasIndex(e => e.Date)
                    .HasName("IX_ESM_Date");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.EvaluationStudentMatters)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentMatter_Category");

                entity.HasOne(d => d.Matter)
                    .WithMany(p => p.EvaluationStudentMatters)
                    .HasForeignKey(d => d.MatterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentMatter_Matter");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.EvaluationStudentMatters)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EvaluationStudentMatter_Student");
            });

            modelBuilder.Entity<Hierarchy>(entity =>
            {
                entity.ToTable("Hierarchy");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Hierarchies)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Hierarchy_User");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Location");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Building)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Location_District");
            });

            modelBuilder.Entity<Matter>(entity =>
            {
                entity.ToTable("Matter");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Matters)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Matter_Course");
            });

            modelBuilder.Entity<Notification>(entity =>
            {
                entity.ToTable("Notification");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Notifications)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK_Notification_Student");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.ToTable("Permission");

                entity.HasOne(d => d.Hierarchy)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.HierarchyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Hierarchy");

                entity.HasOne(d => d.PermissionType)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.PermissionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_PermissionType");

                entity.HasOne(d => d.Screen)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.ScreenId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Permission_Screen");
            });

            modelBuilder.Entity<PermissionType>(entity =>
            {
                entity.ToTable("PermissionType");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("Project");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Projects)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Company");
            });

            modelBuilder.Entity<Quote>(entity =>
            {
                entity.ToTable("Quote");

                entity.Property(e => e.Date)
                    .HasColumnType("date")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Time).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_Schedule");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Quotes)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Quote_Student");
            });

            modelBuilder.Entity<Reason>(entity =>
            {
                entity.ToTable("Reason");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Target)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Rol>(entity =>
            {
                entity.ToTable("Rol");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(150);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Schedule>(entity =>
            {
                entity.ToTable("Schedule");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Course");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Schedules)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Schedule_Location");
            });

            modelBuilder.Entity<Screen>(entity =>
            {
                entity.ToTable("Screen");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Icon).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State");

                entity.HasIndex(e => e.CountryId)
                    .HasName("IX_State_Country");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_State_Country");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.HasIndex(e => e.Curp)
                    .HasName("IX_Student_Curp")
                    .IsUnique();

                entity.HasIndex(e => e.Id)
                    .HasName("IX_Student");

                entity.HasIndex(e => e.UserId)
                    .HasName("IX_Student_User")
                    .IsUnique();

                entity.Property(e => e.Activated).HasColumnType("datetime");

                entity.Property(e => e.Building)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Curp)
                    .IsRequired()
                    .HasColumnName("CURP")
                    .HasMaxLength(20);

                entity.Property(e => e.DateofBirth).HasColumnType("date");

                entity.Property(e => e.Floor)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Mobile)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Company)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Company");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_District");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_User");
            });

            modelBuilder.Entity<StudentContent>(entity =>
            {
                entity.ToTable("StudentContent");

                entity.HasOne(d => d.Content)
                    .WithMany(p => p.StudentContents)
                    .HasForeignKey(d => d.ContentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentContent_Content");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.StudentContents)
                    .HasForeignKey(d => d.StudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentContent_StudentCourse");
            });

            modelBuilder.Entity<StudentContentDetail>(entity =>
            {
                entity.ToTable("StudentContentDetail");

                entity.Property(e => e.Viewed)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("StudentCourse");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Course");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentCourse_Student");
            });

            modelBuilder.Entity<StudentMatter>(entity =>
            {
                entity.ToTable("StudentMatter");

                entity.HasOne(d => d.Matter)
                    .WithMany(p => p.StudentMatters)
                    .HasForeignKey(d => d.MatterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentMatter_Matter");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.StudentMatters)
                    .HasForeignKey(d => d.StudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentMatter_StudentCourse");
            });

            modelBuilder.Entity<StudentSubTopic>(entity =>
            {
                entity.ToTable("StudentSubTopic");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.StudentSubTopics)
                    .HasForeignKey(d => d.StudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSubTopic_StudentCourse");

                entity.HasOne(d => d.SubTopic)
                    .WithMany(p => p.StudentSubTopics)
                    .HasForeignKey(d => d.SubTopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentSubTopic_SubTopic");
            });

            modelBuilder.Entity<StudentTopic>(entity =>
            {
                entity.ToTable("StudentTopic");

                entity.HasOne(d => d.StudentCourse)
                    .WithMany(p => p.StudentTopics)
                    .HasForeignKey(d => d.StudentCourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTopic_StudentCourse");

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.StudentTopics)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StudentTopic_Topic");
            });

            modelBuilder.Entity<SubTopic>(entity =>
            {
                entity.ToTable("SubTopic");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Topic)
                    .WithMany(p => p.SubTopics)
                    .HasForeignKey(d => d.TopicId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubTopic_Topic");
            });

            modelBuilder.Entity<Token>(entity =>
            {
                entity.ToTable("Token");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Date).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Token1)
                    .IsRequired()
                    .HasColumnName("Token")
                    .HasMaxLength(500);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Tokens)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Token_User");
            });

            modelBuilder.Entity<Topic>(entity =>
            {
                entity.ToTable("Topic");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Matter)
                    .WithMany(p => p.Topics)
                    .HasForeignKey(d => d.MatterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Topic_Matter");
            });

            modelBuilder.Entity<Tutorial>(entity =>
            {
                entity.ToTable("Tutorial");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.HasIndex(e => e.Email)
                    .HasName("UC_Email");

                entity.Property(e => e.Active)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Avatar).HasMaxLength(200);

                entity.Property(e => e.Created).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.MotherName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RolId)
                    .HasConstraintName("FK_User_Rol");
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.ToTable("UserRole");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
