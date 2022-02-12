namespace DDDSample1.Domain.PedidoIntroducao
{
    public class PedidoIntroducaoPutDTO
    {
        public string userNameRem { get; set; }
        
        public string userNameInt { get; set; }
        
        public string userNameDest { get; set; }

        public string apresentacao { get; set; }
        
        public string apresentacaoLigacao { get; set; }

        public PedidoIntroducaoPutDTO()
        {
            
        }

        public PedidoIntroducaoPutDTO(string userNameRem, string userNameInt, string userNameDest, string apresentacao,string apresentacaoLigacao)
        {
            this.userNameRem = userNameRem;
            this.userNameInt = userNameInt;
            this.userNameDest = userNameDest;
            this.apresentacao = apresentacao;
            this.apresentacaoLigacao = apresentacaoLigacao;
        }
    }
}