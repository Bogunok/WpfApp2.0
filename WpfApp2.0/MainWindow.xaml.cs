using System.Windows;
using WpfApp2._0;

namespace WpfApp2._0
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        PersonViewModel pvm;
        public MainWindow()
        {
            InitializeComponent();
            pvm = new PersonViewModel(this);
            DataContext = pvm;
        }
    }
}
