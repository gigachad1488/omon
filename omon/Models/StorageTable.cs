using System;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using DynamicData;
using MsBox.Avalonia;
using omon.ViewModels;
using omon.Views;

namespace omon.Models;

public class StorageTable : ITable
{
    public ObservableCollection<Storage> Storages = new ObservableCollection<Storage>();

    public Window owner { get; set; }

    public StorageTable()
    {
        Refresh();
        MainWindowViewModel.view.Filter = Filter;
    }
    
    public void CollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e)
    {
        switch (e.PropertyName)
        {
            case "name":
                e.Column.Header = "Название";
                break;
            case "Address":
                e.Column.Header = "Адресс";
                break;
            default:
                e.Column.Header = "хз";
                break;
        }
    }
    
    public bool Filter(Object item)
    {
        Storage storage;
        if (item is Storage)
        {
            storage = (Storage)item;
            if (MainWindowViewModel.filterText != null && MainWindowViewModel.filterText != string.Empty)
            {
                if (storage.name.Contains(MainWindowViewModel.filterText) ||
                    storage.address.Contains(MainWindowViewModel.filterText))
                {
                    return true;
                }

                return false;
            }

            return false;
        }

        return false;
    }

    public void Refresh()
    {
        Storages.Clear();
        Storages.AddRange(Db.GetAllStorages());
        MainWindowViewModel.view = new DataGridCollectionView(Storages);
        MainWindowViewModel.view.Refresh();
    }
    
    public void AddWindow()
    {
        AddStorageWindow aw = new AddStorageWindow();
        aw.Closed += delegate { Refresh(); };
        aw.ShowDialog(owner);
    }

    public void RedactWindow(Object item)
    {
        Storage storage = (Storage)item;
        AddStorageWindow aw = new AddStorageWindow(storage);
        aw.Closed += delegate { Refresh(); };
        aw.ShowDialog(owner);
    }

    public async void DeleteWindow(Object item)
    {
        Storage storage = (Storage)item;
        var mBox = MessageBoxManager.GetMessageBoxStandard("Удаление", "Удалить запись?", MsBox.Avalonia.Enums.ButtonEnum.YesNo);
        var result = await mBox.ShowAsPopupAsync(owner);

        if (result == MsBox.Avalonia.Enums.ButtonResult.Yes)
        { 
            Db.DeleteStorage(storage);
            Refresh();
        }
        //Debug.WriteLine(clientsDataGrid.Columns.Count);
    }
}