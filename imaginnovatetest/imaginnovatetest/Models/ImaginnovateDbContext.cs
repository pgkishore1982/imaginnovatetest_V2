using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace imaginnovatetest.Models;

public partial class ImaginnovateDbContext : DbContext
{
    public ImaginnovateDbContext()
    {
    }

    public ImaginnovateDbContext(DbContextOptions<ImaginnovateDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblEmployee> TblEmployees { get; set; }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=123456;Database=imaginnovateDB;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblEmployee>(entity =>
        {
            entity.HasKey(e => e.Empid).HasName("tbl_employees_pkey");

            entity.ToTable("tbl_employees");

            entity.Property(e => e.Empid).HasColumnName("empid");
            entity.Property(e => e.Doj)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("doj");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(300)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(300)
                .HasColumnName("lastname");
            entity.Property(e => e.Phonenumber)
                .HasMaxLength(25)
                .HasColumnName("phonenumber");
            entity.Property(e => e.Salary).HasColumnName("salary");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
