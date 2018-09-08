using Bogus;
using CursoOnline.Dominio.Cursos;
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
                PublicoAlvo = PublicoAlvoEnum.Empreendedor,
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
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
        void Atualizar(Curso curso);
    }

    public class ArmazenadorCurso
    {
        private ICursoRepositorio _cursoRepositorio;

        public ArmazenadorCurso(ICursoRepositorio cursoRepositorio)
        {
            this._cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var curso = new Curso(cursoDto.Nome, cursoDto.Descricao, cursoDto.CargaHoraria, cursoDto.PublicoAlvo, cursoDto.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public PublicoAlvoEnum PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}
