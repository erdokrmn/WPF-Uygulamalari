using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using SpreadsheetLight;
using Microsoft.Win32;
using System.IO;

namespace ExcelDosyasınaKayıtYapanKronometre
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        Timer tmrSalise = new Timer();
        string süre;
        bool tarih_ = false;
        int saniye, dakika, saat, salise = 0, tur = 0, durum = 0;
        DateTime tarih;
        List<Veriler> veriler = new List<Veriler>();

        public MainWindow()
        {
            InitializeComponent();
            tarih = DateTime.Now;
            tmrSalise.Tick += TmrSalise_Tick;
            tmrSalise.Interval = 1;
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

            tbSüre.Text = saat.ToString() + " : " + dakika.ToString() + " : " + saniye.ToString() + " : " + salise.ToString();
            
        }
        private void btnBaslat_Click(object sender, RoutedEventArgs e)
        {
            
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
        public void CreateFile(string path)
        {
                
        }
        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.SaveFileDialog saveFile = new Microsoft.Win32.SaveFileDialog();
            //saveFile.DefaultExt = "xlsx";
            //if (saveFile.ShowDialog() == true)
            //{
            //    CreateFile(saveFile.FileName);
            //}
            if (lv.Items.Count > 0)
            {
                SLDocument exc = new SLDocument();
                SLStyle st = exc.CreateStyle();
                st.SetFontBold(true);
                exc.SetCellValue("A1", "Tur");
                exc.SetCellStyle("A1", st);
                exc.SetCellValue("B1", "Geçen Süre");
                exc.SetCellStyle("B1", st);
                exc.SetCellValue("C1", "Tarih");
                exc.SetCellStyle("C1", st);
                int ex_sira = 2;
                foreach (Veriler itemRow in lv.Items)
                {
                    string tur = itemRow.Tur.ToString();
                    string süre = itemRow.Süre;
                    string tarih = itemRow.Tarih;

                        exc.SetCellValue("A" + ex_sira, tur);
                        exc.SetCellValue("B" + ex_sira, süre);
                        exc.SetCellValue("C" + ex_sira, tarih);
                        ex_sira++;
                    
                }
                if (File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "\\CalıstığımSaatler.xlsx"))
                {

                }

                    FolderBrowserDialog fbd = new FolderBrowserDialog();
                DialogResult result = fbd.ShowDialog();
                string a = fbd.SelectedPath + "\\CalıstığımSaatler.xlsx";
                exc.SaveAs(a);
                System.Windows.MessageBox.Show("Dosyanız " + a + " olarak kayıt edildi.");

            }
        }
    }
}
