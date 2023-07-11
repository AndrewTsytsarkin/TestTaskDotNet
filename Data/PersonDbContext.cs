using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PersonEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public string Position { get; set; }
    public string FirstName { get; set; }
    public string SecondName { get; set; }
}

public class PersonDbContext : DbContext
{
    public PersonDbContext(DbContextOptions<PersonDbContext> options)
           : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PersonEntity>()
           .ToTable("persons")
           .HasAlternateKey(c => new { c.FirstName, c.SecondName });

    }

    public DbSet<PersonEntity> Persons { get; set; }
}