using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

using Todo.Domain.Entities;

namespace Todo.Infrastructure.Persistence
{
    public class TodoContext : DbContext
    {
        public required DbSet<TodoItem> Todos { get; set; }

        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Store");

            modelBuilder.Entity<TodoItem>()
                        .ToContainer("Todos");
            
            modelBuilder.Entity<TodoItem>()
                        .HasPartitionKey(o => o.Id);

            modelBuilder.Entity<TodoItem>(entity =>
            { 
                entity.Property(e => e.Id);
                entity.Property(e => e.CreatedDate);
                entity.Property(e => e.Description);
                entity.Property(x => x.Status)
                      .HasConversion(new EnumToStringConverter<ItemStatus>());
            });
        }
    }
}
