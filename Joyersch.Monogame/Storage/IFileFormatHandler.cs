namespace Joyersch.Monogame.Storage;

public interface IFileFormatHandler
{
    public void ReadFile(Type[] types, Dictionary<string, object> outputMap , byte[] data);

    public byte[] WriteFile(Dictionary<string, object> data);
}