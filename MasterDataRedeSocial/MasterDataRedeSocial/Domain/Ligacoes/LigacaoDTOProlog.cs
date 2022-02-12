using DDDSample1.Domain.Utilizador;

namespace DDDSample1.Domain.Ligacoes
{
    public class LigacaoDTOProlog
    {
        public string principal { get; set; }
        
        public string secundario { get; set; }
        
        public int forcaLigacaoPrincipal { get; set; }
        
        public int forcaLigacaoSecundario { get; set; }
        
        public int forcaRelacao { get; set; }

        public LigacaoDTOProlog(string principal, string secundario, int forcaLigacaoPrincipal, int forcaLigacaoSecundario,int forcaRelacao)
        {
            this.principal = principal;
            this.secundario = secundario;
            this.forcaLigacaoPrincipal = forcaLigacaoPrincipal;
            this.forcaLigacaoSecundario = forcaLigacaoSecundario;
            this.forcaRelacao = forcaRelacao;
        }
        
    }
}