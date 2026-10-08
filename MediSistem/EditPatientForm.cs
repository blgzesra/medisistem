using MySql.Data.MySqlClient;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace MediSistem
{
    public partial class EditPatientForm : Form
    {
        private int patientID;

        // Seçili hastanın ID'si ile formu başlat
        public EditPatientForm(int id)
        {
            InitializeComponent();
            patientID = id;

            LoadPatientData();
        }

        private void LoadPatientData()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql = "SELECT * FROM patients WHERE id=@id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", patientID);

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBox1.Text = reader["tc_no"].ToString();
                            textBox2.Text = reader["adsoyad"].ToString();
                            textBox3.Text = reader["telefon"].ToString();
                            textBox4.Text = reader["tani"].ToString();
                            textBox5.Text = reader["doktor"].ToString();

                            // ÖNEMLİ: Combobox'ta SelectedItem yerine Text kullanıyoruz
                            comboBox1.Text = reader["durum"].ToString();

                            dateTimePicker1.Value =
                                Convert.ToDateTime(reader["son_ziyaret"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta bilgileri yüklenirken hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql = "UPDATE patients SET " +
                                 "tc_no=@tc, adsoyad=@adsoyad, telefon=@tel, tani=@tani, doktor=@doktor, " +
                                 "durum=@durum, son_ziyaret=@son WHERE id=@id";

                    MySqlCommand cmd = new MySqlCommand(sql, conn);

                    cmd.Parameters.AddWithValue("@tc", textBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@adsoyad", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@tel", textBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@tani", textBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@doktor", textBox5.Text.Trim());

                    // BURASI DÜZELTİLDİ: SelectedItem yerine Text kullanıyoruz
                    string durumText = comboBox1.Text.Trim();
                    if (string.IsNullOrWhiteSpace(durumText))
                    {
                        durumText = "Belirtilmedi"; // istersen başka varsayılan yaz
                    }
                    cmd.Parameters.AddWithValue("@durum", durumText);

                    cmd.Parameters.AddWithValue("@son",
                        dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@id", patientID);

                    int sonuc = cmd.ExecuteNonQuery();

                    if (sonuc > 0)
                    {
                        MessageBox.Show("Hasta başarıyla güncellendi ✅", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Güncelleme yapılamadı!", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme sırasında hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void AddPatientForm_Load(object sender, EventArgs e)
        {
            
        }

        
    }
}
