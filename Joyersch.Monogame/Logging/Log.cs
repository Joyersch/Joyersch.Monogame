using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Logging;

public static class Log
{
    public static LogAdapter Out { get; set; }
    private static string LeftBracket => Out.IsConsole ? "[SBO]" : "[";
    private static string RightBracket => Out.IsConsole ? "[SBC]" : "]";

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

    public static void WriteColor(string msg, Color[] colors)
    {
        Out.SetLine(-1);
        Out.WriteColor(msg, colors);
    }

    public static void Error(string msg)
    {
        Out.SetLine(-1);

        Out.WriteColor($"{LeftBracket}Error{RightBracket} {msg}", Color.Red);
    }

    public static void Critical(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{LeftBracket}Critical{RightBracket} {msg}", Color.DarkRed);
    }

    public static void Warning(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{LeftBracket}Warning{RightBracket} {msg}", Color.Gold);
    }

    public static void Information(string msg)
    {
        Out.SetLine(-1);
        Out.WriteColor($"{LeftBracket}Info{RightBracket} {msg}", Color.DeepSkyBlue);
    }
}