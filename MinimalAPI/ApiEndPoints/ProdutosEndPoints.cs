using Microsoft.EntityFrameworkCore;
using MinimalAPI.Context;
using MinimalAPI.Models;

namespace MinimalAPI.ApiEndPoints
{
    public static class ProdutosEndPoints
    {
        public static void MapProdutosEndPoints(this WebApplication app)
        {
            app.MapPost("/produtos", async (Produto produto, AppDbContext db) =>
            {
                db.Produtos.Add(produto);
                db.SaveChanges();
                return Results.Created($"/produtos{produto.ProdutoId}", produto);
            });

            app.MapGet("/produtos", async (AppDbContext db) =>

                    await db.Produtos.ToListAsync());


            app.MapGet("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Produtos.FindAsync(id)
                is Produto produto
                ? Results.Ok(produto)
                : Results.NotFound();

            });

            app.MapPut("/produtos/{id:int}", async (int id, AppDbContext db, Produto produto) =>
            {
                if (produto.ProdutoId != id)
                {
                    Results.BadRequest();
                }
                var produtoDoDB = await db.Produtos.FindAsync(id);
                if (produtoDoDB is null) return Results.NotFound();

                produtoDoDB.Nome = produto.Nome;
                produtoDoDB.Descricao = produto.Descricao;
                produtoDoDB.Imagem = produto.Imagem;
                produtoDoDB.Preco = produto.Preco;
                produtoDoDB.Estoque = produto.Estoque;
                produtoDoDB.CategoriaId = produto.CategoriaId;


                await db.SaveChangesAsync();
                return Results.Ok(produtoDoDB);
            });

            app.MapDelete("/produtos/{id:int}", async (int id, AppDbContext db) =>
            {
                var produtoDELL = await db.Produtos.FindAsync(id);

                if (produtoDELL is null)
                {
                    Results.NotFound();
                }

                db.Produtos.Remove(produtoDELL);

                await db.SaveChangesAsync();

                return Results.Ok(produtoDELL);
            });
        }
    }
}
