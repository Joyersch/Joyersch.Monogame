namespace Joyersch.Monogame.Graphics;

[AttributeUsage(AttributeTargets.Property)]
public class UsesAtlasAttribute(string atlasName) : Attribute
{
    public string AtlasName { get; } = atlasName;
}