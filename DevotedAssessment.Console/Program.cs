internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var running = true;

        while (running)
        {

            var echo = Console.ReadLine();

            if (echo.Equals("exit", StringComparison.InvariantCultureIgnoreCase)) break;

            Console.WriteLine(echo);

        }
    }
}