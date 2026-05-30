using System.Windows;
using System.Windows.Controls;
using TaskManager_Sabitov.ViewModels;

namespace TaskManager_Sabitov
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instance { get; private set; }
        public MainWindow()
        {
            InitializeComponent();
            Instance = this;
            DataContext = new VM_Pages();
        }
        public void OpenPage(Page page)
        {
            frame.Navigate(page);
        }
    }
}