using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for HasilKlasifikasi.xaml
    /// </summary>
    public partial class HasilKlasifikasi : Window
    {
        List<IndividualModel> data;
        //List<String> data;
        public HasilKlasifikasi(List<String> data)
        {
            InitializeComponent();
            
            this.data = data;
            datagridList.ItemsSource = this.data;
        }
        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            DetailKlasifikasi testDetail = new DetailKlasifikasi(data.ElementAt(datagridList.SelectedIndex));
            testDetail.ShowDialog();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
