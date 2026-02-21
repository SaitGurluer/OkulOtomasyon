using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SinavTakvimi
{
    public partial class Ogrenci : Form
    {
        public Ogrenci()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;initial catalog =TarsusUniversity; integrated security = true");

        public void bilgiGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Adi ,Soyadi , Email ,BolumAdi from Ogrenciler,Bolumler where KullaniciID=@KullaniciID and Ogrenciler.BolumID=Bolumler.ID ", conn);
            cmd.Parameters.AddWithValue("@KullaniciID", int.Parse(txtID.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lblAd.Text = rdr["Adi"].ToString();
                lblSoyad.Text = rdr["Soyadi"].ToString();
                lblEmail.Text = rdr["Email"].ToString();
                lblBolum.Text = rdr["BolumAdi"].ToString();
         

            }
            conn.Close();
        }

        public void listele()
        {

            conn.Open();
            string sorgu = "select DersAdi,SinavTarihi,SalonNo,Ogretmenler.AdSoyad AS 'Öğretmen Adı' from Ogrenciler,Bolumler,Dersler,Sinav,Salonlar,Ogretmenler " +
                "where Ogrenciler.KullaniciID=@KullaniciID and Ogrenciler.BolumID=Bolumler.ID and Dersler.BolumID = Bolumler.ID " +
                "and Sinav.DersID=Dersler.ID and Ogretmenler.ID = Sinav.OgretmenID and Sinav.SalonID = Salonlar.ID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@KullaniciID", int.Parse(txtID.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }



        private void Ogrenci_Load(object sender, EventArgs e)
        {
            bilgiGetir();
            listele();
        }

        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            SifreGuncelle sifreGuncelle = new SifreGuncelle();
            sifreGuncelle.txtID.Text = txtID.Text;
            sifreGuncelle.lblKullanici.Text=lblAd.Text+" "+lblSoyad.Text;
            sifreGuncelle.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
