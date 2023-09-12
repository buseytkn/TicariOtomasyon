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
using DevExpress.XtraGrid;
using System.Xml;

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void stoklar()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select urunad,sum(adet) as 'Adet' from tbl_urunler group by urunad having sum(adet)<=20 order by sum(adet)",bgl.baglanti());
            da.Fill(dt);
            GridAzalanStoklar.DataSource=dt;
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select top 10 nottarıh,notsaat,notbaslık from tbl_notlar order by notıd desc",bgl.baglanti());
            da.Fill(dt);
            GridAjanda.DataSource=dt;
        }
        void firmahareketleri()
        {
             DataTable dt = new DataTable();
             SqlDataAdapter da = new SqlDataAdapter("exec FirmaHareket2", bgl.baglanti());
             da.Fill(dt);
             GridSon10.DataSource = dt;
        }
        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select ad,telefon from tbl_fırmalar",bgl.baglanti());
            da.Fill(dt);
            GridFihrist.DataSource=dt;
        }
        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while(xmloku.Read()) 
            {
                if(xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }
        
        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            firmahareketleri();
            fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/kurlar/today.xml");
            haberler();
        }
    }
}
