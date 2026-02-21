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
    public partial class SifreGuncelle : Form
    {
        public SifreGuncelle()
        {
            InitializeComponent();
        }

        SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;initial catalog =TarsusUniversity; integrated security = true");
        string eskiSifre = "";

        public void eskiSifreBul() 
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Sifre from Kullanicilar where ID = @ID ", conn);
            cmd.Parameters.AddWithValue("@ID", int.Parse(txtID.Text));
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                eskiSifre = rdr["Sifre"].ToString();
            }
            conn.Close();
        }
     


        private void SifreGuncelle_Load(object sender, EventArgs e)
        {
            eskiSifreBul();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if(txtEskiSifre.Text == eskiSifre)
            {
                if(txtYeniSifre.Text == txtYeniSifreTekrar.Text) 
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Update Kullanicilar set Sifre = @sifre where ID=@ID",conn);
                    cmd.Parameters.AddWithValue("@sifre", txtYeniSifre.Text);
                    cmd.Parameters.AddWithValue("@ID", int.Parse(txtID.Text));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Şifreniz Başarıyla Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                
                }
            }
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
            Environment.Exit(0);
        }
    }
}
