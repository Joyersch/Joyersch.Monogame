using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Collision;

public class HitboxCollection : List<IHitbox>, IHitbox
{
    public Rectangle[] Hitbox => this.SelectMany(c => c.Hitbox).ToArray();
}