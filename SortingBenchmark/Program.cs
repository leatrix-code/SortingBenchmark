namespace SortingBenchmark
{
    internal abstract class Program
    {
        private static void Main()
        {
            var adaptivityTester = new AdaptivityTester();

            Console.WriteLine("Выберите тест:");
            Console.WriteLine("1 - Общий бенчмарк (Bubble Sort, Merge Sort, Array.Sort)");
            Console.WriteLine("2 - Адаптивность Bubble Sort");
            Console.Write("Введите номер: ");
            
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    BenchmarkRunner.RunAllBenchmarks();
                    break;
                case "2":
                    adaptivityTester.RunAdaptivityTest();
                    break;
            }

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}