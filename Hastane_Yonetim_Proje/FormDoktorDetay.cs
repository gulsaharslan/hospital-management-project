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
    public partial class FormDoktorDetay : Form
    {
        public FormDoktorDetay()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi();
        public string TC;
       
        private void FormDoktorDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TC;    


            //Doktor Ad Soyad Çekme

            SqlCommand komut=new SqlCommand("Select DoktorAd,DoktorSoyad From Table_Doktorlar where DoktorTC=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", lblTC.Text);
            SqlDataReader dr=komut.ExecuteReader();
            while(dr.Read())
            {
                lblAdsoyad.Text = dr[0] + " " + dr[1];
            }
            bgl.baglanti().Close();


            //Randevular
            DataTable dt= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From Table_Randevular where RandevuDoktor='" + lblAdsoyad.Text+ "'", bgl.baglanti());
            da.Fill(dt);
           dataGridView1.DataSource = dt;

        }

        private void btnDuyuru_Click(object sender, EventArgs e)
        {
            FormDuyurular frm=new FormDuyurular();
            frm.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            Application.DoEvents();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            rtxtSikayet.Text = dataGridView1.Rows[secilen].Cells[7].Value.ToString();
        }
    }
}
