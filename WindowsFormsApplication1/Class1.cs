using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class Entrant
{
    public String displayname;
    public int place;
    public int time;
    public String message;
    public String statetext;
    public String twitch;
    public int trueskill;

}

public class Game
{
    public int id;
    public String name;
    public String abbrev;
    public float popularity;
    public int popularityrank;
}

public class Race
{
    public static Race LoadRace(string id)
    {
        string s = WebData.RetrieveData("http://api.speedrunslive.com/races?id=" + id);
        return JsonConvert.DeserializeObject<Race>(s);
    }
    public void Refresh()
    {
        string s = WebData.RetrieveData("http://api.speedrunslive.com/races?id=" + id);
        Race race = JsonConvert.DeserializeObject<Race>(s);
        this.entrants = race.entrants;
        this.game = race.game;
        this.goal = race.goal;
        this.time = race.time;
        this.state = race.state;
        this.statetext = race.statetext;
        this.filename = race.filename;
        this.numentrants = race.numentrants;
    }
    public String id;
    public Game game;
    public String goal;
    public int time;
    public int state;
    public String statetext;
    public String filename;
    public int numentrants;
    public Dictionary<string, Entrant> entrants;
}

public class SRL
{
    private SRL() { }
    private static SRL instance;
    public static SRL Instance()
    {
        if (instance == null)
        {
            Refresh();
        }
        return instance;
    }
    public static void Refresh()
    {
        string s = WebData.RetrieveData("http://api.speedrunslive.com/races");
        instance = JsonConvert.DeserializeObject<SRL>(s);  
    }
    public int count;
    public List<Race> races;


}

public class WebData
{
    private WebData() { }
    public static string RetrieveData(string url)
    {

        // used to build entire input
        var sb = new StringBuilder();

        // used on each read operation
        var buf = new byte[8192];

        // prepare the web page we will be asking for
        var request = (HttpWebRequest)
                                    WebRequest.Create(url);

        /* Using the proxy class to access the site
            * Uri proxyURI = new Uri("http://proxy.com:80");
            request.Proxy = new WebProxy(proxyURI);
            request.Proxy.Credentials = new NetworkCredential("proxyuser", "proxypassword");*/

        // execute the request
        var response = (HttpWebResponse)
                                    request.GetResponse();

        // we will read data via the response stream
        Stream resStream = response.GetResponseStream();

        string tempString = null;
        int count = 0;

        do
        {
            // fill the buffer with data
            count = resStream.Read(buf, 0, buf.Length);

            // make sure we read some data
            if (count != 0)
            {
                // translate from bytes to ASCII text
                tempString = Encoding.UTF8.GetString(buf, 0, count);

                // continue building the string
                sb.Append(tempString);
            }
        } while (count > 0); // any more data to read?


        return sb.ToString();
    }
}
