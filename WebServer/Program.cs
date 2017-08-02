using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (HttpListener hl = new HttpListener())
            {
                hl.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
                hl.Prefixes.Add("http://+:12/");
                hl.Start();
                Console.WriteLine($"创建成功，连接地址:http://{Dns.GetHostByName(Dns.GetHostName()).AddressList[0]}:12/");
                while (true)
                {
                    HttpListenerContext hlc = hl.GetContext();
                    hlc.Response.StatusCode = 200;
                    hlc.Response.ContentType = "text/html; charset=UTF-8";
                    var data = hlc.Request.QueryString["id"];
                    using (StreamWriter writer = new StreamWriter(hlc.Response.OutputStream, Encoding.UTF8))
                    {
                        if (data == "star")
                            writer.WriteLine(Data.star);
                        else if (data == "yuan")
                            writer.WriteLine(Data.yuan);
                        else if (data == "code")
                            writer.WriteLine(Data.code);
                        else writer.WriteLine(Data.star);
                        writer.Close();
                        hlc.Response.Close();
                    }
                }
            }
        }
    }
}
