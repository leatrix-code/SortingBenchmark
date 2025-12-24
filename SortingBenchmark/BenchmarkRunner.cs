using System.Diagnostics;

namespace SortingBenchmark
{
    public class BenchmarkRunner
    {
        private static readonly int[] Sizes = [100, 300, 500, 1000, 2000, 5000, 10000];
        private const int RepeatsPerSize = 100;

        public static void RunAllBenchmarks()
        {
            Console.WriteLine("Сравнение алгоритмов сортировки: Bubble Sort, Merge Sort и Array.Sort");
            Console.WriteLine($"Количество повторов для каждого размера массива: {RepeatsPerSize}");
            Console.WriteLine();

            foreach (var size in Sizes)
            {
                RunBenchmarksForSize(size);
                Console.WriteLine(new string('-', 80));
            }
        }

        private static void RunBenchmarksForSize(int size)
        {
            var random = new Random(42);

            double bubbleTotalMs = 0;
            double mergeTotalMs = 0;
            double arraySortTotalMs = 0;

            for (var r = 0; r < RepeatsPerSize; r++)
            {
                var data = GenerateRandomArray(size, random);

                var arrBubble = (int[])data.Clone();
                var arrMerge = (int[])data.Clone();
                var arrArraySort = (int[])data.Clone();

                var sw = Stopwatch.StartNew();
                SortingAlgorithms.BubbleSort(arrBubble);
                sw.Stop();
                bubbleTotalMs += sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                SortingAlgorithms.MergeSort(arrMerge);
                sw.Stop();
                mergeTotalMs += sw.Elapsed.TotalMilliseconds;

                sw.Restart();
                Array.Sort(arrArraySort);
                sw.Stop();
                arraySortTotalMs += sw.Elapsed.TotalMilliseconds;
            }

            var bubbleAvg = bubbleTotalMs / RepeatsPerSize;
            var mergeAvg = mergeTotalMs / RepeatsPerSize;
            var arraySortAvg = arraySortTotalMs / RepeatsPerSize;

            Console.WriteLine($"Размер массива: {size}");
            Console.WriteLine("Среднее время (мс) за " + RepeatsPerSize + " повторов:");
            Console.WriteLine($"  Bubble Sort : {bubbleAvg:F4} мс");
            Console.WriteLine($"  Merge Sort  : {mergeAvg:F4} мс");
            Console.WriteLine($"  Array.Sort  : {arraySortAvg:F4} мс");

            Console.WriteLine("Относительные ускорения (по времени):");
            Console.WriteLine($"  Bubble / Merge     : {(bubbleAvg / mergeAvg):F1}x");
            Console.WriteLine($"  Bubble / Array.Sort: {(bubbleAvg / arraySortAvg):F1}x");
            Console.WriteLine($"  Merge  / Array.Sort: {(mergeAvg / arraySortAvg):F1}x");
        }

        private static int[] GenerateRandomArray(int size, Random random)
        {
            var arr = new int[size];
            for (var i = 0; i < size; i++)
                arr[i] = random.Next(int.MinValue, int.MaxValue);
            return arr;
        }
    }
}
