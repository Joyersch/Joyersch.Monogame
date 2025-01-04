
namespace Joyersch.Monogame.Ui.Buttons
{
    public interface IButton : IHitbox, IManageable, IMoveable, IColorable, IMouseActions, IInteractable, ILayerable, IScaleable
    {
        public bool IsHover { get; }
    }
}