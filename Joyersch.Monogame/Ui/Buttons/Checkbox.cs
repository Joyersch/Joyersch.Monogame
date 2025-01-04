using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Ui.Buttons;

public class Checkbox : TextButton<SquareButton>
{
    private bool _checked;

    public Action<bool> ValueChanged;

    public static float DefaultScale { get; set; } = 4F;

    public Checkbox() : this(false)
    {
    }

    public Checkbox(float scale) : this(scale, false)
    {
    }

    public Checkbox(bool state) : this(DefaultScale, state)
    {
    }

    public Checkbox(float scale, bool state) : base(string.Empty, new SquareButton(Vector2.Zero, scale))
    {
        _checked = state;
        BasicText.ChangeText(_checked ? "[checkmark]" : "[crossout]");
        BasicText.ChangeColor(new[] { _checked ? Microsoft.Xna.Framework.Color.Green : Microsoft.Xna.Framework.Color.Red });
        Click += delegate
        {
            _checked = !_checked;
            BasicText.ChangeText(_checked ? "[checkmark]" : "[crossout]");
            BasicText.ChangeColor(new[]
                { _checked ? Microsoft.Xna.Framework.Color.Green : Microsoft.Xna.Framework.Color.Red });
            ValueChanged?.Invoke(_checked);
        };
        // Update BasicText
        Move(GetPosition());
    }

    public bool Checked()
        => _checked;
}