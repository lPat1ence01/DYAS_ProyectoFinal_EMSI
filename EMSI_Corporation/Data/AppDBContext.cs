using EMSI_Corporation.Models;
using Microsoft.EntityFrameworkCore;


namespace EMSI_Corporation.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {

        }

        public DbSet<Empleado> empleados { get; set; }
        public DbSet<Usuario> usuarios { get; set; }
        public DbSet<User_Rol> UserRoles { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Rol_Menu> RolMenus { get; set; }
        public DbSet<Menu> Menus {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>(tb =>
            {
                tb.HasKey(e => e.IdEmpleado);
                tb.Property(e => e.IdEmpleado).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(e => e.nomEmpleado).HasMaxLength(100).IsRequired();
                tb.Property(e => e.apeEmpleado).HasMaxLength(100).IsRequired();
                tb.Property(e => e.dni).HasMaxLength(8).IsRequired();
                tb.Property(e => e.gmail).HasMaxLength(100).IsRequired();
                tb.Property(e => e.numCelular).HasMaxLength(10).IsRequired();
            });

            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(u => u.IdUsuario);
                tb.Property(u => u.IdUsuario).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(u => u.usuario).HasMaxLength(100).IsRequired();
                tb.Property(u => u.password).HasMaxLength(100).IsRequired();

                tb.HasOne(u => u.empleado)
                    .WithOne(e => e.usuario)
                    .HasForeignKey<Usuario>(u => u.IdEmpleado)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User_Rol>(tb =>
            {
                tb.HasKey(ur => new
                {
                    ur.IdUsuario,
                    ur.IdRol
                });

                tb.HasOne(ur => ur.Usuario)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.IdUsuario);

                tb.HasOne(ur => ur.Rol)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.IdRol);

            });

            modelBuilder.Entity<Rol>(tb =>
            {
                tb.HasKey(r => r.IdRol);
                tb.Property(r => r.IdRol).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(r => r.nomRol).HasMaxLength(50).IsRequired();
                tb.Property(r => r.Descripcion).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<Rol_Menu>(tb =>
            {
                tb.HasKey(ur => new
                {
                    ur.IdMenu,
                    ur.IdRol
                });

                tb.HasOne(ur => ur.Rol)
                    .WithMany(u => u.RolMenus)
                    .HasForeignKey(ur => ur.IdRol);

                tb.HasOne(ur => ur.Menu)
                    .WithMany(u => u.RolMenus)
                    .HasForeignKey(ur => ur.IdMenu);
            });

            modelBuilder.Entity<Menu>(tb =>
            {
                tb.HasKey(m => m.IdMenu);
                tb.Property(m => m.IdMenu).UseIdentityColumn().ValueGeneratedOnAdd();
                tb.Property(m => m.nomMenu).HasMaxLength(50).IsRequired();
                tb.Property(m => m.Url).HasMaxLength(250).IsRequired();
            });

            modelBuilder.Entity<Empleado>().ToTable("Empleado");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<User_Rol>().ToTable("User_Rol");
            modelBuilder.Entity<Rol>().ToTable("Rol");
            modelBuilder.Entity<Rol_Menu>().ToTable("Rol_Menu");
            modelBuilder.Entity<Menu>().ToTable("Menu");
        }
    }
}
