using Good_Hamburger.Model.Cardapio;

namespace Good_Hamburger.Model.Pedidos
{
    public class PedidosModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public List<CardapioModel> Itens { get; set; } = [];
        public decimal Subtotal { get; set; }
        public decimal Desconto { get; set; }
        public decimal Total { get; set; }
        public DateTime DataPedido { get; private set; } = DateTime.Now;
    }
}