using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Logging;

public static class Log
{
    public static ILogAdapter Out { get; set; }

    public static void Write(string msg)
    {
        Out.Write(msg);
    }

    public static void WriteLine(string msg, int line)
    {
        Out.Write(msg);
    }

    public static void WriteColor(string msg, Color color)
    {
        Out.WriteColor(msg, color);
    }

    public static void Error(object msg)
    {
        Out.WriteColor($"{Out.LeftBracket}Error{Out.RightBracket} {msg}", Color.Red);
    }

    public static void Critical(object msg)
    {
        Out.WriteColor($"{Out.LeftBracket}Critical{Out.RightBracket} {msg}", Color.DarkRed);
    }

    public static void Warning(object msg)
    {
        Out.WriteColor($"{Out.LeftBracket}Warning{Out.RightBracket} {msg}", Color.Gold);
    }

    public static void Information(object msg)
    {
        Out.WriteColor($"{Out.LeftBracket}Info{Out.RightBracket} {msg}", Color.DeepSkyBlue);
    }
}