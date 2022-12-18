using Microsoft.EntityFrameworkCore;
using MinimalAPI.Models;

namespace MinimalAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            //Categoria

            mb.Entity<Categoria>().HasKey(c => c.CategoriaId);
            mb.Entity<Categoria>().Property(c => c.Nome)
                                  .HasMaxLength(100)
                                  .IsRequired();

            mb.Entity<Categoria>().Property(c => c.Descricao)
                                  .HasMaxLength(100)
                                  .IsRequired();

            //Produto

            mb.Entity<Produto>().HasKey(p => p.ProdutoId);
            mb.Entity<Produto>().Property(c => c.Nome)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Produto>().Property(c => c.Descricao)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Produto>().Property(c => c.Imagem)
                                .HasMaxLength(100)
                                .IsRequired();

            mb.Entity<Produto>().Property(p => p.Preco)
                                .HasPrecision(10, 2);

            //Relacionamento

            mb.Entity<Produto>()
                .HasOne<Categoria>(c => c.Categoria)
                    .WithMany(p => p.Produtos)
                        .HasForeignKey(c => c.CategoriaId);
        }
    }
}
