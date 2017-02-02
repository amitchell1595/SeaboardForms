using ContosoUniversity.Models;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
        class MDMSContext : DbContext
        {

        public DbSet<Department> Departments { get; set; }
        public DbSet<ChangeType> ChangeTypes { get; set; }
        public DbSet<DriverChangeType> DriverChangeTypes { get; set; }
        public DbSet<Payroll> Payrolls { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Company>Companies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Orbit> Orbits { get; set; }
        public DbSet<Domicile> Domiciles { get; set; }
        public DbSet<Entity> Entities { get; set; }
        public DbSet<Axle> Axles { get; set; }
        public DbSet<BillTo> BillTos { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<DriverTerminal> DriverTerminals { get; set; }
        public DbSet<SiteType> SiteTypes { get; set; }
        public DbSet<ProductClass> ProductClasses { get; set; }
        public DbSet<DriverPayroll> DriverPayrolls { get; set; }
        public DbSet<DriverType> DriverTypes { get; set; }
        public DbSet<ExitReason> ExitReasons { get; set; }
        public DbSet<DPayLevel> DriverPayLevels { get; set; }
        public DbSet<Comment> Comments { get; set; }

        public MDMSContext(): base("MasterDatabase")
        {
            //Database.SetInitializer<MDMSDBContext>(new MDMSDBContext());
            Configuration.ProxyCreationEnabled = false;

        }



    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

         
           // modelBuilder.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultConnection, Configuration>());
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            //modelBuilder.Entity<Course>()
            //    .HasMany(c => c.Instructors).WithMany(i => i.Courses)
            //    .Map(t => t.MapLeftKey("CourseID")
            //        .MapRightKey("InstructorID")
            //        .ToTable("CourseInstructor"));

            //modelBuilder.Entity<Department>().MapToStoredProcedures();
        }

        
    }
}