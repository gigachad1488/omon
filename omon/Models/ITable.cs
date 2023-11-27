using System;
using Avalonia.Collections;
using Avalonia.Controls;

namespace omon.Models;

public interface ITable
{
    public bool Filter(Object item); //сигма фильтр 2000
    
    public void CollumnName(object? sender, DataGridAutoGeneratingColumnEventArgs e); //чтобы крутые имена при autogeneratecolumn а не всякое англиское
    
    public Window owner { get; set; } //для дальнейшего наиндусивания

    public void AddWindow();
    public void RedactWindow(Object item);
    public void DeleteWindow(Object item);
}