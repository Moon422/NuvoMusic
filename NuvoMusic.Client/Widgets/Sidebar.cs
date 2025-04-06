using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia;
using Avalonia.Controls.Templates;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using System;

namespace NuvoMusic.Client.Widgets;

public class Sidebar : Grid
{
    private static Color defaultBackground = Colors.Transparent;
    private static Color hoverBackground = Color.FromRgb(55, 65, 81);
    private static Color activeBackground = Color.FromRgb(147, 51, 234);

    public Sidebar()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
        RowDefinitions.Add(new RowDefinition() { Height = new GridLength(1, GridUnitType.Star) });
        RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });

        Background = new SolidColorBrush(Color.FromRgb(31, 41, 55));

        var logoLabel = new TextBlock()
        {
            Text = "Nuvo Music",
            FontSize = 24,
            LineHeight = 32,
            TextAlignment = TextAlignment.Center,
            Foreground = Brushes.White,
            Margin = new Thickness(24),
            FontWeight = FontWeight.Bold
        };
        SetRow(logoLabel, 0);
        Children.Add(logoLabel);

        PrepareTopSidebar();
        PrepareBottomSidebar();
    }

    private void PrepareTopSidebar()
    {
        var radioButtonTemplate = new FuncControlTemplate<RadioButton>((radioButton, scope) =>
        {
            var contentPresenter = new ContentPresenter
            {
                [!ContentPresenter.ContentProperty] = radioButton[!ContentControl.ContentProperty],
                [!ContentPresenter.ContentTemplateProperty] = radioButton[!ContentControl.ContentTemplateProperty],
                [!ContentPresenter.BackgroundProperty] = radioButton[!TemplatedControl.BackgroundProperty],
                [!ContentPresenter.PaddingProperty] = radioButton[!TemplatedControl.PaddingProperty],
                [!ContentPresenter.CornerRadiusProperty] = radioButton[!TemplatedControl.CornerRadiusProperty],
            };

            radioButton.PointerEntered += (s, e) =>
            {
                radioButton.Cursor = new Cursor(StandardCursorType.Hand);
                contentPresenter.Background = new SolidColorBrush(radioButton.IsChecked == true ?
                    activeBackground : hoverBackground);
            };

            radioButton.PointerExited += (s, e) =>
            {
                radioButton.Cursor = null;
                contentPresenter.Background = new SolidColorBrush(radioButton.IsChecked == true ?
                    activeBackground : defaultBackground);
            };

            // Optional: Add visual feedback for checked state
            radioButton.PropertyChanged += (s, e) =>
            {
                if (e.Property == ToggleButton.IsCheckedProperty)
                {
                    contentPresenter.Background = new SolidColorBrush(radioButton.IsChecked == true ?
                        activeBackground : defaultBackground);
                }
            };

            return contentPresenter;
        });

        var topSidebar = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(24, 0),
            VerticalAlignment = VerticalAlignment.Stretch,
        };

        topSidebar.Children.Add(CreateTopSidebarButtons("Songs", "Assets/Icons/White/32/music.png", true, radioButtonTemplate));
        topSidebar.Children.Add(CreateTopSidebarButtons("Albums", "Assets/Icons/White/32/disc.png", false, radioButtonTemplate));
        topSidebar.Children.Add(CreateTopSidebarButtons("Artists", "Assets/Icons/White/32/user-round.png", false, radioButtonTemplate));
        topSidebar.Children.Add(CreateTopSidebarButtons("Playlists", "Assets/Icons/White/32/list-music.png", false, radioButtonTemplate));

        var scrollViewer = new ScrollViewer()
        {
            Content = topSidebar,
            VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
            HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled
        };
        SetRow(scrollViewer, 1);
        Children.Add(scrollViewer);
    }

    private RadioButton CreateTopSidebarButtons(string label, string iconPath, bool isSelected, IControlTemplate radioButtonTemplate)
    {
        var buttonBox = new StackPanel()
        {
            Orientation = Orientation.Horizontal,
            Spacing = 10,
        };
        buttonBox.Children.Add(new Image()
        {
            Width = 16,
            Height = 16,
            Source = new Bitmap(iconPath)
        });
        buttonBox.Children.Add(new TextBlock()
        {
            Text = label,
            Foreground = Brushes.White
        });

        var radioButton = new RadioButton()
        {
            Content = buttonBox,
            GroupName = "top-sidebar",
            Padding = new Thickness(10),
            CornerRadius = new CornerRadius(8),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Template = radioButtonTemplate,
            IsChecked = isSelected,
            Background = new SolidColorBrush(isSelected ? activeBackground : defaultBackground)
        };

        radioButton.IsCheckedChanged += (sender, args) =>
        {
            if (sender is RadioButton rb && (rb.IsChecked ?? false))
            {
                Console.WriteLine(rb.Content);
            }
        };

        return radioButton;
    }

    private void PrepareBottomSidebar()
    {
        var bottomSidebar = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(24)
        };

        bottomSidebar.Children.Add(new Button()
        {
            Content = "Refresh",
            Foreground = Brushes.White,
            Padding = new Thickness(10),
            CornerRadius = new CornerRadius(8),
            HorizontalAlignment = HorizontalAlignment.Stretch,
        });

        SetRow(bottomSidebar, 2);
        Children.Add(bottomSidebar);
    }
}
