﻿using Joyersch.Monogame.Ui.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame.Ui.Buttons;

public class TextButton<T> : IButton where T : IButton
{
    public BasicText BasicText { get; }

    private T _button;
    public Rectangle[] Hitbox => _button.Hitbox;
    public Rectangle Rectangle => _button.Rectangle;
    public event Action<object>? Leave;
    public event Action<object>? Enter;
    public event Action<object>? Click;

    public TextButton(string text, T button) : this(text, 1F, button)
    {
    }

    public TextButton(string text, float scale, T button)
    {
        _button = button;
        _button.Leave += _ => Leave?.Invoke(this);
        _button.Enter += _ => Enter?.Invoke(this);
        _button.Click += _ => Click?.Invoke(this);
        BasicText = new BasicText(text, scale * BasicText.DefaultLetterScale);
        BasicText.InRectangle(_button)
            .OnCenter()
            .Centered()
            .Apply();
    }

    public float Layer
    {
        get => _button.Layer;
        set => _button.Layer = value;
    }

    public bool IsHover => _button.IsHover;

    public float Scale => _button.Scale;


    public virtual void Update(GameTime gameTime)
    {
        _button.Update(gameTime);
        BasicText.Update(gameTime);
        BasicText.InRectangle(_button)
            .OnCenter()
            .Centered()
            .Apply();
    }

    public void UpdateInteraction(GameTime gameTime, IHitbox toCheck)
    {
        _button.UpdateInteraction(gameTime, toCheck);
    }

    public virtual void Draw(SpriteBatch spriteBatch)
    {
        _button.Draw(spriteBatch);
        BasicText.Draw(spriteBatch);
    }

    public Vector2 GetPosition()
        => _button.GetPosition();

    public Vector2 GetSize()
        => _button.GetSize();

    public void Move(Vector2 newPosition)
    {
        _button.Move(newPosition);
        BasicText.InRectangle(_button)
            .OnCenter()
            .Centered()
            .Apply();
    }

    public void ChangeColor(Microsoft.Xna.Framework.Color[] input)
        => _button.ChangeColor(input);

    public int ColorLength()
        => _button.ColorLength();

    public Microsoft.Xna.Framework.Color[] GetColor()
        => _button.GetColor();

    public void SetScale(float scale)
    {
        _button.SetScale(scale);
        BasicText.SetScale(scale);
    }
}