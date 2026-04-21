using Good_Hamburger.Model.Cardapio;
using Good_Hamburger.Services;

namespace Good_Hamburger.Tests
{
    public class PedidoServiceTests
    {
        // Helpers — IDs e preços alinhados com CardapioDbContext
        private static CardapioModel Sanduiche(int id = 1) => new() { Id = id, Nome = "X-Burguer", Preco = 5.00m, Tipo = TipoItemModel.Sanduíches };
        private static CardapioModel Batata(int id = 4) => new() { Id = id, Nome = "Batata Frita", Preco = 2.00m, Tipo = TipoItemModel.Acompanhamentos };
        private static CardapioModel Refrigerante(int id = 5) => new() { Id = id, Nome = "Refrigerante", Preco = 2.50m, Tipo = TipoItemModel.Acompanhamentos };

        // ==================== ValidarItens ====================

        [Fact]
        public void ValidarItens_SanduicheUnico_DeveRetornarNull()
        {
            var itens = new List<CardapioModel> { Sanduiche() };
            Assert.Null(PedidoService.ValidarItens(itens));
        }

        [Fact]
        public void ValidarItens_DoisSanduiches_DeveRetornarErro()
        {
            var itens = new List<CardapioModel> { Sanduiche(1), Sanduiche(2) };
            Assert.Equal("O pedido pode conter apenas um sanduíche.", PedidoService.ValidarItens(itens));
        }

        [Fact]
        public void ValidarItens_DuasBatatas_DeveRetornarErro()
        {
            var itens = new List<CardapioModel> { Sanduiche(), Batata(4), Batata(4) };
            Assert.Equal("O pedido pode conter apenas uma batata frita.", PedidoService.ValidarItens(itens));
        }

        [Fact]
        public void ValidarItens_DoisRefrigerantes_DeveRetornarErro()
        {
            var itens = new List<CardapioModel> { Sanduiche(), Refrigerante(5), Refrigerante(5) };
            Assert.Equal("O pedido pode conter apenas um refrigerante.", PedidoService.ValidarItens(itens));
        }

        [Fact]
        public void ValidarItens_PedidoCompleto_DeveRetornarNull()
        {
            var itens = new List<CardapioModel> { Sanduiche(), Batata(), Refrigerante() };
            Assert.Null(PedidoService.ValidarItens(itens));
        }

        // ==================== CalcularDesconto ====================

        [Fact]
        public void CalcularDesconto_SanduicheBatataRefrigerante_Deve_Aplicar_20_Porcento()
        {
            // 5,00 + 2,00 + 2,50 = 9,50 → 20% = 1,90
            var itens = new List<CardapioModel> { Sanduiche(), Batata(), Refrigerante() };
            var subtotal = itens.Sum(i => i.Preco);
            var desconto = PedidoService.CalcularDesconto(itens, subtotal);
            Assert.Equal(1.90m, desconto);
        }

        [Fact]
        public void CalcularDesconto_SanduicheRefrigerante_Deve_Aplicar_15_Porcento()
        {
            // 5,00 + 2,50 = 7,50 → 15% = 1,125 → arredondado: 1,12
            var itens = new List<CardapioModel> { Sanduiche(), Refrigerante() };
            var subtotal = itens.Sum(i => i.Preco);
            var desconto = PedidoService.CalcularDesconto(itens, subtotal);
            Assert.Equal(1.12m, desconto);
        }

        [Fact]
        public void CalcularDesconto_SanduicheBatata_Deve_Aplicar_10_Porcento()
        {
            // 5,00 + 2,00 = 7,00 → 10% = 0,70
            var itens = new List<CardapioModel> { Sanduiche(), Batata() };
            var subtotal = itens.Sum(i => i.Preco);
            var desconto = PedidoService.CalcularDesconto(itens, subtotal);
            Assert.Equal(0.70m, desconto);
        }

        [Fact]
        public void CalcularDesconto_ApenasAcompanhamentos_Deve_Aplicar_Sem_Desconto()
        {
            var itens = new List<CardapioModel> { Batata(), Refrigerante() };
            var subtotal = itens.Sum(i => i.Preco);
            var desconto = PedidoService.CalcularDesconto(itens, subtotal);
            Assert.Equal(0.00m, desconto);
        }

        [Fact]
        public void CalcularDesconto_ApenasSanduiche_Deve_Aplicar_Sem_Desconto()
        {
            var itens = new List<CardapioModel> { Sanduiche() };
            var subtotal = itens.Sum(i => i.Preco);
            var desconto = PedidoService.CalcularDesconto(itens, subtotal);
            Assert.Equal(0.00m, desconto);
        }
    }
}