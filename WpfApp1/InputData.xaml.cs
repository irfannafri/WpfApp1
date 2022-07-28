using System;
using System.Data;
using System.IO;
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
using Microsoft.Win32;
using WebSocketSharp;
using System.Web.Script.Serialization;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for InputData.xaml
    /// </summary>
    public partial class InputData : Window
    {
        List<string[]> listData = new List<string[]>();
        List<IndividualModel> listPrediksi = new List<IndividualModel>();
       // List<String> listPrediksi = new List<String>();
        private WebSocket ws;
        private JavaScriptSerializer serializer;

        public InputData()
        {
            InitializeComponent();
            ws = new WebSocket("ws://127.0.0.1:8000/");
            ws.OnMessage += onMessage;
            // inisialisasi json parser
            serializer = new JavaScriptSerializer();
        }
       private void onMessage(object sender, MessageEventArgs e)
        {
            if (e.IsText)
            {
                
                var data = new JavaScriptSerializer().Deserialize<double[]>(e.Data);
                IndividualModel individualModel = new IndividualModel()

                {
                    IDPerjalanan = "ID " + listPrediksi.Count,
                    Prediksi = Convert.ToInt32(data[0]),
                    Accuracy = data[1],
                    Bearing = data[2],
                    Acx = data[3],
                    Acy = data[4],
                    Acz = data[5],
                    Gyrox = data[6],
                    Gyroy = data[7],
                    Gyroz = data[8],
                    Second = data[9],
                    Speed = data[10],
                };
                listPrediksi.Add(individualModel);
            }
            if (listPrediksi.Count == listData.Count)
            {
                // memanggil form ListHasilKlasifikasi di main thread
                Application.Current.Dispatcher.Invoke((Action)delegate {
                    HasilKlasifikasi listHasilKlasifikasi = new HasilKlasifikasi(listPrediksi);
                    listHasilKlasifikasi.Show();
                    this.Close();
                });
            }   
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".csv";
            dlg.Filter = "comma separated value Files (*.csv)|*.csv";


            // Tampilkan OpenFileDialog
            Nullable<bool> result = dlg.ShowDialog();


            
            if (result == true)
            {
                // Baca Document
                string filename = dlg.FileName;
                namaFile.Text = filename;
                using (var reader = new StreamReader(filename))
                {
                    listData.Clear();
                    var line = reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        line = reader.ReadLine();
                        var values = line.Split(',');
                        listData.Add(values);
                    }
                    // tampilkan jumlah data
                    jmlData.Text = "" + listData.Count;
                }
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            listPrediksi.Clear();
            if (!ws.IsAlive)
            {
                ws.Connect();
            }
            foreach (var item in listData)
            {
                var dataToSend = serializer.Serialize(item);
                ws.Send(dataToSend);
            }
        }
    }
}
