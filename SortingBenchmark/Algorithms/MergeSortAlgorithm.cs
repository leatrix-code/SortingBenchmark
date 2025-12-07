namespace SortingBenchmark.Algorithms;

public class MergeSortAlgorithm
{
    public int Comparisons { get; private set; }

    public void Sort(int[] array)
    {
        Comparisons = 0;

        var arr = (int[])array.Clone();
        MergeSortRecursive(arr, 0, arr.Length - 1);
    }

    private void MergeSortRecursive(int[] arr, int left, int right)
    {
        if (left >= right)
            return;

        // Вычисление середины
        var mid = left + (right - left) / 2;

        // Рекурсивная сортировка левой половины
        MergeSortRecursive(arr, left, mid);

        // Рекурсивная сортировка правой половины
        MergeSortRecursive(arr, mid + 1, right);

        // Слияние отсортированных половин
        Merge(arr, left, mid, right);
    }

    private void Merge(int[] arr, int left, int mid, int right)
    {
        // Создание временных массивов для левой и правой половин
        var leftArray = new int[mid - left + 1];
        var rightArray = new int[right - mid];

        // Копирование данных во временные массивы
        Array.Copy(arr, left, leftArray, 0, leftArray.Length);
        Array.Copy(arr, mid + 1, rightArray, 0, rightArray.Length);

        int i = 0, j = 0, k = left;

        // Слияние: сравнение и размещение элементов в исходный массив
        while (i < leftArray.Length && j < rightArray.Length)
        {
            // Подсчитываем каждое сравнение
            Comparisons++;

            // Нестрогое неравенство обеспечивает стабильность сортировки
            if (leftArray[i] <= rightArray[j])
            {
                arr[k++] = leftArray[i++];
            }
            else
            {
                arr[k++] = rightArray[j++];
            }
        }

        // Копирование оставшихся элементов левой половины
        while (i < leftArray.Length)
        {
            arr[k++] = leftArray[i++];
        }

        // Копирование оставшихся элементов правой половины
        while (j < rightArray.Length)
        {
            arr[k++] = rightArray[j++];
        }
    }
}