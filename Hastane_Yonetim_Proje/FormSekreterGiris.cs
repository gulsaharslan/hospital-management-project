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
    public partial class FormSekreterGiris : Form
    {
        public FormSekreterGiris()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl=new sqlbaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * From Table_Sekreter where SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", maskedTextBox1.Text);
            komut.Parameters.AddWithValue("@p2", textBox1.Text);
            SqlDataReader dr= komut.ExecuteReader();
            if (dr.Read())
            {
                FormSekreterDetay frs=new FormSekreterDetay();
                frs.TCnumara=maskedTextBox1.Text; 
                frs.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı T.C. veya Şifre");
            }

            bgl.baglanti().Close();
        }
    }
}
