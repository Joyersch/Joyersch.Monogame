using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Collision;

public sealed class CustomHitbox : IHitbox
{
    public Rectangle[] Hitbox => _hitbox;

    private Rectangle[] _hitbox;

    public void SetHitbox(Rectangle[] rectangles)
        => _hitbox = rectangles;
}