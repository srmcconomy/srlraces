using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Message
{

    private String prefix;
    private String command;
    private List<String> parameters;
    private String trail;
    private String user;

    public String Prefix
    {
        get { return prefix; }
        set { prefix = value; }
    }
    public String Command
    {
        get { return command; }
        set { command = value; }
    }
    public List<String> Parameters
    {
        get { return parameters; }
        set { parameters = value; }
    }
    public String Trail
    {
        get { return trail; }
        set { trail = value; }
    }
    public String User
    {
        get { return user; }
        set { user = value; }
    }

    public Message()
    {
        prefix = "";
        command = "";
        parameters = new List<string>();
        trail = "";
        user = "";
    }
    public static Message ParseString(String str)
    {
        Message msg = new Message();
        List<String> split = new List<string>(str.Split(" ".ToCharArray()));
        if (split[0].StartsWith(":"))
        {
            msg.prefix = split[0];
            if (split[0].Contains("!"))
            {
                msg.user = split[0].Substring(1, split[0].IndexOf("!") - 1);
            }
            split.RemoveAt(0);
        }
        msg.command = split[0];
        split.RemoveAt(0);
        while (split.Count > 0 && !split[0].StartsWith(":") )
        {
            if (split[0] != String.Empty)
            {
                msg.parameters.Add(split[0]);
            }
            split.RemoveAt(0);
        }
        if (split.Count > 0)
        {
            split[0] = split[0].Substring(1);
            msg.trail = String.Join(" ", split);
        }
        return msg;

    }
    public override String ToString()
    {
        return prefix + " " + command + " " + String.Join(" ", parameters) + " " + trail;
    }

}

