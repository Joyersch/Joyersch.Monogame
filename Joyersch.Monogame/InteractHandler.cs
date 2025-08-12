using Microsoft.Xna.Framework;

namespace Joyersch.Monogame;

public sealed class InteractHandler : IInteractable
{
    private List<(int zIndex, IInteractable interactable)> _interactables;

    public Rectangle[] Hitbox => [];

    public InteractHandler()
    {
        _interactables = new();
    }

    public void AddInteractable(IInteractable interactable, int zIndex)
        => _interactables.Add((zIndex, interactable));

    public bool UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        bool @return = false;
        foreach (var element in _interactables.OrderBy(i => i.zIndex))
        {
            @return |= element.interactable.UpdateInteraction(gameTime, toCheck);
            if (@return)
                break;
        }

        return @return;
    }
}