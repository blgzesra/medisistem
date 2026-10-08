using System;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Drawing;

namespace MediSistem
{
    public partial class LoginForm : Form
    {
        // Art arda hatalı giriş sınırı. Static: çıkış yapıp yeni LoginForm
        // açılsa da sayaç uygulama çalıştığı sürece korunur.
        private const int MaxFailedAttempts = 5;
        private static readonly TimeSpan LockoutDuration = TimeSpan.FromSeconds(30);
        private static int failedAttempts = 0;
        private static DateTime lockoutUntil = DateTime.MinValue;

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
            if (DateTime.Now < lockoutUntil)
            {
                int kalan = (int)Math.Ceiling((lockoutUntil - DateTime.Now).TotalSeconds);
                MessageBox.Show("Çok fazla hatalı deneme yapıldı.\n" + kalan +
                    " saniye sonra tekrar deneyin.", "Giriş Kilitli",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                        failedAttempts = 0;

                        MessageBox.Show("Giriş başarılı: " + adSoyad, "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);


                        MainForm anaForm = new MainForm();
                        anaForm.Show();
                        this.Hide();
                    }
                    else
                    {
                        failedAttempts++;

                        if (failedAttempts >= MaxFailedAttempts)
                        {
                            failedAttempts = 0;
                            lockoutUntil = DateTime.Now.Add(LockoutDuration);
                            MessageBox.Show("Art arda " + MaxFailedAttempts + " hatalı deneme yapıldı.\n" +
                                "Giriş " + (int)LockoutDuration.TotalSeconds + " saniye kilitlendi.",
                                "Giriş Kilitli", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Hatalı e-posta veya şifre!", "Hata",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessageBox.Show("Veritabanına bağlanılamadı. Lütfen bağlantı ayarlarını kontrol edip tekrar deneyin.",
                    "Bağlantı Hatası", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
