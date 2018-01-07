using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Sockets;
using PacManLib;

namespace Pacman_Web.Controllers
{
    public class ConnectionController : Controller
    {
        public static TcpClient client;

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CheckConnection(FormCollection fc)
        {
            try
            {
                foreach (string item in fc)
                {
                    ViewData[item] = fc[item];
                }

                var ip = (string)ViewData["ip"];
                var port = Convert.ToInt32((string)ViewData["port"]);

                client = Networking.EstablishConnection(ip, port);

                if (!client.Connected)
                {
                    // We are not connected
                    return RedirectToAction("ConnectionFailed");
                }
            }
            catch
            {
                return RedirectToAction("ConnectionFailed");
            }

            return RedirectToAction("Index", "Direction");
        }

        public ActionResult Disconnect()
        {
            if (client.Connected)
            {
                client.GetStream().Close();
                client.Close();

                return View("Index");
            }

            return RedirectToAction("ConnectionFailed");
        }

        public ActionResult ConnectionFailed()
        {
            return View();
        }
    }
}