using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class LigacaoTagBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a100','3b316e04-454b-4d06-a528-64773b63a101')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a101','3b316e04-454b-4d06-a528-64773b63a101')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a100','3b316e04-454b-4d06-a528-64773b63a106')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a101','3b316e04-454b-4d06-a528-64773b63a106')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a104','3b316e04-454b-4d06-a528-64773b27a106')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO LigacaoTag values ('2b316e04-454b-4d06-a528-64792b63a104','3b316e04-422b-4d06-a528-64773b27a106')";
            cmd.ExecuteNonQuery();
        }
    }
}