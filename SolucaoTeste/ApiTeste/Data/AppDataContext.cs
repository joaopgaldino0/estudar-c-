using System;
using ApiTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiTeste.Data;

//Representar o banco de dados da sua aplicação
//1 - Criar a herança da classe DbContext
//2 - Criar os atributos para representar as tabelas
//no banco de dados
//3 - Configurar o banco de dados e a string de conexão
public class AppDataContext : DbContext
{
    //Classes de modelo que vão representar tabela no banco
    public DbSet<Livro> Livros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Banco.db");
    }

}