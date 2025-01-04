using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Collision;

public sealed class EmptyHitbox : IHitbox
{
    public Rectangle[] Hitbox { get; } =  new Rectangle[0];
}