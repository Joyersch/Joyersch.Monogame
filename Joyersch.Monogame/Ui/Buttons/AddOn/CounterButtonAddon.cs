using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame.Ui.Buttons.AddOn;

public sealed class CounterButtonAddon : ButtonAddon
{
    private int _states;
    private readonly BasicText _basicText;

    public CounterButtonAddon(IButton button, int startStates, float scale = 1F) : base(button)
    {
        _states = startStates;
        _basicText = new BasicText("", GetPosition(), scale * BasicText.DefaultLetterScale);
        UpdateText();
        Button.Click += delegate
        {
            if (_states != 0)
                _states--;

            UpdateText();

            if (_states == 0)
                InvokeClick();
        };
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        _basicText.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        _basicText.Draw(spriteBatch);
    }

    private void UpdateText()
    {
        _basicText.ChangeText(_states.ToString());
    }

    public override void Move(Vector2 newPosition)
    {
        base.Move(newPosition);
        _basicText.Move(newPosition);
    }

    public override void SetScale(float scale)
    {
        base.SetScale(scale);
        _basicText.SetScale(scale);
    }
}