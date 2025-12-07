using SortingBenchmark.Benchmarking;

// Создаем тестировщик
var benchmarkRunner = new BenchmarkRunner();

// Определяем параметры тестирования
int[] testSizes = [100, 300, 500, 1000, 2000, 5000, 10000];
const int numTestsPerSize = 100;

// Типы тестируемых данных
DataType[] dataTypes =
[
    DataType.Random,      // Случайные данные
    DataType.Sorted,      // Отсортированные данные
    DataType.Reverse,     // Данные в обратном порядке
    DataType.AlmostSorted // Почти отсортированные данные
];

// Запускаем полный цикл тестирования
benchmarkRunner.Run(testSizes, numTestsPerSize, dataTypes);

Console.WriteLine("\nТестирование завершено!");