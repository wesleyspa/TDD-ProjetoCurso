using CursoOnline.Dominio.Cursos;

namespace CursoOnline.DominioTest._Builders
{
    public class CursoBuilder
    {
        string _nome = "nformática básica";
        double _cargaHoraria = 80;
        PublicoAlvoEnum _publicoAlvo = PublicoAlvoEnum.Estudante;
        decimal _valor = 950;
        string _descricao = "Descrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public Curso Build()
        {
            return new Curso(_nome, _descricao, _cargaHoraria, _publicoAlvo, _valor);
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComDescricao(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComPublicoAlvo(PublicoAlvoEnum publicoAlvo)
        {
            _publicoAlvo = publicoAlvo;
            return this;
        }

        public CursoBuilder ComValor(decimal valor)
        {
            _valor = valor;
            return this;
        }
    }
}
