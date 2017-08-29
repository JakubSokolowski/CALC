using Calc.Core;
using System.Windows;

namespace Calc.Desktop
{ 
    public partial class MainWindow : Window
    {       
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new WindowViewModel(this);
        }
    }
}
