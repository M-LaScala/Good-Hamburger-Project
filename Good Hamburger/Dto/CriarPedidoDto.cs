namespace Good_Hamburger.Dto
{
    public class CriarPedidoDto
    {
        public string Nome { get; set; } = string.Empty;
        public List<int> ItensIds { get; set; } = [];
    }
}
