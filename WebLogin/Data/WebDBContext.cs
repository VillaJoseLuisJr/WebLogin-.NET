using Microsoft.EntityFrameworkCore;
using WebLogin.Models;

namespace WebLogin.Data{

    public class WebDBContext : DbContext
    {
        public WebDBContext(DbContextOptions<WebDBContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(tb =>
            {
                tb.HasKey(col => col.IdUsuario);
                tb.Property(col => col.IdUsuario)
                .UseMySqlIdentityColumn()
                .ValueGeneratedOnAdd();

                tb.Property(col => col.NombreCompleto).HasMaxLength(50);
                tb.Property(col => col.Correo).HasMaxLength(50);
                tb.Property(col => col.Clave).HasMaxLength(50);
            });

            modelBuilder.Entity<Usuario>().ToTable("Usuario");
        }
    }
}