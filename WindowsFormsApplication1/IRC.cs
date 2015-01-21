using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WindowsFormsApplication1;

public class IRC
{
    List<String> ss = new List<string>();
    Socket s = null;


    
	public IRC()
	{
    }
    public bool Connect() {
        IPHostEntry hostEntry = null;

        hostEntry = Dns.GetHostEntry("irc2.speedrunslive.com");


        foreach (IPAddress address in hostEntry.AddressList)
        {
            IPEndPoint ipe = new IPEndPoint(address, 6667);
            Socket temp = new Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Unspecified);
            temp.Connect(ipe);

            if (temp.Connected)
            {
                s = temp;
                break;
            }           
        }
        return s.Connected;
    }

    public void Login(string nick)
    {
        Send("NICK " + nick + "\r\n");
        Send("User " + nick + " " + nick + " " + nick + " :" + nick + "\r\n");
    }
    
    public Message GetLine() {
        String line;

        if (ss.Count > 1)
        {
            line = ss[0];
            ss.RemoveAt(0);
            ss.RemoveAt(0);
            if (line.Length >= 6)
            {
                if (line.Substring(0, 5) == "PING ")
                {
                    char[] temp = (line + "\r\n").ToCharArray();
                    temp[1] = 'O';
                    s.Send(Encoding.UTF8.GetBytes(temp));
                }
            }
            return Message.ParseString(line);
        }

        int numbytes;
        String stream = "";
        do
        {
            Byte[] buf = new Byte[512];
            numbytes = s.Receive(buf, 512, 0);
            stream += Encoding.UTF8.GetString(buf, 0, numbytes);
        } while (numbytes == 512);
        List<String> streamsplit = new List<String>(stream.Split("\r\n".ToCharArray()));
        streamsplit.RemoveAt(streamsplit.Count - 1);
        /*if (ss.Count > 0)
        {
            ss[0] += streamsplit[0];
            streamsplit.RemoveAt(0);            
        }*/
        ss.AddRange(streamsplit);
        return GetLine();

    }
    public void Send(String str)
    {
        s.Send(Encoding.UTF8.GetBytes(str));
    }
       
}
