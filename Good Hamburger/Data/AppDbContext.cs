using Good_Hamburger.Model.Cardapio;
using Good_Hamburger.Model.Pedidos;
using Microsoft.EntityFrameworkCore;


namespace Good_Hamburger.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<PedidosModel> Pedidos { get; set; }
    public DbSet<CardapioModel> Cardapio { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configura o relacionamento muitos-para-muitos explicitamente
        modelBuilder.Entity<PedidosModel>()
            .HasMany(p => p.Itens)
            .WithMany()
            .UsingEntity(j => j.ToTable("PedidoItens"));

        modelBuilder.Entity<CardapioModel>().HasData(
            new CardapioModel { Id = 1, Nome = "X-Burguer", Preco = 5.00m, Tipo = TipoItemModel.Sanduíches },
            new CardapioModel { Id = 2, Nome = "X-Egg", Preco = 4.50m, Tipo = TipoItemModel.Sanduíches },
            new CardapioModel { Id = 3, Nome = "X-Bacon", Preco = 7.00m, Tipo = TipoItemModel.Sanduíches },
            new CardapioModel { Id = 4, Nome = "Batata Frita", Preco = 2.00m, Tipo = TipoItemModel.Acompanhamentos },
            new CardapioModel { Id = 5, Nome = "Refrigerante", Preco = 2.50m, Tipo = TipoItemModel.Acompanhamentos }
        );
    }
}