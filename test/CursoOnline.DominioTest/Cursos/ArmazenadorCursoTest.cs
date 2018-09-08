using CursoOnline.Dominio.Cursos;
using Moq;
using Xunit;

namespace CursoOnline.DominioTest.Cursos
{
    public class ArmazenadorCursoTest
    {
        public ArmazenadorCursoTest()
        {
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDto = new CursoDto
            {
                Nome = "Curso A",
                Descricao = "Descrição",
                CargaHoraria = 80,
                PublicoAlvo = PublicoAlvoEnum.Empreendedor,
                Valor = 850
            };

            var cursoRepositorioMock = new Mock<ICursoRepositorio>();
            var armazenadorCurso = new ArmazenadorCurso(cursoRepositorioMock.Object);

            armazenadorCurso.Armazenar(cursoDto);

            cursoRepositorioMock.Verify(x => x.Adicionar(It.IsAny<Curso>()));
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
        public int CargaHoraria { get; set; }
        public PublicoAlvoEnum PublicoAlvo { get; set; }
        public decimal Valor { get; set; }
    }
}
