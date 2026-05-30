using System.Windows.Controls;

namespace TaskManager_Sabitov.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main(Object context)
        {
            InitializeComponent();
            DataContext = context;
        }
    }
}
