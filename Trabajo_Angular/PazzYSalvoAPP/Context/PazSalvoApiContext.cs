using System;
using System.Collections.Generic;
using PazzYSalvoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace PazzYSalvoApp.Context;

public partial class PazSalvoApiContext : DbContext
{
    public PazSalvoApiContext()
    {
    }

    public PazSalvoApiContext(DbContextOptions<PazSalvoApiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Factura> Facturas { get; set; }

    public virtual DbSet<MediosDePago> MediosDePagos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-88MTFLQ\\SQLEXPRESS;Initial Catalog=PazSalvoApi;Integrated Security=true;Encrypt=True;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Factura>(entity =>
        {
            entity.HasIndex(e => e.ClienteId, "IX_Facturas_ClienteId");

            entity.HasIndex(e => e.EstadoId, "IX_Facturas_EstadoId");

            entity.HasIndex(e => e.MedioDePagoId, "IX_Facturas_MedioDePagoId");

            entity.Property(e => e.Saldo).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Valor).HasColumnType("decimal(18, 2)");

            entity.HasOne(d => d.Cliente).WithMany(p => p.Facturas).HasForeignKey(d => d.ClienteId);

            entity.HasOne(d => d.Estado).WithMany(p => p.Facturas).HasForeignKey(d => d.EstadoId);

            entity.HasOne(d => d.MedioDePago).WithMany(p => p.Facturas).HasForeignKey(d => d.MedioDePagoId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
