using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
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
            /*5、接收连接*/
            while (true)
            {
                Socket ClientSocket = SeverSocket.Accept();
                Thread ClientRWThead = new Thread(TcpWR);
                ClientRWThead.Start(ClientSocket);
                IPEndPoint iPEndPoint = ClientSocket.RemoteEndPoint as IPEndPoint;
                Console.WriteLine(iPEndPoint.Address + "已连接");
            }
            
                     
           
            return 0;
            
        }

        /*TCP收发函数*/
        public static void TcpWR(object socket)
        {
            Socket clientSocket = socket as Socket;
            byte[] ReadBuff = new byte[1024];
            int ReciveNumber;
            while (true)
            {
                /*6、接收数据*/
                ReciveNumber = clientSocket.Receive(ReadBuff);
                /*8、断开连接*/
                if (ReciveNumber == 0)
                {
                    IPEndPoint iPEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
                    Console.WriteLine(iPEndPoint.Address + "已断开");
                    return ;
                }
                /*7、发送数据*/
                clientSocket.Send(ReadBuff,ReciveNumber,0);
                
            }
        }
    }

  
}
