using Microsoft.Xna.Framework;

namespace Joyersch.Monogame.Graphics;

[AttributeUsage(AttributeTargets.Property)]
public class UsesAtlasRegionAttribute(string regionName, int x, int y, int width, int height) : Attribute
{
    public string RegionName { get; } = regionName;
    public int X { get; } = x;
    public int Y { get; } = y;
    public int Width { get; } = width;
    public int Height { get; } = height;
}