//CONTRUIR A APLICAÇÃO BASE
using TerceiraApi.Data;
using TerceiraApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Adicionar o contexto na lista de serviços da aplicação
builder.Services.AddDbContext<AppDataContext>();

builder.Services.AddCors(options =>
    options.AddPolicy("Acesso Total", 
        configs => configs
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod())
);

var app = builder.Build();

List<Livro> livros = new List<Livro>();

//Endpoints - ADICIONAR FUNCIONALIDADES NA APLICAÇÃO
//Requisição
// - URL
// - Método HTTP
// - Parâmetros/Informações/Dados (Opcional)
//  - Listar/buscar (Retrive) dados: Método HTTP GET
//  - Cadastrar (Create) dados: Método HTTP POST

//Resposta
// - Código de Status HTTP
// - Informações/Dados (Opcional)
app.MapGet("/", () => "API de Livros!");

//GET: /api/produto/listar
app.MapGet("/apiteste/livros/listar",
    ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Livros.Any())
    {
        //Configurar a resposta da requisição
        return Results.Ok(ctx.Livros.ToList());
    }
    return Results.NotFound("Lista de livros vazia!");
});

//POST: /api/produto/cadastrar
app.MapPost("/apiteste/livros/cadastrar",
    ([FromBody] Livro livro,
    [FromServices] AppDataContext ctx) =>
{
    //Não permitir o cadastro de um produto com o mesmo nome
    //Expressão lambda
    Livro? resultado = ctx.Livros.FirstOrDefault
        (x => x.Nome == livro.Nome);

    if (resultado is not null)
    {
        return Results.Conflict("Esse produto já existe!");
    }

    ctx.Livros.Add(livro);
    ctx.SaveChanges();
    return Results.Created("", livro);
});

app.MapDelete("/apiteste/livros/deletar/{id}",
    ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{

    Livro? resultado = ctx.Livros.Find(id);

    if (resultado is not null)
    {
        ctx.Livros.Remove(resultado);
        ctx.SaveChanges();
        return Results.NoContent();
    }

    return Results.NotFound("Produto não encontrado!");
});

app.MapPut("/apiteste/livros/emprestar/{id}",
    ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Livro? resultado = ctx.Livros.Find(id);

    if (resultado is null)
    {
        return Results.NotFound("Livro não encontrado!");
    }

    if (resultado.Emprestado)
    {
        return Results.BadRequest("Livro já está emprestado!");
    }

    resultado.Emprestado = true;
    resultado.TotalEmprestimos++;
    ctx.SaveChanges();

    return Results.Ok("Livro emprestado com sucesso." );
});

app.MapPut("/apiteste/livros/devolver/{id}",
    ([FromRoute] string id,
    [FromServices] AppDataContext ctx) =>
{
    Livro? resultado = ctx.Livros.Find(id);

    if (resultado is null)
    {
        return Results.NotFound("Livro não encontrado!");
    }

    if (!resultado.Emprestado)
    {
        return Results.BadRequest("Livro não está emprestado!");
    }

    resultado.Emprestado = false;
    ctx.SaveChanges();

    return Results.Ok("Livro devolvido com sucesso.");
});

app.MapGet("/apiteste/livros/disponiveis",
    ([FromServices] AppDataContext ctx) =>
{
    List<Livro> resultado = ctx.Livros
        .Where(l => l.Emprestado == false)
        .ToList();

    if (resultado.Count == 0)
    {
        return Results.NotFound("Nenhum livro disponível!");
    }

    return Results.Ok(resultado);
});

app.MapGet("/apiteste/livros/emprestados",
    ([FromServices] AppDataContext ctx) =>
{
    List<Livro> resultado = ctx.Livros
        .Where(l => l.Emprestado == true)
        .ToList();

    if (resultado.Count == 0)
    {
        return Results.NotFound("Nenhum livro emprestado!");
    }

    return Results.Ok(resultado);
});

app.UseCors("Acesso Total");

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDataContext>();
    db.Database.Migrate(); // aplica migrations automaticamente
}

app.MapGet("/apiteste/livros/maisemprestado",
    ([FromServices] AppDataContext ctx) =>
{
    Livro? resultado = ctx.Livros
        .OrderByDescending(x => x.TotalEmprestimos)
        .FirstOrDefault();

    if (resultado is null)
    {
        return Results.NotFound("Nenhum livro cadastrado!");
    }

    return Results.Ok(resultado);
});

//RODAR A APLICAÇÃO
app.Run();