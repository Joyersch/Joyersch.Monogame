using Joyersch.Monogame.Ui.Color;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using IDrawable = Joyersch.Monogame.IDrawable;

namespace Joyersch.Monogame.Ui.Titlecard;

public class NeyaFace : IDrawable, IColorable, IMoveable
{
    public static Texture2D Texture;

    private Microsoft.Xna.Framework.Color _color;
    private readonly Scene _scene;

    private Vector2 _position;
    private Vector2 _size;


    public Rectangle Rectangle { get; private set; }

    public NeyaFace(Scene scene, float scale)
    {
        _scene = scene;
        _color = Microsoft.Xna.Framework.Color.White;
        _size = new Vector2(128, 128) * scene.Display.Scale * scale;
        Rectangle = new Rectangle(_position.ToPoint(), _size.ToPoint());
    }

    public static void LoadContent(ContentManager content)
    {
        Texture = content.GetTexture("Titlecard/Neya");
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(Texture, Rectangle, _color);
    }

    public void ChangeColor(Microsoft.Xna.Framework.Color[] input)
        => _color = input[0];

    public int ColorLength()
        => 1;

    public Microsoft.Xna.Framework.Color[] GetColor()
        => [_color];

    public Vector2 GetPosition() => _position;

    public Vector2 GetSize()
        => _size;

    public void Move(Vector2 newPosition)
    {
        _position = newPosition;
        Rectangle = new Rectangle(_position.ToPoint(), _size.ToPoint());
    }
}