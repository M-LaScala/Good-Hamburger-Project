using Good_Hamburger.Model.Cardapio;

namespace Good_Hamburger.Services
{
    public class PedidoService
    {
        // Valida itens duplicados e quantidade máxima por tipo
        public static string? ValidarItens(List<CardapioModel> itens)
        {
            var sanduiches = itens.Where(i => i.Tipo == TipoItemModel.Sanduíches).ToList();
            var acompanhamentos = itens.Where(i => i.Tipo == TipoItemModel.Acompanhamentos).ToList();

            if (sanduiches.Count > 1)
            {
                return "O pedido pode conter apenas um sanduíche.";
            }

            // Verifica batatas e refrigerantes duplicados pelo nome
            var batatas = acompanhamentos.Where(i => i.Nome.Contains("Batata", StringComparison.OrdinalIgnoreCase)).ToList();
            var refrigerantes = acompanhamentos.Where(i => i.Nome.Contains("Refrigerante", StringComparison.OrdinalIgnoreCase)).ToList();

            if (batatas.Count > 1)
            {
                return "O pedido pode conter apenas uma batata frita.";
            }


            if (refrigerantes.Count > 1)
            {
                return "O pedido pode conter apenas um refrigerante.";
            }

            return null;
        }

        // Aplica as regras de desconto
        public static decimal CalcularDesconto(List<CardapioModel> itens, decimal subtotal)
        {
            bool temSanduiche = itens.Any(i => i.Tipo == TipoItemModel.Sanduíches);
            bool temBatata = itens.Any(i => i.Nome.Contains("Batata", StringComparison.OrdinalIgnoreCase));
            bool temRefrigerante = itens.Any(i => i.Nome.Contains("Refrigerante", StringComparison.OrdinalIgnoreCase));

            decimal percentual = 0m;

            if (temSanduiche && temBatata && temRefrigerante)
            {
                percentual = 0.20m;
            }
            else if (temSanduiche && temRefrigerante)
            {
                percentual = 0.15m;
            }
            else if (temSanduiche && temBatata)
            {
                percentual = 0.10m;
            }

            return Math.Round(subtotal * percentual, 2);
        }
    }
}