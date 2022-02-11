using MySqlConnector;

namespace MasterDataRedeSocialBootstrap.Bootstrappers
{
    public class PerfilBootstrapper
    {
        public void guarda(MySqlCommand cmd)
        {
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a100','1b316e04-454b-4d06-a528-64792b63a100','0b316e04-454b-4d06-a528-64792b63a100','2001-08-07','https://www.facebook.com/GJordao12/','https://www.linkedin.com/in/gonçalo-jordão-7a1535121/','Gonçalo Jordão','+351931111111',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a101','1b316e04-454b-4d06-a528-64792b63a101','0b316e04-454b-4d06-a528-64792b63a105','1998-02-06','https://www.facebook.com/fabio.fernandes.1826','https://www.linkedin.com/in/fabioalexandrefernandes/','Fábio Fernandes','+351932222222',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a102','1b316e04-454b-4d06-a528-64792b63a102','0b316e04-454b-4d06-a528-64792b63a102','2001-04-01', 'https://www.facebook.com/brunogabrielfloressilva','','Bruno Silva','+351933333333',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a103','1b316e04-454b-4d06-a528-64792b63a103','0b316e04-454b-4d06-a528-64792b63a107','2001-12-13','https://www.facebook.com/profile.php?id=100006451487811','','Diogo Domingues','+351934444444',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a104','1b316e04-454b-4d06-a528-64792b63a104','0b316e04-454b-4d06-a528-64792b63a108','2000-05-03','https://www.facebook.com/profile.php?id=100006408487811','', 'Alejandro','+351935555555',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a105','1b316e04-454b-4d06-a528-64792b63a105','0b316e04-454b-4d06-a528-64792b63a109','2001-05-03','https://www.facebook.com/profile.php?id=100005408487811','', 'Tiago','+351935555556',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "INSERT INTO Perfil values ('3b316e04-454b-4d06-a528-64792b63a106','1b316e04-454b-4d06-a528-64792b63a106','0b316e04-454b-4d06-a528-64792b63a109','2002-05-03','https://www.facebook.com/profile.php?id=100007408487811','', 'Joao','+351935555565',0.3,0.4,0.5,0.3,0.2,0.1,0.4,0.3,0.6,0.9)";
            cmd.ExecuteNonQuery();
        }
    }
}