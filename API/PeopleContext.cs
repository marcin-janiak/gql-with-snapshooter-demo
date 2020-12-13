using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API
{
    public class PeopleContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public PeopleContext(DbContextOptions<PeopleContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(GetType()
                .Assembly);
        }
    }

    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne<Person>(x => x.Supervisor)
                .WithOne()
                .HasForeignKey<Person>(x => x.SupervisorId)
                .IsRequired(false);
        }
    }
}