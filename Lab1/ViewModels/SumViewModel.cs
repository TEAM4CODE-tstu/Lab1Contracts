using SortingAggregator.Domain;

namespace SortingAggregator.ViewModels;

public class SumViewModel : OperationViewModelBase
{
    public override string DisplayName => "Сумма элементов";
    public override string ContractPre =>
        "Массив не null и не пустой";
    public override string ContractPost =>
        "Результат = сумма всех элементов; min*len ≤ сумма ≤ max*len";
    public override string ContractEffects =>
        "Возвращает сумму элементов " +
        "Исключение: ArgumentException при пустом или null-входе";
    public override string ExampleValid =>
        "Вход: 5, 3, 8, 1 → результат: 17";
    public override string ExampleInvalid =>
        "Вход: (пусто) → предусловие не выполнено";

    protected override bool CheckPre(out string failReason)
    {
        (bool isValid, failReason, int[] values) = ValidateInputText();

        return isValid;
    }

    protected override void RunOperation(int[] input, out string result)
    {
        var sum = Operations.Sum(input);
        result = sum.ToString();
    }

    protected override bool CheckPost(int[] input, string result)
    {
        long sum = long.Parse(result);
        long expected = input.Select(x => (long)x).Sum();
        return sum == expected;
    }
}
