namespace SortingBenchmark.Algorithms;

public class BubbleSortAlgorithm
{
    public int Comparisons { get; private set; }
    public int Swaps { get; private set; }
    
    public void Sort(int[] array)
    {
        Comparisons = 0;
        Swaps = 0;

        var arr = (int[])array.Clone();
        var n = arr.Length;

        for (var i = 0; i < n - 1; i++)
        {
            var swapped = false;

            for (var j = 0; j < n - i - 1; j++)
            {
                // Подсчитываем каждое сравнение
                Comparisons++;

                if (arr[j] <= arr[j + 1]) continue;
                // Подсчитываем каждый обмен
                Swaps++;

                // Обмен элементов
                (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                swapped = true;
            }

            // Оптимизация: выход при отсутствии обменов
            if (!swapped)
                break;
        }
    }
}