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
using DevExpress.Charts;

namespace Ticari_Otomasyon
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void musterihareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("execute musterihareketler",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void firmahareket()
        {
            DataTable dt1 = new DataTable();
            SqlDataAdapter da1 = new SqlDataAdapter("execute firmahareketler",bgl.baglanti());
            da1.Fill(dt1);
            gridControl3.DataSource = dt1;
        }
        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            LblAktifKullanici.Text = ad;
            musterihareket();
            firmahareket(); 

            SqlCommand komut = new SqlCommand("select sum(tutar) from tbl_faturadetay",bgl.baglanti());
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read()) 
            {
                LblKasaToplam.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut1 = new SqlCommand("select (elektrık+su+dogalgaz) from tbl_gıderler order by ID asc",bgl.baglanti());
            SqlDataReader dr1 = komut1.ExecuteReader();
            while (dr1.Read()) 
            {
                LblOdemeler.Text = dr1[0].ToString();
            }
            bgl.baglanti() .Close();

            SqlCommand komut2 = new SqlCommand("select maaslar from tbl_gıderler order by ıd asc",bgl.baglanti());
            SqlDataReader d2 = komut2.ExecuteReader();
            while (d2.Read()) 
            {
                LblPersonelMaaslari.Text = d2[0].ToString();
            }
            bgl.baglanti() .Close();

            SqlCommand komut3 = new SqlCommand("select count(*) from tbl_musterıler", bgl.baglanti());
            SqlDataReader d3 = komut3.ExecuteReader();
            while (d3.Read())
            {
                LblMusteriSayisi.Text = d3[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut4 = new SqlCommand("select count(*) from tbl_fırmalar", bgl.baglanti());
            SqlDataReader d4 = komut4.ExecuteReader();
            while (d4.Read())
            {
                LblFirmaSayisi.Text = d4[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut5 = new SqlCommand("select count(distinct(ıl)) from tbl_fırmalar", bgl.baglanti());
            SqlDataReader d5 = komut5.ExecuteReader();
            while (d5.Read())
            {
                LblSehirSayisi.Text = d5[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut6 = new SqlCommand("select count(distinct(ıl)) from tbl_musterıler", bgl.baglanti());
            SqlDataReader d6 = komut6.ExecuteReader();
            while (d6.Read())
            {
                LblSehirSayisi2.Text = d6[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut7 = new SqlCommand("select count(*) from tbl_personeller", bgl.baglanti());
            SqlDataReader d7 = komut7.ExecuteReader();
            while (d7.Read())
            {
                LblPersonelSayisi.Text = d7[0].ToString();
            }
            bgl.baglanti().Close();

            SqlCommand komut8 = new SqlCommand("select sum(adet) from tbl_urunler", bgl.baglanti());
            SqlDataReader d8 = komut8.ExecuteReader();
            while (d8.Read())
            {
                LblStokSayisi.Text = d8[0].ToString();
            }
            bgl.baglanti().Close();
        }
        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            if(sayac>0 && sayac<=5)
            {
                groupControl11.Text = "Elektrik";
                SqlCommand komut9 = new SqlCommand("select top 4 ay,elektrık from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr9 = komut9.ExecuteReader();
                while (dr9.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
                }
                bgl.baglanti().Close();
            }
            if(sayac>5 && sayac<=10)
            {
                groupControl11.Text = "Su";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,su from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 10 && sayac <= 15)
            {
                groupControl11.Text = "Doğalgaz";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,dogalgaz from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 15 && sayac <= 20)
            {
                groupControl11.Text = "İnternet";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,ınternet from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac > 20 && sayac <= 25)
            {
                groupControl11.Text = "Ekstra";
                chartControl1.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,ekstra from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl1.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if(sayac==26)
            {
                sayac= 0;
            }
        }
        int sayac2 = 0;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl12.Text = "Elektrik";
                SqlCommand komut9 = new SqlCommand("select top 4 ay,elektrık from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr9 = komut9.ExecuteReader();
                while (dr9.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr9[0], dr9[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 5 && sayac2 <= 10)
            {
                groupControl12.Text = "Su";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,su from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 10 && sayac2 <= 15)
            {
                groupControl12.Text = "Doğalgaz";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,dogalgaz from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 15 && sayac2 <= 20)
            {
                groupControl12.Text = "İnternet";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,ınternet from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 > 20 && sayac2 <= 25)
            {
                groupControl12.Text = "Ekstra";
                chartControl2.Series["Aylar"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("select top 4 ay,ekstra from tbl_gıderler order by ıd desc", bgl.baglanti());
                SqlDataReader dr10 = komut10.ExecuteReader();
                while (dr10.Read())
                {
                    chartControl2.Series["Aylar"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(dr10[0], dr10[1]));
                }
                bgl.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
