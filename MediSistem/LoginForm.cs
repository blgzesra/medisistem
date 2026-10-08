using System;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace MediSistem
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();

            // Basit bir tema
            this.BackColor = UITheme.Background;
            label1.Text = "E-posta:";
            label2.Text = "Şifre:";

            button1.Text = "Giriş";
            button1.BackColor = UITheme.Primary;
            button1.ForeColor = Color.White;
            button1.FlatStyle = FlatStyle.Flat;
            button1.FlatAppearance.BorderSize = 0;

            // Şifre kutusu noktalar şeklinde gözüksün
            textBox2.UseSystemPasswordChar = true;
        }

        // Giriş butonu olayı 
        
            private void button1_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text.Trim();
            // Şifre olduğu gibi kullanılır (boşluklar şifrenin parçası olabilir)
            string password = textBox2.Text;

            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                MessageBox.Show("E-posta ve şifre boş olamaz!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    // Şifre özeti çekilir, doğrulama PBKDF2 ile uygulamada yapılır
                    string sql = "SELECT adsoyad, password FROM users WHERE email=@e";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@e", email);

                    string adSoyad = null;
                    string storedHash = null;

                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            adSoyad = reader["adsoyad"].ToString();
                            storedHash = reader["password"].ToString();
                        }
                    }

                    if (storedHash != null && PasswordHasher.Verify(password, storedHash))
                    {
                        MessageBox.Show("Giriş başarılı: " + adSoyad, "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        
                        MainForm anaForm = new MainForm();
                        anaForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Hatalı e-posta veya şifre!", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bağlantı hatası:\n" + ex.Message, "Bağlantı Hatası",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


            
        
        private void LoginForm_Load(object sender, EventArgs e)
        {
            // Form açılırken çalışacak
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }

}
