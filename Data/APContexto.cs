using ApiPedreirao.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiPedreirao.Data
{
    public class APContexto : DbContext
    {
        public APContexto(DbContextOptions<APContexto> options) : base(options) { }
        public DbSet<FechaNegocio> FechaNegocio { get; set; }
        public DbSet<FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Servico> Servico { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Usuario>().ToTable("Usuario");
            mb.Entity<FormaPagamento>().ToTable("FormaPagamento");




            /*  Relacionamento tabela Perfil */
          mb.Entity<Perfil>().ToTable("Perfil").HasKey(pe => pe.Id);

            mb.Entity<Perfil>().ToTable("Perfil")
            .HasOne<Usuario>(us => us.Usuario)
            .WithOne(pe => pe.Perfil)
            .HasForeignKey<Perfil>(p => p.IdUsuario);


            /* Relacionamento tabela FechaNegocio*/

           mb.Entity<FechaNegocio>().ToTable("FechaNegocio").HasKey(fh => fh.Id);


            mb.Entity<FechaNegocio>().ToTable("FechaNegocio")
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(fn => fn.FechaNegocio)
            .HasForeignKey(fne => fne.IdUsuarioC);

            mb.Entity<FechaNegocio>().ToTable("FechaNegocio")
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(fn => fn.FechaNegocio)
            .HasForeignKey(fne => fne.IdUsuarioP);

            mb.Entity<FechaNegocio>().ToTable("FechaNegocio")
            .HasOne<FormaPagamento>(fp => fp.FormaPagamento)
            .WithMany(fn => fn.FechaNegocio)
            .HasForeignKey(fne => fne.IdFormaDPag);

            /*  Relacionamento tabela Servico */
           mb.Entity<Servico>().ToTable("Servico").HasKey(se => se.Id);
            mb.Entity<Servico>().ToTable("Servico")
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(se => se.Servico)
            .HasForeignKey(ser => ser.IdUsuarioC);

            mb.Entity<Servico>().ToTable("Servico")
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(se => se.Servico)
            .HasForeignKey(ser => ser.IdUsuarioP);

            mb.Entity<Servico>().ToTable("Servico")
            .HasOne<Usuario>(us => us.Usuario)
            .WithMany(se => se.Servico)
            .HasForeignKey(ser => ser.IdFechaNegocio);



        }
    }


}