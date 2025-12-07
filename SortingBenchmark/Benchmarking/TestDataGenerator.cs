namespace SortingBenchmark.Benchmarking;

public static class TestDataGenerator
{
    private static readonly Random Random = new(42); // Seed для воспроизводимости
    
    // Генерирует набор уникальных тестовых массивов
    public static List<int[]> GenerateTestDatasets(
        int arraySize,
        int numTests,
        DataType dataType = DataType.Random)
    {
        var datasets = new List<int[]>();

        for (var i = 0; i < numTests; i++)
        {
            var dataset = dataType switch
            {
                DataType.Random => GenerateRandomArray(arraySize),
                DataType.Sorted => GenerateSortedArray(arraySize),
                DataType.Reverse => GenerateReverseArray(arraySize),
                DataType.AlmostSorted => GenerateAlmostSortedArray(arraySize),
            };

            datasets.Add(dataset);
        }

        return datasets;
    }
    
    // Генерирует массив случайных целых чисел
    private static int[] GenerateRandomArray(int size)
    {
        var array = new int[size];
        for (var i = 0; i < size; i++)
        {
            array[i] = Random.Next(0, 100000);
        }
        return array;
    }
    
    // Генерирует отсортированный массив
    private static int[] GenerateSortedArray(int size)
    {
        return Enumerable.Range(0, size).ToArray();
    }

    // Генерирует массив в обратном порядке
    private static int[] GenerateReverseArray(int size)
    {
        return Enumerable.Range(0, size).Reverse().ToArray();
    }
    
    // Генерирует почти отсортированный массивW
    private static int[] GenerateAlmostSortedArray(int size)
    {
        var array = Enumerable.Range(0, size).ToArray();
        var swapCount = Math.Max(1, size / 20);

        for (var i = 0; i < swapCount; i++)
        {
            var idx1 = Random.Next(size);
            var idx2 = Random.Next(size);
            (array[idx1], array[idx2]) = (array[idx2], array[idx1]);
        }

        return array;
    }
}

// Типы генерируемых тестовых данных
public enum DataType
{
    Random,
    Sorted,
    Reverse,
    AlmostSorted
}