using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class PerfilTagBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a100','3b316e04-454b-4d06-a528-64792b63a100')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a100','3b316e04-454b-4d06-a528-64792b63a101')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a104','3b316e04-454b-4d06-a528-64792b63a102')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a104','3b316e04-454b-4d06-a528-64792b63a103')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a102','3b316e04-454b-4d06-a528-64792b63a105')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO PerfilTag values ('2b316e04-454b-4d06-a528-64792b63a102','3b316e04-454b-4d06-a528-64792b63a106')";
            cmd.ExecuteNonQuery();
        }
    }
}