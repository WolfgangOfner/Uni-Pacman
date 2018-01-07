using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PacManLib;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Pacman_Web.Controllers
{
    public class DirectionController : Controller
    {
        public MemoryStream ms = new MemoryStream();
        public BinaryFormatter formatter = new BinaryFormatter();
        public NetworkStream stream = GetNetworkStream();

        public ActionResult Index()
        {
            return View();
        }

        public static NetworkStream GetNetworkStream()
        {
            try
            {
                return ConnectionController.client.GetStream();
            }
            catch
            {
                return null;
            }
        }

        public void SendPosition(string direction)
        {
            if (ConnectionController.client.Connected)
            {
                formatter.Serialize(ms, direction);
                byte[] data = ms.ToArray();

                int dataLength = data.Length;
                byte[] length = BitConverter.GetBytes(dataLength);

                byte[] fullData = new byte[sizeof(int) + dataLength];

                Array.Copy(length, 0, fullData, 0, length.Length);
                Array.Copy(data, 0, fullData, length.Length, data.Length);

                try
                {
                    stream.Write(fullData, 0, fullData.Length);
                    stream.Flush();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    RedirectToAction("ConnectionFailed", "Connection");
                }

                ms.Close();
            }
            else
            {
                RedirectToAction("ConnectionFailed", "Connection");
            }
        }
	}
}