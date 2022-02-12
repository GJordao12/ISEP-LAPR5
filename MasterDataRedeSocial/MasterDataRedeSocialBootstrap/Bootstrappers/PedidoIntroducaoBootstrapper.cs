using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class PedidoIntroducaoBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO PedidoIntroducao values ('7b316e04-454b-4d06-a528-64792b63a200','PENDENTE','1b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a102','Espero que aprove','Espero que aceite')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PedidoIntroducao values ('7b316e04-454b-4d06-a528-64792b63a201','PENDENTE','1b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a100','Gostaria que aprovasse','Gostaria que aceitasse')";
            cmd.ExecuteNonQuery();
        }
    }
}