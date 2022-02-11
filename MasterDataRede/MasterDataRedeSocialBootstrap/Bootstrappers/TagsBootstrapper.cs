using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class TagsBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Tag values ('2b316e04-454b-4d06-a528-64792b63a100','isep')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Tag values ('2b316e04-454b-4d06-a528-64792b63a101','informática')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Tag values ('2b316e04-454b-4d06-a528-64792b63a102','nba')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Tag values ('2b316e04-454b-4d06-a528-64792b63a103','futebol')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Tag values ('2b316e04-454b-4d06-a528-64792b63a104','lapr5')";
            cmd.ExecuteNonQuery();
        }
    }
}