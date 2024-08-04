using BackendRestAPI.Model;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace BackendRestAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) 
        {

        }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Subcategory> Subcategories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>()
                .HasMany(c => c.Subcategories)
                .WithOne(s => s.Category);

            builder.Entity<Subcategory>()
                .HasMany(c => c.Contacts)
                .WithOne(c => c.Subcategory);

            builder.Entity<User>()
                .HasMany(u => u.Contacts)
                .WithOne(c => c.User);

            builder.Entity<Contact>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // set every string to be given in Consts size
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(string))
                    {
                        property.SetMaxLength(Consts.DefaultStringSizeInDb);
                        property.SetColumnType($"VARCHAR({Consts.DefaultStringSizeInDb})");
                    }
                }
            }
        }
    }
}
