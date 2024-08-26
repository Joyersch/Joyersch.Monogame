using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoUtils.Logic.Management;
using MonoUtils.Ui;
using MonoUtils.Ui.TextSystem;
using MonoUtils.Logic;
using MonoUtils.Ui.Color;

namespace Joyersch.Monogame;

public class Titlecard : IManageable
{
    public static Texture2D Texture;
    public event Action FinishedScene;

    private Text _byText;
    private readonly Scene _scene;
    private readonly NeyaFace _neyaFace;
    private ColorTransition _colorTransition;
    private OverTimeInvoker _overTimeInvoker;

    private float _firstHold = 500F;
    private float _faceInTime = 2000F;
    private float _secondHold = 2500F;
    private float _faceOutTime = 1500F;
    private float _thirdHold = 500F;

    public Rectangle Rectangle => _scene.Camera.Rectangle;

    public Titlecard(Scene scene)
    {
        _scene = scene;
        float scale = 1.5F;
        _neyaFace = new NeyaFace(scene, scale);
        _neyaFace.InRectangle(_scene.Camera.Rectangle)
            .OnCenter()
            .OnY(0.4F)
            .Centered()
            .Move();

        _byText = new Text("A game by Joyersch...", _scene.Display.SimpleScale * scale);
        _byText.InRectangle(_scene.Camera.Rectangle)
            .OnCenter()
            .OnY(0.75F)
            .Centered()
            .Move();

        _colorTransition = new ColorTransition(Color.Transparent, Color.Transparent, 0F);
        _overTimeInvoker = new OverTimeInvoker(_firstHold)
        {
            InvokeOnce = true
        };
        _overTimeInvoker.Trigger += delegate
        {
            _colorTransition = new ColorTransition(Color.Transparent, Color.White, _faceInTime);
            _colorTransition.FinishedTransition += delegate { _overTimeInvoker?.Start(); };
            _overTimeInvoker = new OverTimeInvoker(_secondHold, false)
            {
                InvokeOnce = true
            };
            _overTimeInvoker.Trigger += delegate
            {
                _colorTransition = new ColorTransition(Color.White, Color.Transparent, _faceOutTime);
                _overTimeInvoker = new OverTimeInvoker(_thirdHold, false)
                {
                    InvokeOnce = true
                };
                _colorTransition.FinishedTransition += delegate { _overTimeInvoker.Start(); };
                _overTimeInvoker.Trigger += delegate { FinishedScene?.Invoke(); };
            };
        };
    }

    public static void LoadContent(ContentManager content)
    {
        Texture = content.GetTexture("Titlecard/Background");
        NeyaFace.LoadContent(content);
    }

    public void Update(GameTime gameTime)
    {
        _colorTransition.Update(gameTime);
        var color = _colorTransition.GetColor()[0];
        _neyaFace.ChangeColor([color]);
        _byText.ChangeColor(color);
        _byText.Update(gameTime);

        _overTimeInvoker.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            _scene.Camera.RealPosition,
            null,
            Color.White,
            0, /*Rotation*/
            Vector2.Zero,
            Vector2.One * 10 * _scene.Display.SimpleScale * 1 / _scene.Camera.Zoom,
            SpriteEffects.None,
            0 /*Layer*/);

        _byText.Draw(spriteBatch);

        _neyaFace.Draw(spriteBatch);
    }
}