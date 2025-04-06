using Avalonia.Controls;
using Avalonia.Media;
using NuvoMusic.Client.Widgets;

namespace NuvoMusic.Client;

public class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        MinWidth = 800;
        MinHeight = 600;

        var grid = new Grid();

        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(1, GridUnitType.Star), MinWidth = 200, MaxWidth = 250 });
        grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(3, GridUnitType.Star) });

        var leftPanel = new Sidebar();
        Grid.SetColumn(leftPanel, 0);

        var rightPanel = new Border
        {
            Background = Brushes.LightGreen,
            Child = new TextBlock
            {
                Text = "Right Panel",
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            }
        };
        Grid.SetColumn(rightPanel, 1);

        grid.Children.Add(leftPanel);
        grid.Children.Add(rightPanel);

        Content = grid;
    }
}