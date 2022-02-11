using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class EstadosDeHumorBootstrapper
    { 
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a100','Joy')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a101','Distress')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a102','Hope')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a103','Fear')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a104','Relief')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a105','Disappointment')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a106','Pride')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a107','Remorse')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a108','Gratitude')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO EstadosDeHumor values ('0b316e04-454b-4d06-a528-64792b63a109','Anger')";
            cmd.ExecuteNonQuery();
        }
    }
}