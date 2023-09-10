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

namespace Ticari_Otomasyon
{
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select urunad,sum(adet) as 'Miktar' from tbl_urunler group by urunad",bgl.baglanti());
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridControl1.DataSource = dt;

            SqlCommand komut = new SqlCommand("select urunad,sum(adet) as 'Miktar' from tbl_urunler group by urunad",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read()) 
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]), int.Parse(dr[1].ToString()));
            }
            bgl.baglanti().Close();

            SqlCommand komut2 = new SqlCommand("select IL, count(*) from tbl_fırmalar group by IL",bgl.baglanti());
            SqlDataReader dr2 = komut2.ExecuteReader();
            while(dr2.Read()) 
            {
                chartControl2.Series["Series 1"].Points.AddPoint(Convert.ToString(dr2[0]), int.Parse(dr2[1].ToString()));
            }
            bgl.baglanti().Close();
        }
    }
}
