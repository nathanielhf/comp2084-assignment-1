namespace nathanielhf_comp2084_assignment_1.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class GroceryListModel : DbContext
    {
        public GroceryListModel()
            : base("name=DefaultConnection")
        {
        }

        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .Property(e => e.storage_type)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .Property(e => e.aisle_number)
                .IsFixedLength();

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Items)
                .WithRequired(e => e.Department)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Item>()
                .Property(e => e.name)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.image)
                .IsFixedLength();

            modelBuilder.Entity<Item>()
                .Property(e => e.price)
                .HasPrecision(6, 2);
        }
    }
}
