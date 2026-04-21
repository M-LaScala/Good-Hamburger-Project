namespace Good_Hamburger.Model.Cardapio
{
    public class CardapioModel
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public TipoItemModel Tipo { get; set; }
    }
}
