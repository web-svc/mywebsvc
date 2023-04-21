using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PortalWeb.Models;

namespace PortalWeb.Data;

public partial class ApplicationDbContext : DbContext
{


    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<App> Apps { get; set; }

    public virtual DbSet<AuthServer> AuthServers { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Router> Routers { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserInApp> UserInApps { get; set; }

    public virtual DbSet<UserInRole> UserInRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=WebPortal;Trusted_Connection=True; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<App>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__App__3214EC07333458BE");

            entity.ToTable("App");

            entity.HasIndex(e => e.Secret, "UQ__App__8F8373A15F0C10A3").IsUnique();

            entity.Property(e => e.LogoUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(40);
            entity.Property(e => e.RedirectUrl)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Secret)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(CONVERT([varchar](255),lower(newid())))");
            entity.Property(e => e.TagLine).HasMaxLength(50);

            entity.HasOne(d => d.User).WithMany(p => p.Apps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__App__UserId__52593CB8");
        });

        modelBuilder.Entity<AuthServer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__AuthServ__3214EC07BE84E2DD");

            entity.ToTable("AuthServer");

            entity.Property(e => e.Key).HasMaxLength(255);
            entity.Property(e => e.Secret).HasMaxLength(255);

            entity.HasOne(d => d.App).WithMany(p => p.AuthServers)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuthServe__AppId__6EF57B66");

            entity.HasOne(d => d.Router).WithMany(p => p.AuthServers)
                .HasForeignKey(d => d.RouterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__AuthServe__Route__6FE99F9F");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Country__3214EC074B60A19C");

            entity.ToTable("Country");

            entity.HasIndex(e => e.Code, "UQ__Country__A25C5AA777A1FCFD").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Customer__3214EC07A2DEC2A0");

            entity.ToTable("Customer");

            entity.HasIndex(e => e.UserId, "UQ__Customer__1788CC4D9A0572E5").IsUnique();

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.City).HasMaxLength(70);
            entity.Property(e => e.Company).HasMaxLength(40);
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(70);

            entity.HasOne(d => d.Country).WithMany(p => p.Customers)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__Customer__Countr__36B12243");

            entity.HasOne(d => d.State).WithMany(p => p.Customers)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("FK__Customer__StateI__37A5467C");

            entity.HasOne(d => d.User).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Customer__UserId__38996AB5");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC076B821EC5");

            entity.HasIndex(e => e.Description, "UQ__Roles__4EBBBAC948F24140").IsUnique();

            entity.HasIndex(e => e.Name, "UQ__Roles__737584F66775D866").IsUnique();

            entity.HasIndex(e => e.InnerName, "UQ__Roles__73DA93DA7467410B").IsUnique();

            entity.Property(e => e.Description).HasMaxLength(50);
            entity.Property(e => e.InnerName)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name).HasMaxLength(25);

            entity.HasOne(d => d.App).WithMany(p => p.Roles)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Roles__AppId__5EBF139D");
        });

        modelBuilder.Entity<Router>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OAuthRou__3214EC07C90747E9");

            entity.ToTable("Router");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).HasMaxLength(30);
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__States__3214EC07186FF386");

            entity.HasIndex(e => e.StateId, "UQ__States__C3BA3B3B10ED8377").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__States__CountryI__32E0915F");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC071AFDC7D1");

            entity.HasIndex(e => e.WebGuid, "UQ__Users__C790C838BC861566").IsUnique();

            entity.HasIndex(e => e.UserName, "UQ__Users__C9F28456C2BF0E2C").IsUnique();

            entity.Property(e => e.CreationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.OauthRouter).HasColumnName("OAuthRouter");
            entity.Property(e => e.Password)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.WebGuid)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasDefaultValueSql("(CONVERT([varchar](255),lower(newid())))");

            entity.HasOne(d => d.OauthRouterNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.OauthRouter)
                .HasConstraintName("FK__Users__OAuthRout__2C3393D0");
        });

        modelBuilder.Entity<UserInApp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInAp__3214EC07FAD6493A");

            entity.ToTable("UserInApp");

            entity.HasOne(d => d.App).WithMany(p => p.UserInApps)
                .HasForeignKey(d => d.AppId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInApp__AppId__6C190EBB");

            entity.HasOne(d => d.User).WithMany(p => p.UserInApps)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInApp__UserI__6B24EA82");
        });

        modelBuilder.Entity<UserInRole>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UserInRo__3214EC0727C60F9D");

            entity.HasOne(d => d.Role).WithMany(p => p.UserInRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInRol__RoleI__68487DD7");

            entity.HasOne(d => d.User).WithMany(p => p.UserInRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserInRol__UserI__6754599E");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
