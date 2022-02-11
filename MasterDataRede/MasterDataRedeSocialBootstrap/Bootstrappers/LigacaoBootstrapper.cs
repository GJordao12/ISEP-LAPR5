using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class LigacaoBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b63a101','1b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a101','10','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b63a106','1b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a100','20','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b27a106','1b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a102','12','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a106','1b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a101','72','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b63a102','1b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a103','30','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b63a103','1b316e04-454b-4d06-a528-64792b63a103','1b316e04-454b-4d06-a528-64792b63a102','40','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-454b-4d06-a528-64773b27a104','1b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a104','10','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a105','1b316e04-454b-4d06-a528-64792b63a104','1b316e04-454b-4d06-a528-64792b63a100','50','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a109','1b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a105','50','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a110','1b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a106','50','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a111','1b316e04-454b-4d06-a528-64792b63a106','1b316e04-454b-4d06-a528-64792b63a100','50','0')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Ligacao values ('3b316e04-422b-4d06-a528-64773b27a112','1b316e04-454b-4d06-a528-64792b63a105','1b316e04-454b-4d06-a528-64792b63a100','50','0')";
            cmd.ExecuteNonQuery();
        }
    }
}