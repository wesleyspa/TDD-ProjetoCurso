using System;

namespace CursoOnline.Dominio.Cursos
{
    public class Curso
    {
        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvoEnum publicoAlvo, decimal valor)
        {
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentException("Nome inválido!");

            if (string.IsNullOrEmpty(descricao))
                throw new ArgumentException("Descrição inválida!");

            if (cargaHoraria < 1)
                throw new ArgumentException("Carga horária inválida!");

            if (valor < 1)
                throw new ArgumentException("Valor inválido!");

            Nome = nome;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
        }

        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvoEnum PublicoAlvo { get; private set; }
        public decimal Valor { get; private set; }
    }
}