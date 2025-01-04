using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public struct TripodPoint(Vector2 position, float zoom)
{
    public Vector2 Position { get; set; } = position;
    public float Zoom { get; set; } = zoom;
}