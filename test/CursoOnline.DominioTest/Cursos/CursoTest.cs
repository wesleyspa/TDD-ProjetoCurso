using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Utils;
using ExpectedObjects;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        readonly ITestOutputHelper _testOutputHelper;
        readonly string _nome;
        readonly double _cargaHoraria;
        readonly PublicoAlvoEnum _publicoAlvo;
        readonly decimal _valor;
        readonly string _descricao;

        public CursoTest(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
            _testOutputHelper.WriteLine("Construtor sendo executado");
            var faker = new Faker();

            _nome = faker.Random.Words(2);
            _cargaHoraria = faker.Random.Double(10, 100);
            _publicoAlvo = PublicoAlvoEnum.Estudante;
            _valor = faker.Finance.Amount(100, 1000);
            _descricao = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            _testOutputHelper.WriteLine("Dispose sendo executado!");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeCurso)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComNome(nomeCurso).Build())
                  .ComMensagem("Nome inválido!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void NaoDeveCursoTerCargaHorariaMenor1(double cargaHoraria)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComCargaHoraria(cargaHoraria).Build())
                  .ComMensagem("Carga horária inválida!");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-10)]
        [InlineData(-100)]
        public void NaoDeveCursoTerValorMenor1(decimal valor)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComValor(valor).Build())
                  .ComMensagem("Valor inválido!");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void DeveCursoTerUmaDescricao(string descricao)
        {
            Assert.Throws<ArgumentException>(() => CursoBuilder.Novo().ComDescricao(descricao).Build())
                  .ComMensagem("Descrição inválida!");
        }
    }
}
