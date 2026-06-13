namespace Pattern.Core.Facade;

public class HomeTheaterFacade(Projector projector, SoundSystem sound, StreamingService streaming)
{
    private string _currentMovie = string.Empty;

    public void StartMovie(string title)
    {
        _currentMovie = title;
        projector.TurnOn();
        sound.SetVolume(50);
        streaming.PlayMovie(title);
    }

    public void StopMovie()
    {
        streaming.StopMovie(_currentMovie);
        projector.TurnOff();
        sound.Mute();
    }
}
