using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class UtilizadoresBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a100','jordao@gmail.com','jordao','jordao123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a101','fabio@gmail.com','fabio','fabio123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a102','bruno@gmail.com','bruno','bruno123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a103','diogo@gmail.com','diogo','diogo123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a104','alejandro@gmail.com','alejandro','alejandro123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a105','tiago@gmail.com','tiago','tiago123')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO User values ('1b316e04-454b-4d06-a528-64792b63a106','joao@gmail.com','joao','joao123')";
            cmd.ExecuteNonQuery();
        }
    }
}