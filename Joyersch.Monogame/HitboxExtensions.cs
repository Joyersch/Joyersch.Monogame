using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public static class HitboxExtensions
{
    public static bool IntersectsHitbox(this IHitbox haystack, Rectangle neddle)
    {
        if (haystack.Hitbox.Any(box => box.Intersects(neddle)))
            return true;
        return false;
    }
}