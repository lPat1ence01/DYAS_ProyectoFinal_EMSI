using EMSI_Corporation.Models;
using Microsoft.EntityFrameworkCore;


namespace EMSI_Corporation.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        DbSet<Persona> personas { get; set; }
        DbSet<Cliente> clientes { get; set; }
        DbSet<Trabajador> trabajador { get; set; }
        DbSet<Usuario> usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>(tb =>
            {
                // Persona
                modelBuilder.Entity<Persona>(tb =>
                {
                    tb.HasKey(p => p.IdPersona);
                    tb.Property(p => p.Nombres).HasMaxLength(100).IsRequired();
                    tb.Property(p => p.Apellidos).HasMaxLength(100).IsRequired();
                    tb.Property(p => p.NumCelular).HasMaxLength(20);
                    tb.Property(p => p.Direccion).HasMaxLength(150);
                    tb.Property(p => p.Correo).HasMaxLength(100);
                    tb.Property(p => p.DNI).HasMaxLength(15);
                });

                // Cliente
                modelBuilder.Entity<Cliente>(tb =>
                {
                    tb.HasKey(c => c.IdCliente);
                    tb.Property(c => c.TipoCliente).HasMaxLength(50).IsRequired();
                    tb.Property(c => c.RUC).HasMaxLength(15);
                    tb.HasOne(c => c.persona)
                        .WithOne(p => p.Cliente)
                        .HasForeignKey<Cliente>(c => c.IdCliente);
                });

                // Trabajador
                modelBuilder.Entity<Trabajador>(tb =>
                {
                    tb.HasKey(t => t.IdTrabajador);
                    tb.Property(t => t.Cargo).HasMaxLength(50).IsRequired();
                    tb.Property(t => t.Sueldo).HasColumnType("decimal(10,2)");
                    tb.HasOne(t => t.Persona)
                        .WithOne(p => p.Trabajador)
                        .HasForeignKey<Trabajador>(t => t.IdTrabajador);
                });

                // Usuario
                modelBuilder.Entity<Usuario>(tb =>
                {
                    tb.HasKey(u => u.IdUsuario);
                    tb.Property(u => u.Username).HasMaxLength(50).IsRequired();
                    tb.Property(u => u.Password).HasMaxLength(255).IsRequired();
                    tb.Property(u => u.Rol).HasMaxLength(50).IsRequired();
                    tb.HasOne(u => u.Trabajador)
                        .WithMany()
                        .HasForeignKey(u => u.TrabajadorID)
                        .OnDelete(DeleteBehavior.Cascade);
                });

                // Opcional: cambiar los nombres de las tablas si lo deseas
                modelBuilder.Entity<Persona>().ToTable("Persona");
                modelBuilder.Entity<Cliente>().ToTable("Cliente");
                modelBuilder.Entity<Trabajador>().ToTable("Trabajador");
                modelBuilder.Entity<Usuario>().ToTable("Usuario");
            });

        }
    }
}
