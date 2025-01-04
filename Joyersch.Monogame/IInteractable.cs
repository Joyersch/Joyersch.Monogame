using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public interface IInteractable
{
    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck);
}