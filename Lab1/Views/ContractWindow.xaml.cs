using System.Windows;
using SortingAggregator.ViewModels;

namespace SortingAggregator.Views;

public partial class ContractWindow : Window
{
    public ContractWindow(OperationViewModelBase vm)
    {
        InitializeComponent();
        DataContext = vm;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) => Close();
}
