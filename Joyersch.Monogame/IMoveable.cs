using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public interface IMoveable : ISpatial
{

    public void Move(Vector2 newPosition);
}