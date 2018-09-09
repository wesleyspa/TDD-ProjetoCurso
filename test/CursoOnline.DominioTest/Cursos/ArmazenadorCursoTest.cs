using System;
using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.DominioTest._Builders;
using CursoOnline.DominioTest._Utils;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorCursoTest
    {
        private CursoDto _cursoDto;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorCurso _armazenadorCurso;

        public ArmazenadorCursoTest()
        {
            var fake = new Faker();

            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Word(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(10, 100),
                PublicoAlvo = "Empreendedor",
                Valor = fake.Random.Decimal(100, 1000)
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorCurso = new ArmazenadorCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(x => x.Adicionar(It.Is<Curso>(y =>
                                                                      y.Nome == _cursoDto.Nome
                                                                      && y.Descricao == _cursoDto.Descricao
                                                                      && Equals(y.CargaHoraria, _cursoDto.CargaHoraria)))
                                                                      , Times.AtMostOnce);
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDto.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem("Publico Alvo inválido!");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComNomeJaExistente()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDto)).ComMensagem("Nome do curso já consta no banco de dados!");
        }
    }
}
