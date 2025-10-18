namespace Joyersch.Monogame;

public interface IScaleable
{
    public float Scale { get; }
    public void SetScale(ScaleProvider provider);
}