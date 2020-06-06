using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Drawing.Text;

namespace deneme1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        OleDbConnection baglanti = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=bilgiler.mdb");
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text =="Ekle")
            {
                baglanti.Open();
                OleDbCommand komut = new OleDbCommand("insert into kayitbilgileri(id,ad,soyad,adres,telefon,mail) values('" + textBox7.Text + "','" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')", baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarıyla Eklendi", "Kayıt");
                tablo.Clear();
                listele();
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is TextBox)
                    {
                        Controls[i].Text = "";
                    }
                }
            }
             if(button1.Text =="Güncelle")
            {
                baglanti.Open();
                OleDbCommand komut2 = new OleDbCommand("update kayitbilgileri set ad='"+textBox1.Text+ "',soyad='" + textBox2.Text + "',adres='" + textBox3.Text + "',telefon='" + textBox4.Text + "',mail='" + textBox5.Text +"' where id = '"+textBox7.Text+"'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Kayıt Başarıyla Güncellendi Eğer Güncellenmedi ise Kaydı silip Tekrar Oluşturun", "Kayıt");
                tablo.Clear();
                listele();
                for (int i = 0; i < Controls.Count; i++)
                {
                    if (Controls[i] is TextBox)
                    {
                        Controls[i].Text = "";
                    }
                }
                button1.Text="Ekle";
            }
        }
        DataTable tablo = new DataTable();

        private void listele()
        {
            baglanti.Open();
            OleDbDataAdapter adtr=new OleDbDataAdapter("select *from kayitbilgileri", baglanti);
            adtr.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("delete * from kayitbilgileri where ad='"+dataGridView1.CurrentRow.Cells["ad"].Value.ToString()+"' and id='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'  " , baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Başarıyla Silindi", "Kayıt");
            tablo.Clear();
            listele();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbDataAdapter adtr = new OleDbDataAdapter("select *from kayitbilgileri where ad like '"+textBox6.Text+"%' or id like '"+textBox6.Text+ "%'", baglanti);
            DataTable tablo2 = new DataTable();
            adtr.Fill(tablo2);
            dataGridView1.DataSource = tablo2;
            baglanti.Close();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox7.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
            textBox1.Text = dataGridView1.CurrentRow.Cells["ad"].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells["soyad"].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells["adres"].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells["telefon"].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells["mail"].Value.ToString();
            
            button1.Text= "Güncelle";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
 
           
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar)
                && !char.IsSeparator(e.KeyChar);
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
