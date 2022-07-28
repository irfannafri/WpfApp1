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
    /// Interaction logic for DetailKlasifikasi.xaml
    /// </summary>
    public partial class DetailKlasifikasi : Window
    {
        IndividualModel data = null;
        //String data = null;
        public DetailKlasifikasi(IndividualModel data)
        {
            InitializeComponent();
            this.data = data;
            if (data.Prediksi == 0)
            {
                imgSafe.Visibility = Visibility.Visible;
                
            }
            if (data.Prediksi == 1)
            {
                imgDanger.Visibility = Visibility.Visible;
            }
            listDetail.Items.Add("Accuracy : " + data.Accuracy);
            listDetail.Items.Add("Bearing : " + data.Bearing);
            listDetail.Items.Add("AccelerationX : " + data.Acx);
            listDetail.Items.Add("AccelerationY : " + data.Acy);
            listDetail.Items.Add("AccelerationZ : " + data.Acz);
            listDetail.Items.Add("GyroX : " + data.Gyrox);
            listDetail.Items.Add("GyroY: " + data.Gyroy);
            listDetail.Items.Add("GyroZ: " + data.Gyroz);
            listDetail.Items.Add("Second : " + data.Second);
            listDetail.Items.Add("Speed : " + data.Speed);


        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
