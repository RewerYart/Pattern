namespace Pattern.Core.Facade;

public class StreamingService
{
    public void PlayMovie(string title) => Console.WriteLine($"Playing movie: {title}");
    public void StopMovie(string title) => Console.WriteLine($"Stopping movie: {title}");
}
