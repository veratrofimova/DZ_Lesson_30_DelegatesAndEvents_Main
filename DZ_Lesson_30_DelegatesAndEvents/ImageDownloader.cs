using System.Net;

namespace DZ_Lesson_30_DelegatesAndEvents
{
    public class ImageDownloader
    {
        private const string _remoteUri = "https://webneel.com/daily/sites/default/files/images/daily/08-2018/1-nature-photography-spring-season-mumtazshamsee.jpg";
        private const string _fileName = "bigimage.jpg";

        public event Action<string> DownloadImage;

        protected void ImageStarted(string text)
        {
            DownloadImage?.Invoke(text);
        }

        protected void ImageCompleted(string text)
        {
            DownloadImage?.Invoke(text);
        }

        public void DownloadFile()
        {
            Console.WriteLine("Качаю \"{0}\" из \"{1}\" .......\r\n", _fileName, _remoteUri);
            this.ImageStarted("Скачивание файла началось ...");

            var myWebClient = new WebClient();
            Task result = myWebClient.DownloadFileTaskAsync(_remoteUri, _fileName);            
            Thread.Sleep(5000);

            this.ImageCompleted("Скачивание файла закончилось.\r\n");
        }

        public async Task Download(CancellationToken token)
        {
            DownloadFile();

            await Task.Delay(0, token);
            
        }

        public async Task WaitForResult(Task task, CancellationTokenSource token)
        {
            await task;

            Console.WriteLine("Успешно скачен \"{0}\" из \"{1}\"\r\n", _fileName, _remoteUri);
        }
    }
}