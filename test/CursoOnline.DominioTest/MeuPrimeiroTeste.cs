using Xunit;

namespace CursoOnline.DominioTest
{
    public class MeuPrimeiroTeste
    {
        [Fact(DisplayName = "DeveVariaveisSeremIguais")]
        public void DeveVariaveisSeremIguais()
        {
            //-- Organização
            var variavel1 = 1;
            var variavel2 = 1;

            //-- Ação
            variavel2 = variavel1;

            //-- Assert
            Assert.Equal(variavel1, variavel2);
        }
    }
}
