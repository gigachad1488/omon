using Avalonia.Collections;

namespace omon.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public static DataGridCollectionView view { get; set; }
    
    public static string filterText { get; set; }
}