using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class PedidoLigacaoBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO PedidoLigacao values ('6b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a104','1b316e04-454b-4d06-a528-64792b63a103','Pendente','Aceita,pls')";
            cmd.ExecuteNonQuery();
        }
    }
}