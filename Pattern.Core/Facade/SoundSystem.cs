namespace Pattern.Core.Facade;

public class SoundSystem
{
    public void SetVolume(int volume) => Console.WriteLine($"Volume set to {volume}");
    public void Mute() => Console.WriteLine("Sound is muted");
}
