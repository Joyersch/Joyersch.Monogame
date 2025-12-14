using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Logging;

public class CliAdapter : ILogAdapter
{
    public void SetLine(int line)
    {
        Console.SetCursorPosition(0, line);
    }

    public void Write(string text)
    {
        Console.Write(text);
    }

    public void WriteColor(string text, Color color)
    {
        Console.WriteLine(text);
    }

    public string LeftBracket  => "[";
    public string RightBracket => "]";
}