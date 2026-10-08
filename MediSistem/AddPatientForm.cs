using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MediSistem
{
    public partial class AddPatientForm : Form
    {
        public AddPatientForm()
        {
            InitializeComponent();

            // Durum combobox'ını doldur (comboBoxDurum)
            if (comboBoxDurum.Items.Count == 0)
            {
                comboBoxDurum.Items.Add("Tedavi Görüyor");
                comboBoxDurum.Items.Add("Randevu Bekliyor");
                comboBoxDurum.Items.Add("Taburcu Edildi");
            }

            if (comboBoxDurum.Items.Count > 0 && comboBoxDurum.SelectedIndex == -1)
                comboBoxDurum.SelectedIndex = 0;
        }

        // KAYDET butonu olayı
        private void buttonkaydet_Click(object sender, EventArgs e)
        {
            string tc = textBoxTc.Text.Trim();
            string adsoyad = textBoxAdSoyad.Text.Trim();
            string telefon = textBoxTelefon.Text.Trim();
            string tani = textBoxTani.Text.Trim();
            string doktor = textBoxDoktor.Text.Trim();
            string durum = comboBoxDurum.SelectedItem != null
                ? comboBoxDurum.SelectedItem.ToString()
                : "";
            string sonZiyaret = dateTimePickerSonZiyaret.Value.ToString("yyyy-MM-dd");

            if (tc == "" || adsoyad == "")
            {
                MessageBox.Show("TC Kimlik ve Ad Soyad alanları boş bırakılamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql = "INSERT INTO patients " +
                                 "(tc_no, adsoyad, telefon, tani, doktor, durum, son_ziyaret) " +
                                 "VALUES (@tc, @adsoyad, @tel, @tani, @doktor, @durum, @son)";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@tc", tc);
                    cmd.Parameters.AddWithValue("@adsoyad", adsoyad);
                    cmd.Parameters.AddWithValue("@tel", telefon);
                    cmd.Parameters.AddWithValue("@tani", tani);
                    cmd.Parameters.AddWithValue("@doktor", doktor);
                    cmd.Parameters.AddWithValue("@durum", durum);
                    cmd.Parameters.AddWithValue("@son", sonZiyaret);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Hasta başarıyla eklendi.", "Başarılı",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Hasta kaydedilemedi!", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Veri kaydedilirken hata oluştu.",
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        // Designer bunları arıyor, o yüzden boş 
        private void label4_Click(object sender, EventArgs e)
        {
            // Boş
        }

        private void label5_Click(object sender, EventArgs e)
        {
            // Boş
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        { 
        
            //boş
        
        }
         
        private void picturBox1_Click_1(object sender, EventArgs e)
        {

            // boş
        }
       
    }
        }


    

