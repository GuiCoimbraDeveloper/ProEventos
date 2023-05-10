using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;
using ProEventos.Domain.Identity;

namespace ProEventos.Persistence.Contextos
{
    public class ProEventosContext : IdentityDbContext<User, Role, int,
                                                       IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
                                                       IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options)
            : base(options) { }
        public virtual DbSet<Evento> Eventos { get; set; }
        public virtual DbSet<Lote> Lotes { get; set; }
        public virtual DbSet<Palestrante> Palestrantes { get; set; }
        public virtual DbSet<PalestranteEvento> PalestrantesEventos { get; set; }
        public virtual DbSet<RedeSocial> RedesSociais { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
           // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConve‌​ntion>();

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            }
            );
            
            modelBuilder.Entity<Evento>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Evento)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Evento>()
              .HasMany(e => e.PalestrantesEventos)
              .WithOne(rs => rs.Evento)
              .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Palestrante>()
                .HasMany(e => e.RedesSociais)
                .WithOne(rs => rs.Palestrante)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<PalestranteEvento>()
              .HasKey(PE => new { PE.EventoId, PE.PalestranteId });
        }
    }
}
