using System.Diagnostics;
using SortingBenchmark.Algorithms;

namespace SortingBenchmark.Benchmarking;

public class BenchmarkRunner
{
    private readonly BenchmarkResultsCollection _results = new();
    private readonly BubbleSortAlgorithm _bubbleSort = new();
    private readonly MergeSortAlgorithm _mergeSort = new();
    
    public void Run(int[] testSizes, int numTestsPerSize, DataType[] dataTypes)
    {
        Console.WriteLine("ЭКСПЕРИМЕНТАЛЬНОЕ ТЕСТИРОВАНИЕ АЛГОРИТМОВ СОРТИРОВКИ");
        Console.WriteLine("Платформа: .NET 10.0 | Язык: C# 12.0");
        Console.WriteLine(new string('=', 100));
        Console.WriteLine();

        foreach (var dataType in dataTypes)
        {
            Console.WriteLine($"\n>>> Тестирование на {dataType} данных");
            Console.WriteLine(new string('-', 100));

            foreach (var size in testSizes)
            {
                // Генерируем выборку тестовых данных
                var testDatasets = TestDataGenerator.GenerateTestDatasets(
                    size,
                    numTestsPerSize,
                    dataType);

                Console.Write($"  Размер n = {size:D5}: ");

                double bubbleTotalTime = 0;
                double mergeTotalTime = 0;
                var bubbleOpsTotal = 0;
                var mergeOpsTotal = 0;

                // Проводим тестирование на каждом наборе данных
                for (var i = 0; i < testDatasets.Count; i++)
                {
                    var testData = testDatasets[i];

                    // Тестирование Bubble Sort
                    var stopwatch = Stopwatch.StartNew();
                    _bubbleSort.Sort(testData);
                    stopwatch.Stop();

                    bubbleTotalTime += stopwatch.Elapsed.TotalMilliseconds;
                    bubbleOpsTotal += _bubbleSort.Comparisons + _bubbleSort.Swaps;

                    // Тестирование Merge Sort (на том же наборе данных)
                    stopwatch.Restart();
                    _mergeSort.Sort(testData);
                    stopwatch.Stop();

                    mergeTotalTime += stopwatch.Elapsed.TotalMilliseconds;
                    mergeOpsTotal += _mergeSort.Comparisons;
                }

                // Вычисляем средние значения
                var avgBubbleTime = bubbleTotalTime / testDatasets.Count;
                var avgMergeTime = mergeTotalTime / testDatasets.Count;
                var avgBubbleOps = bubbleOpsTotal / testDatasets.Count;
                var avgMergeOps = mergeOpsTotal / testDatasets.Count;

                // Сохраняем результаты
                _results.AddResult(new BenchmarkResult
                {
                    ArraySize = size,
                    AlgorithmName = "BubbleSort",
                    ExecutionTimeMs = avgBubbleTime,
                    Operations = avgBubbleOps,
                    DataType = dataType
                });

                _results.AddResult(new BenchmarkResult
                {
                    ArraySize = size,
                    AlgorithmName = "MergeSort",
                    ExecutionTimeMs = avgMergeTime,
                    Operations = avgMergeOps,
                    DataType = dataType
                });

                Console.WriteLine(
                    $"Bubble: {avgBubbleTime,8:F4} мс | Merge: {avgMergeTime,8:F4} мс | Ускорение: {avgBubbleTime / avgMergeTime,6:F2}x");
            }
        }

        // Вывод итоговых результатов
        _results.PrintSummary();
    }
}