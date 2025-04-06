using Avalonia.Layout;
using Avalonia.Controls;
using Avalonia.Media.Imaging;
using Avalonia.Media;
using Avalonia;
using Avalonia.Markup.Xaml.Templates;
using Avalonia.Styling;
using Avalonia.Controls.Templates;
using System.Linq;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using System;

namespace NuvoMusic.Client.Widgets;

public class Sidebar : StackPanel
{
    public Sidebar()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        Orientation = Orientation.Vertical;
        Background = new SolidColorBrush(Color.FromRgb(17, 24, 39));

        var topSidebar = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(24)
        };

        Color defaultBackground = Colors.Transparent;
        Color hoverBackground = Color.FromRgb(55, 65, 81);
        Color activeBackground = Color.FromRgb(147, 51, 234);

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

        topSidebar.Children.Add(CreateTopSidebarButtons("Songs", "Assets/Icons/White/32/music.png", radioButtonTemplate));
        topSidebar.Children.Add(CreateTopSidebarButtons("Albums", "Assets/Icons/White/32/disc.png", radioButtonTemplate));

        var bottomSidebar = new StackPanel()
        {
            Orientation = Orientation.Vertical,
            Margin = new Thickness(24)
        };

        Children.Add(topSidebar);
        Children.Add(bottomSidebar);
    }

    private RadioButton CreateTopSidebarButtons(string label, string iconPath, IControlTemplate radioButtonTemplate)
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

        return new RadioButton()
        {
            Content = buttonBox,
            GroupName = "top-sidebar",
            Padding = new Thickness(10),
            CornerRadius = new CornerRadius(8),
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Template = radioButtonTemplate
        };
    }
}
