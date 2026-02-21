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
    public partial class Mudur : Form
    {
        public Mudur()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection(@"server=.\SQLEXPRESS;initial catalog =TarsusUniversity; integrated security = true");
        string BolumID = "";
        string Puan = "0";
        string OgrID = "";
        string OgretmenID = "";
        string DersID = "";
        string SalonID = "";
        string SinavID = "";
        public void bolumGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Bolumler ", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbxBolum.Items.Add(rdr["BolumAdi"].ToString());   
                cmbxOgretmenBolum.Items.Add(rdr["BolumAdi"].ToString());
                cmbxDersBolum.Items.Add(rdr["BolumAdi"].ToString());
            }
            rdr.Close();
            conn.Close();
        }


        public void ogretmenGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Ogretmenler ", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbxGozetmen.Items.Add(rdr["AdSoyad"].ToString());
            }
            rdr.Close();
            conn.Close();
        }

        public void dersGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Dersler ", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbxDers.Items.Add(rdr["DersAdi"].ToString());
            }
            rdr.Close();
            conn.Close();
        }

        public void salonGetir()
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select * from Salonlar ", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                cmbxSalon.Items.Add(rdr["SalonNo"].ToString());
            }
            rdr.Close();
            conn.Close();
        }

       
        public void ogrenciListele()
        {

            conn.Open();
            string sorgu = "select Ogrenciler.ID ,BolumAdi , Adi ,Soyadi,Email from Ogrenciler ,Bolumler where Ogrenciler.BolumID=Bolumler.ID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView1.DataSource = dt;
        }


        public void ogretmenListele()
        {

            conn.Open();
            string sorgu = "select Ogretmenler.ID ,BolumAdi , AdSoyad,TelNo,Email from Ogretmenler ,Bolumler where Ogretmenler.BolumID=Bolumler.ID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView2.DataSource = dt;
        }


        public void sinavListele()
        {

            conn.Open();
            string sorgu = "select Sinav.ID, DersAdi as [Ders Adı],AdSoyad  as [Gözetmen],SinavTarihi  as [Sınav Tarihi],SalonNo as [Salon No]" +
                "from Sinav,Dersler,Ogretmenler,Salonlar where Dersler.ID = Sinav.DersID and Ogretmenler.ID = Sinav.OgretmenID " +
                "and Salonlar.ID =Sinav.SalonID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView5.DataSource = dt;
        }

        public void dersListele()
        {
            conn.Open();
            string sorgu = "select Dersler.ID,BolumAdi,DersAdi from Dersler,Bolumler where Dersler.BolumID = Bolumler.ID";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView3.DataSource = dt;
        }

        public void bolumListele()
        {
            conn.Open();
            string sorgu = "select ID,BolumAdi from Bolumler";
            SqlCommand cmd = new SqlCommand(sorgu, conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView4.DataSource = dt;
        }

        public void ogrenciBolum()
        {
            conn.Open();
            SqlCommand cmdBolumID = new SqlCommand("select ID from Bolumler where BolumAdi=@BolumAdi", conn);
            cmdBolumID.Parameters.AddWithValue("@BolumAdi", cmbxBolum.Text);
            SqlDataReader reader = cmdBolumID.ExecuteReader();

            while (reader.Read())
            {
                BolumID = reader["ID"].ToString();
            }
            reader.Close();
            conn.Close();
        }


        public void ogretmenBolum()
        {
            conn.Open();
            SqlCommand cmdBolumID = new SqlCommand("select ID from Bolumler where BolumAdi=@BolumAdi", conn);
            cmdBolumID.Parameters.AddWithValue("@BolumAdi", cmbxOgretmenBolum.Text);
            SqlDataReader reader = cmdBolumID.ExecuteReader();

            while (reader.Read())
            {
                BolumID = reader["ID"].ToString();
            }
            reader.Close();
            conn.Close();
        }


        public void dersBolum()
        {
            conn.Open();
            SqlCommand cmdBolumID = new SqlCommand("select ID from Bolumler where BolumAdi=@BolumAdi", conn);
            cmdBolumID.Parameters.AddWithValue("@BolumAdi", cmbxDersBolum.Text);
            SqlDataReader reader = cmdBolumID.ExecuteReader();

            while (reader.Read())
            {
                BolumID = reader["ID"].ToString();
            }
            reader.Close();
            conn.Close();
        }
        public void ogrencitemizle()
        {
            cmbxBolum.Text = "";
            txtAd.Text = "";
            txtEmail.Text = "";
            txtSoyad.Text = "";
        }


        public void ogretmentemizle()
        {
            cmbxOgretmenBolum.Text = "";
            txtOgretmenAdSoyad.Text = "";
            txtOgretmenTel.Text = "";
            txtOgretmenEmail.Text = "";
        }

        public void sinavTemizle()
        {
            cmbxDers.Text = "";
            cmbxSalon.Text = "";
            cmbxGozetmen.Text = "";
            dtpTarih.Text = "";
        }

        public void dersTemizle()
        {
            cmbxDersBolum.Text = "";
            txtDersAdi.Text = "";
        }

        public void bolumTemizle()
        {
            txtBolumAdi.Text = "";
        }




        private void Mudur_Load(object sender, EventArgs e)
        {
            bolumGetir();
            ogrenciListele();
            ogretmenListele();
            ogretmenGetir();
            dersGetir();
            salonGetir();
            sinavListele();
            dersListele();
            bolumListele();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {

            conn.Open();
            string sorguK = "insert into Kullanicilar values(@KullaniciAdi,@Sifre,@Yetki)";
            SqlCommand cmd = new SqlCommand(sorguK, conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi",txtEmail.Text);
            cmd.Parameters.AddWithValue("@Sifre", 12345);
            cmd.Parameters.AddWithValue("@Yetki", "ogrenci");
            cmd.ExecuteNonQuery();


            string ID ="";
            SqlCommand cmdKullaniciID = new SqlCommand("select ID from Kullanicilar where KullaniciAdi=@KullaniciAdi", conn);
            cmdKullaniciID.Parameters.AddWithValue("@KullaniciAdi", txtEmail.Text);
            SqlDataReader rdr = cmdKullaniciID.ExecuteReader();
            while (rdr.Read())
            {
                ID = rdr["ID"].ToString();        
            }
            rdr.Close();

            string sorguO = "insert into Ogrenciler values(@KullaniciID,@BolumID,@Adi,@Soyadi ,@Email,@Vize,@Final)";
            SqlCommand command = new SqlCommand(sorguO, conn);
            command.Parameters.AddWithValue("@KullaniciID",int.Parse(ID));
            command.Parameters.AddWithValue("@BolumID", int.Parse(BolumID));
            command.Parameters.AddWithValue("@Adi", txtAd.Text);
            command.Parameters.AddWithValue("@Soyadi", txtSoyad.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);
            command.Parameters.AddWithValue("@Vize", int.Parse(Puan));
            command.Parameters.AddWithValue("@Final", int.Parse(Puan));
            command.ExecuteNonQuery();
            conn.Close();
            ogrenciListele();
            ogrencitemizle();
            MessageBox.Show("Öğrenci Ekleme İşlemi Başarılı", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void btnSil_Click(object sender, EventArgs e)
        {


            conn.Open();

            string KullaniciID = "";
            SqlCommand cmdKullaniciID = new SqlCommand("select KullaniciID from Ogrenciler where ID=@ID", conn);
            cmdKullaniciID.Parameters.AddWithValue("@ID", int.Parse(OgrID));
            SqlDataReader rdr = cmdKullaniciID.ExecuteReader();
            while (rdr.Read())
            {
                KullaniciID = rdr["KullaniciID"].ToString();
            }
            rdr.Close();


            SqlCommand command = new SqlCommand("delete from Ogrenciler where ID = @ID",conn);
            command.Parameters.AddWithValue("@ID", int.Parse(OgrID));
            command.ExecuteNonQuery();


            SqlCommand cmd = new SqlCommand("delete from Kullanicilar where ID =@KullaniciID",conn);
            cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
            cmd.ExecuteNonQuery();


            conn.Close();
            ogrenciListele();
            MessageBox.Show("Öğrenci Başarıyla Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OgrID = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            cmbxBolum.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtAd.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtSoyad.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            txtEmail.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "update Ogrenciler set BolumID=@BolumID , Adi=@Adi,Soyadi=@Soyadi ,Email=@Email where ID=@OgrID";
            SqlCommand command = new SqlCommand(sorgu, conn);            
            command.Parameters.AddWithValue("@OgrID", int.Parse(OgrID));
            command.Parameters.AddWithValue("@BolumID", int.Parse(BolumID));
            command.Parameters.AddWithValue("@Adi", txtAd.Text);
            command.Parameters.AddWithValue("@Soyadi", txtSoyad.Text);
            command.Parameters.AddWithValue("@Email", txtEmail.Text);         
            command.ExecuteNonQuery();
            conn.Close();
            ogrenciListele();
            ogrencitemizle();
            MessageBox.Show("Öğrenci Bilgileri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbxBolum_SelectedIndexChanged(object sender, EventArgs e)
        {
            ogrenciBolum();
           //MessageBox.Show(BolumID);

        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Ogrenciler.ID ,BolumAdi , Adi ,Soyadi,Email from Ogrenciler ,Bolumler where Ogrenciler.BolumID=Bolumler.ID and Adi Like '%" + txtOgrAd.Text + "%' ", conn);
            SqlDataReader rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            conn.Close();
            dataGridView1.DataSource = dt;

        }

        private void btnSifirla_Click(object sender, EventArgs e)
        {
            ogrenciListele();
        }

      
        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OgretmenID = dataGridView2.CurrentRow.Cells[0].Value.ToString();
            cmbxOgretmenBolum.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
            txtOgretmenAdSoyad.Text = dataGridView2.CurrentRow.Cells[2].Value.ToString();
            txtOgretmenTel.Text = dataGridView2.CurrentRow.Cells[3].Value.ToString();
            txtOgretmenEmail.Text = dataGridView2.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnOgretmenEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorguK = "insert into Kullanicilar values(@KullaniciAdi,@Sifre,@Yetki)";
            SqlCommand cmd = new SqlCommand(sorguK, conn);
            cmd.Parameters.AddWithValue("@KullaniciAdi", txtOgretmenEmail.Text);
            cmd.Parameters.AddWithValue("@Sifre", 12345);
            cmd.Parameters.AddWithValue("@Yetki", "ogretmen");
            cmd.ExecuteNonQuery();


            string ID = "";
            SqlCommand cmdKullaniciID = new SqlCommand("select ID from Kullanicilar where KullaniciAdi=@KullaniciAdi", conn);
            cmdKullaniciID.Parameters.AddWithValue("@KullaniciAdi", txtOgretmenEmail.Text);
            SqlDataReader rdr = cmdKullaniciID.ExecuteReader();
            while (rdr.Read())
            {
                ID = rdr["ID"].ToString();
            }
            rdr.Close();

      
            string sorguO = "insert into Ogretmenler values(@BolumID,@KullaniciID,@AdSoyad,@TelNo ,@Email)";
            SqlCommand command = new SqlCommand(sorguO, conn);   
            command.Parameters.AddWithValue("@BolumID", int.Parse(BolumID));
            command.Parameters.AddWithValue("@KullaniciID", int.Parse(ID));
            command.Parameters.AddWithValue("@AdSoyad", txtOgretmenAdSoyad.Text);
            command.Parameters.AddWithValue("@TelNo", txtOgretmenTel.Text);
            command.Parameters.AddWithValue("@Email", txtOgretmenEmail.Text);          
            command.ExecuteNonQuery();
            conn.Close();
            ogretmenListele();
            ogretmentemizle();
            MessageBox.Show("Öğretmen Ekleme İşlemi Başarılı","Bilgilendirme",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }

        private void cmbxOgretmenBolum_SelectedIndexChanged(object sender, EventArgs e)
        {      
            ogretmenBolum();       
        }

        private void btnOgretmenGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            string sorgu = "update Ogretmenler set BolumID=@BolumID,AdSoyad=@AdSoyad,TelNo=@TelNo ,Email=@Email where ID=@OgretmenID";
            SqlCommand command = new SqlCommand(sorgu, conn);
            command.Parameters.AddWithValue("@OgretmenID", int.Parse(OgretmenID));
            command.Parameters.AddWithValue("@BolumID", int.Parse(BolumID));
            command.Parameters.AddWithValue("@AdSoyad", txtOgretmenAdSoyad.Text);
            command.Parameters.AddWithValue("@TelNo", txtOgretmenTel.Text);
            command.Parameters.AddWithValue("@Email", txtOgretmenEmail.Text);
            command.ExecuteNonQuery();
            conn.Close();
            ogretmenListele();
            ogretmentemizle();
            MessageBox.Show("Öğretmen Bilgileri Güncellendi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnOgretmenSil_Click(object sender, EventArgs e)
        {
            conn.Open();

            string KullaniciID = "";
            SqlCommand cmdKullaniciID = new SqlCommand("select KullaniciID from Ogretmenler where ID=@ID", conn);
            cmdKullaniciID.Parameters.AddWithValue("@ID", int.Parse(OgretmenID));
            SqlDataReader rdr = cmdKullaniciID.ExecuteReader();
            while (rdr.Read())
            {
                KullaniciID = rdr["KullaniciID"].ToString();
            }
            rdr.Close();


            SqlCommand command = new SqlCommand("delete from Ogretmenler where ID = @ID", conn);
            command.Parameters.AddWithValue("@ID", int.Parse(OgretmenID));
            command.ExecuteNonQuery();


            SqlCommand cmd = new SqlCommand("delete from Kullanicilar where ID =@KullaniciID", conn);
            cmd.Parameters.AddWithValue("@KullaniciID", KullaniciID);
            cmd.ExecuteNonQuery();


            conn.Close();
            ogretmenListele();
            MessageBox.Show("Öğretmen Başarıyla Silindi", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnSinavEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Sinav values(@DersID,@OgretmenID,@SinavTarihi,@SalonID)",conn);
            cmd.Parameters.AddWithValue("@DersID",int.Parse(DersID));
            cmd.Parameters.AddWithValue("@OgretmenID", int.Parse(OgretmenID));
            cmd.Parameters.AddWithValue("@SinavTarihi", dtpTarih.Value);
            cmd.Parameters.AddWithValue("@SalonID", int.Parse(SalonID));
            cmd.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Sınav Kaydı Başarıyla Oluşturuldu","Bilgilendirme",MessageBoxButtons.OK, MessageBoxIcon.Information);
            sinavListele();
            sinavTemizle();
        }

        
        private void cmbxDers_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select ID from Dersler where DersAdi = @DersAdi", conn);
            cmd.Parameters.AddWithValue("@DersAdi", cmbxDers.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                DersID = rdr["ID"].ToString();             
            }
            rdr.Close();
            conn.Close();
       
        }

        private void cmbxSalon_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select ID from Salonlar where SalonNo = @SalonNo", conn);
            cmd.Parameters.AddWithValue("@SalonNo", cmbxSalon.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                SalonID = rdr["ID"].ToString();
            }
            rdr.Close();
            conn.Close();
        }

        private void cmbxGozetmen_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Select ID from Ogretmenler where AdSoyad = @AdSoyad", conn);
            cmd.Parameters.AddWithValue("@AdSoyad", cmbxGozetmen.Text);
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                OgretmenID = rdr["ID"].ToString();
            }
            rdr.Close();
            conn.Close();
        }


        private void btnDersEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Dersler values(@BolumID,@DersAdi)", conn);
            cmd.Parameters.AddWithValue("@BolumID",int.Parse(BolumID));
            cmd.Parameters.AddWithValue("@DersAdi",txtDersAdi.Text);
            cmd.ExecuteNonQuery();
            conn.Close() ;
            dersListele();
            dersTemizle();


        }

        private void cmbxDersBolum_SelectedIndexChanged(object sender, EventArgs e)
        {
            dersBolum();
        }

        private void btnBolumEkle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Insert into Bolumler values(@BolumAdi)", conn);
            cmd.Parameters.AddWithValue("@BolumAdi", txtBolumAdi.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            bolumListele();
            bolumTemizle();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DersID = dataGridView3.CurrentRow.Cells[0].Value.ToString();
            cmbxDersBolum.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            txtDersAdi.Text= dataGridView3.CurrentRow.Cells[2].Value.ToString();
        }

        private void dataGridView5_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            SinavID = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            cmbxDers.Text = dataGridView5.CurrentRow.Cells[1].Value.ToString();        
            cmbxGozetmen.Text = dataGridView5.CurrentRow.Cells[2].Value.ToString();
            dtpTarih.Text = dataGridView5.CurrentRow.Cells[3].Value.ToString();
            cmbxSalon.Text = dataGridView5.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnSinavGüncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Sinav set DersID=@DersID,OgretmenID=@OgretmenID,SinavTarihi=@SinavTarihi,SalonID=@SalonID where ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", SinavID);
            cmd.Parameters.AddWithValue("@DersID",DersID);
            cmd.Parameters.AddWithValue("@OgretmenID", OgretmenID);
            cmd.Parameters.AddWithValue("@SinavTarihi",dtpTarih.Value);
            cmd.Parameters.AddWithValue("@SalonID", SalonID);
            cmd.ExecuteNonQuery();
            conn.Close();
            sinavListele();
            sinavTemizle();
        }

        private void btnSinavSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from sinav where ID=@ID", conn);
            cmd.Parameters.AddWithValue("@ID", SinavID);
            cmd.ExecuteNonQuery();
            conn.Close();
            sinavListele();
            sinavTemizle();
        }

        private void btnDersGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Dersler set BolumID=@BolumID,DersAdi=@DersAdi where ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", DersID);
            cmd.Parameters.AddWithValue("@BolumID", BolumID);
            cmd.Parameters.AddWithValue("@DersAdi", txtDersAdi.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            dersListele(); 
            dersTemizle();
        }

        private void btnDersSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Delete from Dersler where ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", DersID);           
            cmd.ExecuteNonQuery();
            conn.Close();
            dersListele();
            dersTemizle();
        }

        private void dataGridView4_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            BolumID = dataGridView4.CurrentRow.Cells[0].Value.ToString();
            txtBolumAdi.Text = dataGridView4.CurrentRow.Cells[1].Value.ToString();
        }

        private void btnBolumGuncelle_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Update Bolumler set BolumAdi=@BolumAdi where ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", BolumID);
            cmd.Parameters.AddWithValue("@BolumAdi", txtBolumAdi.Text);
            cmd.ExecuteNonQuery();
            conn.Close();
            bolumListele();
            bolumTemizle();
        }

        private void btnBolumSil_Click(object sender, EventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("delete from Bolumler where ID = @ID", conn);
            cmd.Parameters.AddWithValue("@ID", BolumID);
            cmd.ExecuteNonQuery();
            conn.Close();
            bolumListele();
            bolumTemizle();
        }

        private void btnOgretmenAra_Click(object sender, EventArgs e)
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
