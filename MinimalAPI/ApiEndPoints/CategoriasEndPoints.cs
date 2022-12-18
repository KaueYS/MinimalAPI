using Microsoft.EntityFrameworkCore;
using MinimalAPI.Context;
using MinimalAPI.Models;

namespace MinimalAPI.ApiEndPoints
{
    public static class CategoriasEndPoints
    {
        public static void MapCategoriaEndPoints(this WebApplication app)
        {
            app.MapGet("/categorias", async (AppDbContext db) =>

    await db.Categorias.ToListAsync());


            //gerar Lista do banco de dados pelo ID (GET - ID)
            app.MapGet("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                return await db.Categorias.FindAsync(id)
                is Categoria categoria
                ? Results.Ok(categoria)
                : Results.NotFound();
            });


            //Adicionar informacoes ao Banco de dados
            app.MapPost("/categorias", async (Categoria cat, AppDbContext db)
                =>
            {
                db.Categorias.Add(cat);
                db.SaveChanges();
                return Results.Created($"/categorias{cat.CategoriaId}", cat);
            });


            //Alterar (PUT) dados do banco de dados pelo ID
            app.MapPut("/categoria/{id:int}", async (int id, Categoria categoria, AppDbContext db) =>
            {
                if (categoria.CategoriaId != id)
                {
                    Results.BadRequest();
                }

                var cat = await db.Categorias.FindAsync(id);
                if (cat is null) Results.NotFound();
                cat.Nome = categoria.Nome;
                cat.Descricao = categoria.Descricao;

                await db.SaveChangesAsync();
                return Results.Ok(cat);
            });

            //Deletar informacoes do banco de dados pelo ID

            app.MapDelete("/categorias/{id:int}", async (int id, AppDbContext db) =>
            {
                var categoria = await db.Categorias.FindAsync(id);

                if (categoria is null)
                {
                    Results.NotFound();
                }

                db.Categorias.Remove(categoria);

                await db.SaveChangesAsync();

                return Results.Ok(categoria);
            });



        }
    }
}
