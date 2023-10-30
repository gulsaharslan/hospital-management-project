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
    public partial class FormBilgiDüzenle : Form
    {
        public FormBilgiDüzenle()
        {
            InitializeComponent();
        }

        public string TCno;
        sqlbaglantisi bgl= new sqlbaglantisi();
        private void FormBilgiDüzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand komut= new SqlCommand("Select * From Table_Hastalar where HastaTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            SqlDataReader dr= komut.ExecuteReader   ();
            while (dr.Read()) 
            {
             txtAd.Text= dr[1].ToString();
             txtSoyad.Text= dr[2].ToString();
             mskTel.Text= dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                comboCins.Text = dr[6].ToString();
            }

            bgl.baglanti().Close();
        }

        private void btnBilgiGun_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("update Table_Hastalar set HastaAd=@p1, HastaSoyad=@p2, HastaTel=@p3, HastaSifre=@p4,HastaCins=@p5 where HastaTC=@p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1", txtAd.Text);
            komut2.Parameters.AddWithValue("@p2", txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3", mskTel.Text);
            komut2.Parameters.AddWithValue("@p4", txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5", comboCins.Text);
            komut2.Parameters.AddWithValue("@p6", mskTC.Text);
            komut2.ExecuteNonQuery();

            bgl.baglanti().Close ();
            MessageBox.Show("Bilgileriniz Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        }
    }
}
