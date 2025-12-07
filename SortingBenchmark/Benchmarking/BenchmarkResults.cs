namespace SortingBenchmark.Benchmarking;

// Класс для хранения результатов одного теста
public class BenchmarkResult
{
    public required int ArraySize { get; init; }
    public required string AlgorithmName { get; init; }
    public required double ExecutionTimeMs { get; init; }
    public required int Operations { get; init; }
    public required DataType DataType { get; init; }
}

// Контейнер для накопления и анализа результатов
public class BenchmarkResultsCollection
{
    private readonly List<BenchmarkResult> _results = [];

    public void AddResult(BenchmarkResult result) => _results.Add(result);
    
    // Выводит сводку результатов в консоль
    public void PrintSummary()
    {
        var groupedBySize = _results
            .GroupBy(r => r.ArraySize)
            .OrderBy(g => g.Key);

        Console.WriteLine("\n" + new string('=', 100));
        Console.WriteLine("ТАБЛИЦА 1: Результаты экспериментального тестирования");
        Console.WriteLine(new string('=', 100));
        Console.WriteLine();

        foreach (var sizeGroup in groupedBySize)
        {
            var bubbleResults = sizeGroup.Where(r => r.AlgorithmName == "BubbleSort").ToList();
            var mergeResults = sizeGroup.Where(r => r.AlgorithmName == "MergeSort").ToList();

            var bubbleTimeAvg = bubbleResults.Average(r => r.ExecutionTimeMs);
            var mergeTimeAvg = mergeResults.Average(r => r.ExecutionTimeMs);
            var bubbleOpsAvg = bubbleResults.Average(r => r.Operations);
            var mergeOpsAvg = mergeResults.Average(r => r.Operations);

            Console.WriteLine($"Размер массива: n = {sizeGroup.Key:D5}");
            Console.WriteLine($"  Bubble Sort:   {bubbleTimeAvg,10:F4} мс   |  {bubbleOpsAvg,12:F0} операций");
            Console.WriteLine($"  Merge Sort:    {mergeTimeAvg,10:F4} мс   |  {mergeOpsAvg,12:F0} операций");
            Console.WriteLine($"  Ускорение:     {bubbleTimeAvg / mergeTimeAvg,10:F2}x");
            Console.WriteLine();
        }

        Console.WriteLine(new string('=', 100));
    }
}