using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace ExcelDosyasınaKayıtYapanKronometre
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        int durum = 0;
        private DispatcherTimer tmrSalise = new DispatcherTimer(DispatcherPriority.Normal);
        private int saniye, dakika, saat, salise = 0,tur=0;
        
        DateTime tarih;

        private List<Veriler> veriler=new List<Veriler>();


        public MainWindow()
        {
            InitializeComponent();
            
            tarih = DateTime.Now;
            tmrSalise.Tick += new EventHandler(TmrSalise_Tick);
            tmrSalise.Interval = new TimeSpan(0, 0, 0, 0, 1);


        }

        private void TmrSalise_Tick(object sender, EventArgs e)
        {
            
            salise++;
            if (salise == 60)
            {
                salise = 0;
                saniye++;
                

            }
            if (saniye >= 60)
            {
                saniye = 0;
                dakika++;
                
            }
            if (dakika >= 60)
            {
                dakika = 0;
                saat++;
                

            }
            tbSüre.Text = saat.ToString() + " : "+ dakika.ToString() + " : "+ saniye.ToString() + " : " + salise.ToString();
            
        }
        private void btnBaslat_Click(object sender, RoutedEventArgs e)
        {
            string süre;
            süre = tbSüre.Text;           
            if (durum==0)
            {
                btnBaslat.Content = "Durdur";
                durum = 1;
                tmrSalise.Start();
               

            }
            else
            {
                btnBaslat.Content = "Başlat";
                tmrSalise.Stop();
                tur++;
                veriler.Add(new Veriler() { Süre = süre, Tarih = tarih.ToString("dd/M/yyyy"), Tur = tur });
                durum = 0;
            }
        }

       

        private void btnTur_Click(object sender, RoutedEventArgs e)
        {
            lv.ItemsSource = null;
            for (int i = 0; i < veriler.Count; i++)
            {
                lv.ItemsSource = veriler;
            }
            
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
