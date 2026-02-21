using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
namespace SinavTakvimi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;initial catalog =TarsusUniversity; integrated security = true");

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            string KullaniciAdi = txtAd.Text;
            string Sifre = txtSifre.Text;

            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Kullanicilar where KullaniciAdi = @KullaniciAdi and Sifre=@Sifre ", conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi", KullaniciAdi);
            cmd.Parameters.AddWithValue("@Sifre", Sifre);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                if (rdr["Yetki"].ToString() == "ogrenci")
                {
                    Ogrenci ogr = new Ogrenci();
                    ogr.txtID.Text = rdr["ID"].ToString();
                    ogr.Show();
                }
                else if(rdr["Yetki"].ToString() == "ogretmen")
                {
                    Ogretmen ogretmen = new Ogretmen();
                    ogretmen.txtID.Text = rdr["ID"].ToString();
                    ogretmen.Show();
                }
                else if(rdr["Yetki"].ToString() == "mudur")
                {
                    Mudur mudur = new Mudur();
                    mudur.txtID.Text = rdr["ID"].ToString();
                    mudur.Show();
                }
            }

            conn.Close();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
