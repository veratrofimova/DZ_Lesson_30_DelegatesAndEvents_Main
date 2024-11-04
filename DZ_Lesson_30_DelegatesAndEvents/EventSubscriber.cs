namespace DZ_Lesson_30_DelegatesAndEvents
{
    public class EventSubscriber
    {
        public void SubcriberTo(ImageDownloader sender)
        {
            sender.DownloadImage += (string text) =>
            {
                Console.WriteLine(text);
            };
        }
    }
}
