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
    public partial class FormHastaDetay : Form
    {
        public FormHastaDetay()
        {
            InitializeComponent();
        }

        public string tc;
        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FormHastaDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = tc;


            // Ad Soyad Çekme

            SqlCommand komut = new SqlCommand("Select HastaAd,HastaSoyad From Table_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr=   komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdsoyad.Text = dr[0] +" "+dr[1];
            }

            bgl.baglanti().Close();


            //Randevu Geçmişi
            DataTable dt= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Table_Randevular where HastaTC=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;


            // Branşları Çekme
            SqlCommand komut2= new SqlCommand("Select BransAd From Table_Branslar", bgl.baglanti() );
            SqlDataReader dr2= komut2.ExecuteReader();
            while(dr2.Read())
            {
                comboBrans.Items.Add(dr2[0]);
            }

            bgl.baglanti().Close();
        }

        private void comboBrans_SelectedIndexChanged(object sender, EventArgs e)

        {
         // Branş Çekme
            comboDoktor.Items.Clear();
            SqlCommand komut3 = new SqlCommand("Select DoktorAd, DoktorSoyad From Table_Doktorlar where DoktorBrans=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", comboBrans.Text);
            SqlDataReader dr3= komut3.ExecuteReader();
            while(dr3.Read())
            {
                comboDoktor.Items.Add(dr3[0] + " "+ dr3[1]);
            }
            bgl.baglanti().Close();
        }


        // Randevu Görüntüleme
        private void comboDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt= new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select * From Table_Randevular where RandevuBrans='" + comboBrans.Text + "'" + " and RandevuDoktor='" + comboDoktor.Text+ "'" , bgl.baglanti()); ;
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }


        // Hasta Bilgi Düzenleme
        private void linklblBilgi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormBilgiDüzenle fr=new FormBilgiDüzenle();
            fr.TCno=lblTC.Text;
            fr.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView2.SelectedCells[0].RowIndex;
            txtID.Text = dataGridView2.Rows[secilen].Cells[0].Value.ToString();
        }

        private void btnRandevu_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Table_Randevular set RandevuDurum=1, HastaTC=@p1,HastaSikayet=@p2 where Randevuid=@p3 ", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            komut.Parameters.AddWithValue("@p2", rtxtsikayet.Text);
            komut.Parameters.AddWithValue("@p3", txtID.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Alındı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
