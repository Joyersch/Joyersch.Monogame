using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public class RectangleWrapper : IRectangle
{
    public Rectangle Rectangle { private set; get; }

    public RectangleWrapper(Rectangle rectangle)
    {
        Rectangle = rectangle;
    }
}