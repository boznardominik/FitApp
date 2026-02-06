using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace FitApp.Views;

public partial class StatisticsView : UserControl
{
    public StatisticsView()
    {
        InitializeComponent();
    }
    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }


}