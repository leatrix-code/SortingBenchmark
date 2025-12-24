using System;

namespace SortingBenchmark
{
    public static class SortingAlgorithms
    {
        public static void BubbleSort(int[] array)
        {
            var n = array.Length;

            for (var i = 0; i < n - 1; i++)
            {
                var swapped = false;

                for (var j = 0; j < n - i - 1; j++)
                {
                    if (array[j] <= array[j + 1]) continue;
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapped = true;
                }

                if (!swapped)
                    break;
            }
        }

        public static void MergeSort(int[] array)
        {
            if (array.Length <= 1)
                return;

            MergeSortRecursive(array, 0, array.Length - 1, new int[array.Length]);
        }

        private static void MergeSortRecursive(int[] array, int left, int right, int[] buffer)
        {
            if (left >= right)
                return;

            var mid = left + (right - left) / 2;

            MergeSortRecursive(array, left, mid, buffer);
            MergeSortRecursive(array, mid + 1, right, buffer);
            Merge(array, left, mid, right, buffer);
        }

        private static void Merge(int[] array, int left, int mid, int right, int[] buffer)
        {
            var i = left;
            var j = mid + 1;
            var k = left;

            while (i <= mid && j <= right)
            {
                if (array[i] <= array[j])
                {
                    buffer[k++] = array[i++];
                }
                else
                {
                    buffer[k++] = array[j++];
                }
            }

            while (i <= mid)
                buffer[k++] = array[i++];

            while (j <= right)
                buffer[k++] = array[j++];

            for (var p = left; p <= right; p++)
                array[p] = buffer[p];
        }
    }
}
