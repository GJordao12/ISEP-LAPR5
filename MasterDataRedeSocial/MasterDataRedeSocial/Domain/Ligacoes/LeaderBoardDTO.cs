namespace DDDSample1.Domain.Ligacoes
{
    public class LeaderBoardDTO
    {
        public int valor { get; set; }
        public string username { get; set; }
        public int posicao { get; set; }

        public LeaderBoardDTO(string username, int valor, int posicao)
        {
            this.username = username;
            this.valor = valor;
            this.posicao = posicao;
        }
    }
}