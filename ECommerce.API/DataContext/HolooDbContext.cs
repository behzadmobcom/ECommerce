using Ecommerce.Entities.HolooEntity;
using Entities.HolooEntity;
using Microsoft.EntityFrameworkCore;

namespace API.DataContext;

public class HolooDbContext : DbContext
{
    public HolooDbContext()
    {
    }

    public HolooDbContext(DbContextOptions<HolooDbContext> options) : base(options)
    {
    }

    public virtual DbSet<HolooABail> ABAILPRE { get; set; }
    public virtual DbSet<HolooAccountNumber> ACOUND_N { get; set; }
    public virtual DbSet<HolooArticle> ARTICLE { get; set; }
    public virtual DbSet<HolooCustomer> Customer { get; set; }
    public virtual DbSet<HolooSarfasl> Sarfasl { get; set; }
    public virtual DbSet<HolooFBail> FBAILPRE { get; set; }
    public virtual DbSet<HolooMGroup> M_GROUP { get; set; }
    public virtual DbSet<HolooSGroup> S_GROUP { get; set; }
    public virtual DbSet<HolooSanad> Sanad { get; set; }
    public virtual DbSet<HolooSndList> Snd_List { get; set; }
    public virtual DbSet<HolooUnit> UNIT { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer("Server=.\\mssql2008r2;Database=Holoo1;Trusted_Connection=True;MultipleActiveResultSets=true;");
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<HolooABail>()
            .HasKey(c => new {c.Fac_Code, c.Fac_Type, c.A_Code, c.A_Index});
        modelBuilder.Entity<HolooAccountNumber>()
            .HasKey(c => new {c.Bank_Code, c.Account_N});
        modelBuilder.Entity<HolooArticle>()
            .HasKey(c => new {c.A_Code});
        modelBuilder.Entity<HolooCustomer>()
            .HasKey(c => new {c.C_Code});  
        modelBuilder.Entity<HolooSarfasl>()
            .HasKey(c => new {c.ID});
        modelBuilder.Entity<HolooFBail>()
            .HasKey(c => new {c.Fac_Code, c.Fac_Type});
        modelBuilder.Entity<HolooMGroup>()
            .HasKey(c => new {c.M_groupcode});
        modelBuilder.Entity<HolooSGroup>()
            .HasKey(c => new {c.M_groupcode, c.S_groupcode});
        modelBuilder.Entity<HolooSanad>()
        .HasKey(c => new { c.Sanad_Code });
        modelBuilder.Entity<HolooSndList>()
      .HasKey(c => new { c.Index });
        modelBuilder.Entity<HolooUnit>()
            .HasKey(c => new {c.Unit_Code});
    }
}