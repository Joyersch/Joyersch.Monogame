using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public static class SpartialExtensions
{
    public static Rectangle GetRectangle(this ISpatial spatial)
        => new Rectangle(spatial.GetPosition().ToPoint(), spatial.GetSize().ToPoint());
}