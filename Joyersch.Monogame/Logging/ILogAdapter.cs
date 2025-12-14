using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Logging;

public interface ILogAdapter
{
    public void SetLine(int line);

    public void Write(string text);

    public void WriteColor(string text, Color color);

    public string LeftBracket { get; }

    public string RightBracket { get; }
}