using Microsoft.Xna.Framework.Graphics;

namespace Joyersch.Monogame;

public interface IDrawable : IRectangle
{
    public void Draw(SpriteBatch spriteBatch);
}