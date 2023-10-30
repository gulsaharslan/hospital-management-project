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

namespace Hastane_Yonetim_Proje
{
    public partial class FormSekreterDetay : Form
    {
        public FormSekreterDetay()
        {
            InitializeComponent();
        }

        public string TCnumara;
        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FormSekreterDetay_Load(object sender, EventArgs e)
        {
         lblTC.Text = TCnumara;

            // Ad Soyad Taşıma

            SqlCommand komut1= new SqlCommand("Select SekreterAdSoyad From Table_Sekreter where SekreterTc=@p1", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr1= komut1.ExecuteReader();

            while (dr1.Read())
            {
                lblAd.Text = dr1[0].ToString();


            }
            bgl.baglanti().Close();

            //Branş Data Grid Aktarım

            DataTable dt1=new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Table_Branslar", bgl.baglanti());
            da.Fill(dt1);
           dataGridView1.DataSource = dt1;


            //Doktor Data Grid Aktarma

            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("Select (DoktorAd+ ' ' + DoktorSoyad) as 'Doktorlar', DoktorBrans From Table_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // Branş Combobox Aktarma
            SqlCommand komut2 = new SqlCommand("Select BransAd From Table_Branslar", bgl.baglanti());
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                comboBrans.Items.Add(dr2[0].ToString());
            }
            bgl.baglanti().Close();
        }

        //  Randevu Oluşturma
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutkaydet = new SqlCommand("insert into Table_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@r1,@r2,@r3,@r4)",bgl.baglanti());
            komutkaydet.Parameters.AddWithValue("@r1", mskTarih.Text);
            komutkaydet.Parameters.AddWithValue("@r2", mskSaat.Text);
            komutkaydet.Parameters.AddWithValue("@r3", comboBrans.Text);
            komutkaydet.Parameters.AddWithValue("@r4", comboDoktor.Text);
            komutkaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu");
           
        }

        private void comboBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("Select DoktorAd, DoktorSoyad From Table_Doktorlar where DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", comboBrans.Text);
            SqlDataReader dr=komut.ExecuteReader();
           while (dr.Read())
            {
                comboDoktor.Items.Add(dr[0] + " "+ dr[1]);
            }

            bgl.baglanti().Close();
        }

        private void btnOlus_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Table_Duyurular (duyuru) values (@d1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@d1", rtxtDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu.");


        }

        private void btnDoktorpanel_Click(object sender, EventArgs e)
            
        {
            FormDoktorPaneli drp=new FormDoktorPaneli();
            drp.Show();

        }

        private void btnBranspanel_Click(object sender, EventArgs e)
        {
            FormBransPaneli brm=new FormBransPaneli();
            brm.Show();
        }

        private void btnRandevulis_Click(object sender, EventArgs e)
        {
            FormRandevuListesi frm=new FormRandevuListesi();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FormDuyurular drm=new FormDuyurular();
            drm.Show();
        }
    }
}
