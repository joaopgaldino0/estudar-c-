using System;
namespace TerceiraApi.Models;

public class Livro
{
    public Livro()
    {
        CriadoEm = DateTime.Now;
        Id = Guid.NewGuid().ToString();
    }

    //Atributo, propriedade e característica - C#
    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? Nome { get; set; }
    public string? Autor { get; set; }
    public double Preco { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.Now;
    public bool Emprestado { get; set; } = false;
    public int TotalEmprestimos { get; set; } = 0;

    //Atributo, propriedade e característica - Java
    // private string nome;
    // public string getNome()
    // {
    //     return this.nome;
    // }
    // public void setNome(string nome)
    // {
    //     this.nome = nome;
    //
}