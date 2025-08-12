using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public interface IInteractable : IHitbox
{
    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck);
}