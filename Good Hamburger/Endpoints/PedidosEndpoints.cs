using Good_Hamburger.Data;
using Good_Hamburger.Dto;
using Good_Hamburger.Services;
using Good_Hamburger.Model.Pedidos;
using Microsoft.EntityFrameworkCore;

namespace Good_Hamburger.Endpoints
{
    public static class PedidosEndpoints
    {
        public static void MapPedidosEndpoints(this WebApplication app)
        {
            // Listar todos os pedidos
            app.MapGet("/pedidos", async (AppDbContext db) =>
            {
                return Results.Ok(await db.Pedidos.Include(p => p.Itens).ToListAsync());
            });

            // Buscar pedido por ID
            app.MapGet("/pedidos/{id}", async (int id, AppDbContext db) =>
            {
                var pedido = await db.Pedidos.Include(p => p.Itens)
                                             .FirstOrDefaultAsync(p => p.Id == id);
                return pedido is null ? Results.NotFound() : Results.Ok(pedido);
            });

            // Criar novo pedido
            app.MapPost("/pedidos", async (CriarPedidoDto dto, AppDbContext db) =>
            {
                var itensDb = await db.Cardapio.Where(c => dto.ItensIds.Contains(c.Id)).ToListAsync();

                if (itensDb.Count == 0)
                {
                    return Results.BadRequest("Nenhum item válido encontrado.");
                }
                    
                var validacao = PedidoService.ValidarItens(itensDb);
                if (validacao is not null)
                {
                    return Results.BadRequest(validacao);
                }

                var pedido = new PedidosModel
                {
                    Nome = dto.Nome,
                    Itens = itensDb,
                    Subtotal = itensDb.Sum(i => i.Preco)
                };
                pedido.Desconto = PedidoService.CalcularDesconto(itensDb, pedido.Subtotal);
                pedido.Total = pedido.Subtotal - pedido.Desconto;

                db.Pedidos.Add(pedido);
                await db.SaveChangesAsync();

                return Results.Created($"/pedidos/{pedido.Id}", pedido);
            });

            // Atualizar pedido
            app.MapPut("/pedidos/{id}", async (int id, CriarPedidoDto dto, AppDbContext db) =>
            {
                var pedido = await db.Pedidos.Include(p => p.Itens)
                                             .FirstOrDefaultAsync(p => p.Id == id);

                if (pedido is null) 
                {
                    return Results.NotFound();
                }

                var itensDb = await db.Cardapio.Where(c => dto.ItensIds.Contains(c.Id)).ToListAsync();

                if (itensDb.Count == 0)
                {
                    return Results.BadRequest("Nenhum item válido encontrado.");
                }

                var validacao = PedidoService.ValidarItens(itensDb);
                if (validacao is not null)
                {
                    return Results.BadRequest(validacao);
                }

                pedido.Nome = dto.Nome;
                pedido.Itens = itensDb;
                pedido.Subtotal = itensDb.Sum(i => i.Preco);
                pedido.Desconto = PedidoService.CalcularDesconto(itensDb, pedido.Subtotal);
                pedido.Total = pedido.Subtotal - pedido.Desconto;

                await db.SaveChangesAsync();
                return Results.Ok(pedido);
            });

            // Deletar pedido
            app.MapDelete("/pedidos/{id}", async (int id, AppDbContext db) =>
            {
                var pedido = await db.Pedidos.FindAsync(id);
                if (pedido is null)
                {
                    return Results.NotFound();
                }

                db.Pedidos.Remove(pedido);
                await db.SaveChangesAsync();
                return Results.NoContent();
            });
        }
    }
}