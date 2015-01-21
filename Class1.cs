using System;
using System.Net.Sockets;

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
    public int count;
    public List<Race> races;
}
