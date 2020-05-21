using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpSever
{
    class Program
    {
        public static string SeverIP = "169.254.120.3";
        public static int SeverPort = 8899;
        static int Main(string[] args)
        {
            /*1、创建Socket*/
            Socket SeverSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
            /*2、设置Point属性*/
            if (!IPAddress.TryParse(SeverIP, out IPAddress SeverIPAddress))
            {
                Console.WriteLine("IP地址解析错误！");
                return 1;
            }
            IPEndPoint SeverPoint = new IPEndPoint(SeverIPAddress, SeverPort);
            /*3、绑定Point到Socket上*/
            SeverSocket.Bind(SeverPoint);
            /*4、开启监听*/
            SeverSocket.Listen(0);
            /*5、开启监听*/
            Socket ClientSocket = SeverSocket.Accept();
            IPEndPoint ClientPotin = ClientSocket.RemoteEndPoint as IPEndPoint;
            Console.WriteLine("客户端：" + ClientPotin.Address + "已连接");
          
            

            return 0;
            
        }
    }
}
