﻿using Joyersch.Monogame.Ui.Color;
using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Joyersch.Monogame.Ui.Titlecard;

public class Titlecard : IManageable
{
    public static Texture2D Texture;
    public event Action FinishedScene;

    private BasicText _byBasicText;
    private readonly Scene _scene;
    private readonly NeyaFace _neyaFace;
    private ColorTransition _colorTransition;
    private OverTimeInvoker _overTimeInvoker;

    private float _firstHold = 500F;
    private float _faceInTime = 850F;
    private float _secondHold = 1750F;
    private float _faceOutTime = 850F;
    private float _thirdHold = 500F;

    public Rectangle Rectangle => _scene.Camera.Rectangle;

    public Titlecard(Scene scene)
    {
        _scene = scene;
        float scale = 3F;
        _neyaFace = new NeyaFace(scene, scale);
        _neyaFace.InRectangle(_scene.Camera)
            .OnX(0.5F)
            .OnY(0.4F)
            .Centered()
            .Apply();

        _byBasicText = new BasicText("Joyersch presents...", _scene.Display.Scale * scale);
        _byBasicText.InRectangle(_scene.Camera)
            .OnX(0.5F)
            .OnY(0.75F)
            .Centered()
            .Apply();

        _colorTransition = new ColorTransition(Microsoft.Xna.Framework.Color.Transparent, Microsoft.Xna.Framework.Color.Transparent, 0F);
        _overTimeInvoker = new OverTimeInvoker(_firstHold)
        {
            InvokeOnce = true
        };
        _overTimeInvoker.Trigger += delegate
        {
            _colorTransition = new ColorTransition(Microsoft.Xna.Framework.Color.Transparent, Microsoft.Xna.Framework.Color.White, _faceInTime);
            _colorTransition.FinishedTransition += delegate { _overTimeInvoker?.Start(); };
            _overTimeInvoker = new OverTimeInvoker(_secondHold, false)
            {
                InvokeOnce = true
            };
            _overTimeInvoker.Trigger += delegate
            {
                _colorTransition = new ColorTransition(Microsoft.Xna.Framework.Color.White, Microsoft.Xna.Framework.Color.Transparent, _faceOutTime);
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
        if (Keyboard.GetState().GetPressedKeyCount() > 0
            || Mouse.GetState().LeftButton == ButtonState.Pressed
            || Mouse.GetState().RightButton == ButtonState.Pressed
            || Mouse.GetState().MiddleButton == ButtonState.Pressed)
            FinishedScene?.Invoke();

        _colorTransition.Update(gameTime);
        var color = _colorTransition.GetColor()[0];
        _neyaFace.ChangeColor([color]);
        _byBasicText.ChangeColor(color);
        _byBasicText.Update(gameTime);

        _overTimeInvoker.Update(gameTime);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(
            Texture,
            _scene.Camera.RealPosition,
            null,
            Microsoft.Xna.Framework.Color.White,
            0, /*Rotation*/
            Vector2.Zero,
            Vector2.One * 10 * _scene.Display.Scale,
            SpriteEffects.None,
            0 /*Layer*/);

        _byBasicText.Draw(spriteBatch);

        _neyaFace.Draw(spriteBatch);
    }
}