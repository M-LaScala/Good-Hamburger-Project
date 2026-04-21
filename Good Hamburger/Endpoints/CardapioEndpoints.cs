using Good_Hamburger.Data;
using Microsoft.EntityFrameworkCore;

namespace Good_Hamburger.Endpoints
{
    public static class CardapioEndpoints
    {
        public static void MapCardapioEndpoints(this WebApplication app)
        {
            app.MapGet("/cardapio", async (AppDbContext db) =>
                await db.Cardapio.ToListAsync());
        }
    }
}