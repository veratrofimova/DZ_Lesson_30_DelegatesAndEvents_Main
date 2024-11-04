using DZ_Lesson_30_DelegatesAndEvents;

Console.WriteLine("Вашему вниманию предлагается работа на тему: Делегаты, Event-ы, добавляем асинхронное выполнение\r\n");

var imageDownloader = new ImageDownloader();
var sender = new EventSubscriber();
sender.SubcriberTo(imageDownloader);
var cts = new CancellationTokenSource();
var token = cts.Token;

var result = Task.Run(() => imageDownloader.Download(token));
Task.Run(async () =>
{
    await imageDownloader.WaitForResult(result, cts);
});

Console.WriteLine("Нажмите клавишу A для выхода или любую другую клавишу для проверки статуса скачивания\r\n");

var input = "";
do 
{
    input = Console.ReadLine();
    if (input != "A" && input != "А")
    {
        Console.WriteLine(string.Format("Скачивание файла {0}", result.IsCompleted ? "закончилось\r\n" : "продолжается..."));
    }
    else
    {
        cts.Cancel();
        Console.WriteLine("\r\nВыполнение прервано по решению пользователя");
    }

} while (!token.IsCancellationRequested);

Console.WriteLine("\r\nПрограмма завершена");