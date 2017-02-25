namespace RealEstateProject.Data
{
    using Migrations;
    using Model;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class dbContext : DbContext
    {
        // Your context has been configured to use a 'dbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'RealEstateProject.Data.dbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'dbContext' 
        // connection string in the application configuration file.
        public dbContext()
            : base("name=dbContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<dbContext, Configuration>());
        }

        public virtual DbSet<CATEGORY> CATEGORY { get; set; }
        public virtual DbSet<CATEGORYTYPES> CATEGORYTYPES { get; set; }
        public virtual DbSet<CITIES> CITIES { get; set; }
        public virtual DbSet<IMAGES> IMAGES { get; set; }
        public virtual DbSet<OWNERS> OWNERS { get; set; }
        public virtual DbSet<PARAMS> PARAMS { get; set; }
        public virtual DbSet<PROPERTIES> PROPERTIES { get; set; }
        public virtual DbSet<PROPERTYTYPES> PROPERTYTYPES { get; set; }
        public virtual DbSet<REGIONS> REGIONS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CATEGORY>()
                .HasMany(e => e.PARAMS)
                .WithRequired(e => e.CATEGORY)
                .HasForeignKey(e => e.CATEGORYNAME)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CATEGORYTYPES>()
                .HasMany(e => e.PARAMS)
                .WithRequired(e => e.CATEGORYTYPES)
                .HasForeignKey(e => e.CATEGORYVALUES)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROPERTIES>()
                .Property(e => e.RENT)
                .HasPrecision(8, 0);

            modelBuilder.Entity<PROPERTIES>()
                .Property(e => e.SELL)
                .HasPrecision(8, 0);

            modelBuilder.Entity<PROPERTIES>()
                .HasMany(e => e.IMAGES)
                .WithOptional(e => e.PROPERTIES)
                .HasForeignKey(e => e.PID);

            modelBuilder.Entity<PROPERTIES>()
                .HasMany(e => e.PARAMS)
                .WithRequired(e => e.PROPERTIES)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PROPERTYTYPES>()
                .HasMany(e => e.PROPERTIES)
                .WithOptional(e => e.PROPERTYTYPES)
                .HasForeignKey(e => e.PROPERTYTYPEID);
        }
    }
}