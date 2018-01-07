namespace PacmanClientWPF.Util
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Net;
   using System.Net.Sockets;
   using System.Text;
   using System.Threading.Tasks;
   using PacManLib;

   public static class PmNet
   {
      public static bool IsTcp { get; private set; } 
      public static int ClientId { get; private set; }
      public static NetworkStream Stream {get; private set; }
      public static InitialNetPackage Pack { get; set; }
      public static bool[,] CoinArray { get; set; }
      public static void NetInit(string ipString, string portString)
      {
         try
         {
            int port = Convert.ToInt32(portString);
            TcpClient client = Networking.EstablishConnection(ipString, port);
            Stream = client.GetStream();

            Pack = Networking.RecieveInitialPackage(Stream);
            ClientId = Pack.Index;
            IsTcp = true;
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }
   }
}
