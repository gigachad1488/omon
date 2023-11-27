using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using omon.Models;

namespace omon.Views;

public partial class AddStorageWindow : Window
{
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        cancelText.PointerReleased += delegate { Close(null); };
    }

    public AddStorageWindow()
    {
        InitializeComponent();
        saveText.PointerReleased += delegate { Add(); };
    }
    
    public AddStorageWindow(Storage storage)
    {
        InitializeComponent();
        saveText.PointerReleased += delegate { Redact(); };
    }

    public void Add()
    {
        Close(null);
    }

    public void Redact()
    {
        Close(null);
    }
}