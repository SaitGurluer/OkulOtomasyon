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
    public partial class Ogretmen : Form
    {
        public Ogretmen()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;initial catalog =TarsusUniversity; integrated security = true");
        string ID;
        public void bilgiGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select AdSoyad,BolumAdi,TelNo,Email from Ogretmenler,Bolumler where KullaniciID=@KullaniciID and Ogretmenler.BolumID=Bolumler.ID ", conn);
            cmd.Parameters.AddWithValue("@KullaniciID", int.Parse(txtID.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                lblAdSoyad.Text = rdr["AdSoyad"].ToString();
                lblBolum.Text = rdr["BolumAdi"].ToString();
                lblTelNo.Text = rdr["TelNo"].ToString();
                lblEmail.Text = rdr["Email"].ToString();


            }
            conn.Close();
        }

        public void ogrenciGetir()
        {

            conn.Open();
            string sorgu = "select Ogrenciler.ID,Adi , Soyadi ,Ogrenciler.Email ,Vize ,Final from Ogrenciler , Ogretmenler where Ogretmenler.KullaniciID=@KullaniciID and Ogrenciler.BolumID = Ogretmenler.BolumID ";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@KullaniciID", int.Parse(txtID.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void Ogretmen_Load(object sender, EventArgs e)
        {
            bilgiGetir();
            ogrenciGetir();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtVize.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            txtFinal.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            txtAdSoyad.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString() + " " + dataGridView1.CurrentRow.Cells[2].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "update Ogrenciler set Vize =@Vize , Final=@Final where Ogrenciler.ID = @ID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            cmd.Parameters.AddWithValue("@ID", int.Parse(ID));
            cmd.Parameters.AddWithValue("@Vize", txtVize.Text);
            cmd.Parameters.AddWithValue("@Final", txtFinal.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            ogrenciGetir();

           
        }

        private void btnSifreDegistir_Click(object sender, EventArgs e)
        {
            SifreGuncelle sifreGuncelle = new SifreGuncelle();
            sifreGuncelle.txtID.Text = txtID.Text;
            sifreGuncelle.lblKullanici.Text = lblAdSoyad.Text;
            sifreGuncelle.Show();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
