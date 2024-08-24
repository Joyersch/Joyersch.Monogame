using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoUtils.Logic.Management;
using MonoUtils.Ui;
using MonoUtils.Ui.TextSystem;
using MonoUtils.Logic;

namespace Joyersch.Monogame;

public class Titlecard : IManageable
{
    public static Texture2D Texture;
    public event Action FinishedScene;

    private List<Text> _display;
    private readonly Scene _scene;

    public Rectangle Rectangle => _scene.Camera.Rectangle;

    public Titlecard(Scene scene)
    {
        _scene = scene;
        _display = new List<Text>();
        for (int i = 0; i < 40; i++)
        {
            var text = new Text("000000000000000000000000000000000000000000000000000", _scene.Display.SimpleScale);
            text.InRectangle(_scene.Camera.Rectangle)
                .OnCenter()
                .OnY(0.1F + 0.85F * (i / 40F))
                .Centered()
                .Move();
            _display.Add(text);
        }

    }

    public static void LoadContent(ContentManager contentManager)
    {
        Texture = contentManager.GetTexture("Titlecard");
    }

    public void Update(GameTime gameTime)
    {
        foreach (var text in _display)
            text.Update(gameTime);

        if (Keyboard.GetState().IsKeyDown(Keys.F))
            FinishedScene.Invoke();
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
            Vector2.One * 10 * _scene.Display.SimpleScale * 1 /_scene.Camera.Zoom,
            SpriteEffects.None,
            0 /*Layer*/);

        foreach (var text in _display)
            text.Draw(spriteBatch);
    }
}