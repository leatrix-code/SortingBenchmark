using System.Diagnostics;

namespace SortingBenchmark
{
    public class AdaptivityTester
    {
        private static readonly int[] Sizes = [100, 300, 500, 1000, 2000, 5000, 10000];
        private const int RepeatsPerSize = 50;

        public void RunAdaptivityTest()
        {
            Console.WriteLine($"Количество повторов для каждого размера: {RepeatsPerSize}");
            Console.WriteLine();

            Console.WriteLine("{0,-8} | {1,-14} | {2,-14} | {3,-14}", 
                "Размер", "Отсортирован", "Обратный порядок", "Случайный");
            Console.WriteLine(new string('-', 60));

            foreach (var size in Sizes)
            {
                var results = RunAdaptivityTestForSize(size);
                
                var sortedTime = results.SortedTime;
                var reversedTime = results.ReversedTime;
                var randomTime = results.RandomTime;

                Console.WriteLine("{0,-8} | {1,-14:F4} | {2,-14:F4} | {3,-14:F4}",
                    size,
                    sortedTime,
                    reversedTime,
                    randomTime);
            }
        }

        private AdaptivityResult RunAdaptivityTestForSize(int size)
        {
            double sortedTotalMs = 0;
            double reversedTotalMs = 0;
            double randomTotalMs = 0;

            for (var r = 0; r < RepeatsPerSize; r++)
            {
                var arrSorted = GenerateSortedArray(size);
                var sw = Stopwatch.StartNew();
                SortingAlgorithms.BubbleSort(arrSorted);
                sw.Stop();
                sortedTotalMs += sw.Elapsed.TotalMilliseconds;

                var arrReversed = GenerateReversedArray(size);
                sw.Restart();
                SortingAlgorithms.BubbleSort(arrReversed);
                sw.Stop();
                reversedTotalMs += sw.Elapsed.TotalMilliseconds;

                var arrRandom = GenerateRandomArray(size);
                sw.Restart();
                SortingAlgorithms.BubbleSort(arrRandom);
                sw.Stop();
                randomTotalMs += sw.Elapsed.TotalMilliseconds;
            }

            return new AdaptivityResult
            {
                SortedTime = sortedTotalMs / RepeatsPerSize,
                ReversedTime = reversedTotalMs / RepeatsPerSize,
                RandomTime = randomTotalMs / RepeatsPerSize
            };
        }

        private static int[] GenerateSortedArray(int size)
        {
            var arr = new int[size];
            for (var i = 0; i < size; i++)
                arr[i] = i;
            return arr;
        }

        private static int[] GenerateReversedArray(int size)
        {
            var arr = new int[size];
            for (var i = 0; i < size; i++)
                arr[i] = size - i;
            return arr;
        }

        private static int[] GenerateRandomArray(int size)
        {
            var random = new Random(42);
            var arr = new int[size];
            for (var i = 0; i < size; i++)
                arr[i] = random.Next(int.MinValue, int.MaxValue);
            return arr;
        }

        private class AdaptivityResult
        {
            public double SortedTime { get; init; }
            public double ReversedTime { get; init; }
            public double RandomTime { get; init; }
        }
    }
}
