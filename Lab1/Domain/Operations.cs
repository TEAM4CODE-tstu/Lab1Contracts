using System.Diagnostics;

namespace SortingAggregator.Domain;

public static class Operations
{
    /// <summary>
    /// Сортировка массива по неубыванию
    /// Pre:  массив не null и не пустой
    /// Post: массив упорядочен по неубыванию; мультимножество сохранено
    /// </summary>
    public static int[] Sort(int[] input)
    {
        // Pre: массив не null и не пустой
        Guard.Requires(input != null && input.Length > 0,
            "Массив не должен быть null или пустым");

        var result = (int[])input.Clone();
        Array.Sort(result);

        // Post: упорядочен
        for (int i = 1; i < result.Length; i++)
            Debug.Assert(result[i - 1] <= result[i],
                "Нарушено постусловие: массив не упорядочен");

        // Post: мультимножество сохранено
        Debug.Assert(result.SequenceEqual(input.OrderBy(x => x)),
            "Нарушено постусловие: мультимножество элементов изменено");

        return result;
    }

    /// <summary>
    /// Поиск минимума и максимума.
    /// Pre:  массив не null и не пустой
    /// Post: (min, max) — элементы из массива; min ≤ max;
    ///       min — наименьший, max — наибольший
    /// </summary>
    public static (int Min, int Max) FindMinMax(int[] input)
    {
        Guard.Requires(input != null && input.Length > 0,
            "Массив не должен быть null или пустым");

        int min = input.Min();
        int max = input.Max();

        // Post: min ≤ max
        Debug.Assert(min <= max, "Нарушено постусловие: min > max");
        // Post: min и max присутствуют в массиве
        Debug.Assert(input.Contains(min) && input.Contains(max),
            "Нарушено постусловие: min/max не найдены в массиве");

        return (min, max);
    }

    /// <summary>
    /// Сумма элементов массива
    /// Pre:  массив не null и не пустой
    /// Post: результат = сумма всех элементов; результат ≥ min*len и результат ≤ max*len
    /// </summary>
    public static long Sum(int[] input)
    {
        Guard.Requires(input != null && input.Length > 0,
            "Массив не должен быть null или пустым");

        long sum = input.Sum();

        var (min, max) = FindMinMax(input);
         
        // Post: сумма в ожидаемых границах
        Debug.Assert(sum >= (long)min * input.Length,
            "Нарушено постусловие: сумма меньше возможного минимума");
        Debug.Assert(sum <= (long)max * input.Length,
            "Нарушено постусловие: сумма больше возможного максимума");

        return sum;
    }
}
