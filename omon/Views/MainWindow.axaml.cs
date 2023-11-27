using System;
using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using omon.Models;
using omon.ViewModels;

namespace omon.Views;

public partial class MainWindow : Window
{
    public ITable currentTable;
    public Object selectedItem;
    public MainWindow()
    {
        InitializeComponent();
        SizeChanged += StretchPopup;
        tablesMenuButton.PointerEntered += delegate { SwitchMenu(); };
        storageTableButton.Click += delegate { f(); };
        grid.SelectionChanged += DataGrid_OnSelectionChanged;
    }

    public void StretchPopup(object? sender, SizeChangedEventArgs args)
    {
        Size ns = args.NewSize;
        menuPopup.Width = ns.Width * 0.995;
        menuPopup.Height = ns.Height * 0.92;
    }
    public void SwitchMenu()
    {
        menuPopup.IsOpen = !menuPopup.IsOpen;
    }
    public void f()
    {
        menuPopup.IsOpen = false;
        if (currentTable != null)
        {
            grid.AutoGeneratingColumn -= currentTable.CollumnName;
        }
            currentTable = new StorageTable();
            grid.AutoGeneratingColumn += currentTable.CollumnName;
    }
    
    private void DataGrid_OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.AddedItems.Count > 0)
        {
            selectedItem = e.AddedItems[0];
        }
    }
}