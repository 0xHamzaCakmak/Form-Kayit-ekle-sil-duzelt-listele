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

namespace ornek_kayıt_sil_ekle_duzenle
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=Kütüphane;Integrated Security=True");
        private void verilerigoruntule()
        {
            listView1.Items.Clear();
            listView1.Items.Clear();
            listView1.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from Kitaplar", baglanti);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = oku["id"].ToString();
                ekle.SubItems.Add(oku["KitapAd"].ToString());
                ekle.SubItems.Add(oku["Yazar"].ToString());
                ekle.SubItems.Add(oku["Yayınevi"].ToString());
                ekle.SubItems.Add(oku["Sayfa"].ToString());
                listView1.Items.Add(ekle);
            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            verilerigoruntule();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Kitaplar(id, KitapAd, Yazar, Yayınevi, Sayfa) values('" + textBox1.Text.ToString() + "','" + textBox2.Text.ToString() + "','" + textBox3.Text.ToString() + "','" + textBox4.Text.ToString() + "','" + textBox5.Text.ToString()+ "')",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoruntule();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }
        int id = 0;
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from Kitaplar where id=("+id+")",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoruntule();
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            id = int.Parse(listView1.SelectedItems[0].SubItems[0].Text);
            textBox1.Text = listView1.SelectedItems[0].SubItems[0].Text;
            textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;
            textBox3.Text = listView1.SelectedItems[0].SubItems[2].Text;
            textBox4.Text = listView1.SelectedItems[0].SubItems[3].Text;
            textBox5.Text = listView1.SelectedItems[0].SubItems[4].Text;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update Kitaplar set id='" + textBox1.Text.ToString() + "',KitapAd='" + textBox2.Text.ToString() + "',Yazar='" + textBox3.Text.ToString() + "',Yayınevi='" + textBox4.Text.ToString() + "',Sayfa='" + textBox5.Text.ToString() + "' where id=" + id+"", baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            verilerigoruntule();
        }
    }
}
