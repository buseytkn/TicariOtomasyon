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
using DevExpress.XtraBars;

namespace Ticari_Otomasyon
{
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();    
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from tbl_faturabılgı",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            TxtAlici.Text = "";
            TxtId.Text = "";
            TxtSeri.Text = "";
            TxtSiraNo.Text = "";
            TxtTeslimAlan.Text = "";
            TxtTeslimEden.Text = "";
            TxtVergiDaire.Text = "";
            MskSaat.Text = "";
            MskTarih.Text = "";
        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            if(TxtFaturaId.Text == "")
            {
                SqlCommand komut = new SqlCommand("insert into tbl_faturabılgı (serı,sırano,tarıh,saat,vergıdaıre,alıcı,teslımeden,teslımalan) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)",bgl.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@p2", TxtSiraNo.Text);
                komut.Parameters.AddWithValue("@p3", MskTarih.Text);
                komut.Parameters.AddWithValue("@p4", MskSaat.Text);
                komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@p6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
                komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
                komut.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Fatura Bilgisi Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if(TxtFaturaId.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (urunad,mıktar,fıyat,tutar,faturaıd) values (@p1,@p2,@p3,@p4,@p5)",bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut3 = new SqlCommand("insert into tbl_fırmahareketler (urunıd,adet,personel,fırma,fıyat,toplam,faturaıd,tarıh) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8)",bgl.baglanti());
                komut3.Parameters.AddWithValue("@a1", TxtUrunId.Text);
                komut3.Parameters.AddWithValue("@a2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@a3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@a4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@a5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@a6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@a7", TxtFaturaId.Text);
                komut3.Parameters.AddWithValue("@a8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut4 = new SqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2",bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1",TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2",TxtUrunId.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
            if (TxtFaturaId.Text != "" && comboBox1.Text == "Müşteri")
            {
                double miktar, fiyat, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();

                SqlCommand komut2 = new SqlCommand("insert into tbl_faturadetay (urunad,mıktar,fıyat,tutar,faturaıd) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                komut2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@p3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@p4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@p5", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut3 = new SqlCommand("insert into tbl_musterıhareketler (urunıd,adet,personel,musterı,fıyat,toplam,faturaıd,tarıh) values (@a1,@a2,@a3,@a4,@a5,@a6,@a7,@a8)", bgl.baglanti());
                komut3.Parameters.AddWithValue("@a1", TxtUrunId.Text);
                komut3.Parameters.AddWithValue("@a2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@a3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@a4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@a5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@a6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@a7", TxtFaturaId.Text);
                komut3.Parameters.AddWithValue("@a8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                bgl.baglanti().Close();

                SqlCommand komut4 = new SqlCommand("update tbl_urunler set adet=adet-@s1 where ıd=@s2", bgl.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunId.Text);
                komut4.ExecuteNonQuery();
                bgl.baglanti().Close();
                MessageBox.Show("Faturaya Ait Ürün Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtId.Text = dr["FATURABILGIID"].ToString();
                TxtSiraNo.Text = dr["SIRANO"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTeslimEden.Text = dr["TESLIMEDEN"].ToString();
                TxtTeslimAlan.Text = dr["TESLIMALAN"].ToString();
                TxtVergiDaire.Text = dr["VERGIDAIRE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("delete from tbl_faturabılgı where faturabılgııd=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Silindi","Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update tbl_faturabılgı set serı=@p1,sırano=@p2,tarıh=@p3,saat=@p4,vergıdaıre=@p5,alıcı=@p6,teslımeden=@p7,teslımalan=@p8 where faturabılgııd=@p9",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@p2", TxtSiraNo.Text);
            komut.Parameters.AddWithValue("@p3", MskTarih.Text);
            komut.Parameters.AddWithValue("@p4", MskSaat.Text);
            komut.Parameters.AddWithValue("@p5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@p6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@p7", TxtTeslimEden.Text);
            komut.Parameters.AddWithValue("@p8", TxtTeslimAlan.Text);
            komut.Parameters.AddWithValue("@p9",TxtId.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Fatura Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrun fr = new FrmFaturaUrun();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null ) 
            {
                fr.id = dr["FATURABILGIID"].ToString();
            }
            fr.Show();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("select urunad,satısfıyat from tbl_urunler where ıd=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunId.Text);
            SqlDataReader dr = komut.ExecuteReader();
            while(dr.Read()) 
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();
            }
            bgl.baglanti().Close();
        }
    }
}
