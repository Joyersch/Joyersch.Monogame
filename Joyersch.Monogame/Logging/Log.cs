using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Logging;

public static class Log
{
    public static ILogAdapter Out { get; set; }

    public static void Write(string msg)
    {
        Out.SetLine(-1);
        Out.Write(msg);
    }

    public static void WriteLine(string msg, int line)
    {
        Out.SetLine(line);
        Out.Write(msg);
    }

    public static void WriteColor(string msg, Color color)
    {
        Out.SetLine(-1);
        Out.WriteColor(msg, color);
    }

    public static void Error(string msg)
    {
        Out.SetLine(-1);

        Out.WriteColor($"{Out.LeftBracket}Error{Out.RightBracket} {msg}", Color.Red);
    }

    public static void Critical(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{Out.LeftBracket}Critical{Out.RightBracket} {msg}", Color.DarkRed);
    }

    public static void Warning(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{Out.LeftBracket}Warning{Out.RightBracket} {msg}", Color.Gold);
    }

    public static void Information(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{Out.LeftBracket}Info{Out.RightBracket} {msg}", Color.DeepSkyBlue);
    }
}