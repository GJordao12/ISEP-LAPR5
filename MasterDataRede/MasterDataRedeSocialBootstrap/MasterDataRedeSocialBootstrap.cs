using System;
using MasterDataRedeSocialBootstrap.Bootstrappers;
using MySqlConnector;

namespace MasterDataRedeSocialBootstrap
{
    class MasterDataRedeSocialBootstrap
    {
        static void Main(string[] args)
        {
            string cs = @"server=;user=root;password=;database=";

            using var con = new MySqlConnection(cs);
            con.Open();

            using var cmd = new MySqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "DELETE FROM LigacaoTag";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM PerfilTag";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM PedidoIntroducao";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM PedidoLigacao";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM Ligacao";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "DELETE FROM Perfil";
            cmd.ExecuteNonQuery();

            Console.WriteLine("[BOOTSTRAP ESTADOS DE HUMOR]");
            cmd.CommandText = "DELETE FROM EstadosDeHumor";
            cmd.ExecuteNonQuery();
            new EstadosDeHumorBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP UTILIZADORES]");
            cmd.CommandText = "DELETE FROM User";
            cmd.ExecuteNonQuery();
            new UtilizadoresBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP TAGS]");
            cmd.CommandText = "DELETE FROM Tag";
            cmd.ExecuteNonQuery();
            new TagsBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP PERFIL]");
            new PerfilBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP LIGACOES]");
            new LigacaoBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP LIGACOESTAGS]");
            new LigacaoTagBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP PERFILTAGS]");
            new PerfilTagBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP PEDIDOLIGACAO]");
            new PedidoLigacaoBootstrapper().guarda(cmd);

            Console.WriteLine("[BOOTSTRAP PEDIDOINTRODUCAO]");
            new PedidoIntroducaoBootstrapper().guarda(cmd);
        }
    }
}