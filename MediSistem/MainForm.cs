using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MediSistem
{
    public partial class MainForm : Form
    {
        private PrintDocument printDocument = new PrintDocument();
        private int printRowIndex = 0;

        public MainForm()
        {
            InitializeComponent();

            this.Text = "MediSistem - Hasta Kayıt Takip";

            printDocument.PrintPage += printDocument_PrintPage;

            LoadPatients();

            LoadPatients();
            StyleGrid();

        }
        private void StyleGrid()
        {
            var grid = guna2DataGridView1; 

            grid.EnableHeadersVisualStyles = false;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;
            grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;

            // Başlıklar
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(17, 24, 39);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            grid.ColumnHeadersHeight = 40;
            grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Satırlar
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.ForeColor = Color.FromArgb(31, 41, 55);
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254); // açık mavi
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 64, 175);
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9);

            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(249, 250, 251);

            grid.RowTemplate.Height = 32;
            grid.GridColor = Color.FromArgb(229, 231, 235);
        }


        
        /// Tüm hastaları veritabanından çeker ve ana listedeki grid'e doldurur.
        
        private void LoadPatients()
        {
            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql =
                        "SELECT " +
                        "id, " +
                        "tc_no AS 'TC Kimlik', " +
                        "adsoyad AS 'Hasta Adı Soyadı', " +
                        "telefon AS 'Telefon', " +
                        "tani AS 'Tanı / Şikayet', " +
                        "doktor AS 'Doktor', " +
                        "durum AS 'Durum', " +
                        "son_ziyaret AS 'Son Ziyaret' " +
                        "FROM patients";

                    MySqlDataAdapter adapter = new MySqlDataAdapter(sql, conn);
                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    guna2DataGridView1.DataSource = table;
                    guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    guna2DataGridView1.ReadOnly = true;
                    guna2DataGridView1.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Hasta listesi yüklenirken bir hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

      
        /// TC, Ad Soyad veya Doktor adına göre arama yapar.
      
        private void SearchPatients(string keyword)
        {
            keyword = keyword.Trim();

            if (string.IsNullOrWhiteSpace(keyword))
            {
                LoadPatients();
                return;
            }

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql =
                        "SELECT " +
                        "id, " +
                        "tc_no AS 'TC Kimlik', " +
                        "adsoyad AS 'Hasta Adı Soyadı', " +
                        "telefon AS 'Telefon', " +
                        "tani AS 'Tanı / Şikayet', " +
                        "doktor AS 'Doktor', " +
                        "durum AS 'Durum', " +
                        "son_ziyaret AS 'Son Ziyaret' " +
                        "FROM patients " +
                        "WHERE tc_no LIKE @k " +
                        "   OR adsoyad LIKE @k " +
                        "   OR doktor LIKE @k";

                    MySqlDataAdapter da = new MySqlDataAdapter(sql, conn);
                    da.SelectCommand.Parameters.AddWithValue("@k", "%" + keyword + "%");

                    DataTable table = new DataTable();
                    da.Fill(table);

                    guna2DataGridView1.DataSource = table;
                    guna2DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    guna2DataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    guna2DataGridView1.ReadOnly = true;
                    guna2DataGridView1.MultiSelect = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Arama sırasında bir hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        
        /// Yazdırma sırasında her sayfayı çizmek için kullanılır.
        
        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int leftMargin = e.MarginBounds.Left;
            int topMargin = e.MarginBounds.Top;
            int y = topMargin;

            Font headerFont = new Font("Segoe UI", 10, FontStyle.Bold);
            Font cellFont = new Font("Segoe UI", 9);

            int x = leftMargin;

            // Sütun başlıkları
            foreach (DataGridViewColumn col in guna2DataGridView1.Columns)
            {
                if (!col.Visible) continue;

                e.Graphics.DrawString(col.HeaderText, headerFont, Brushes.Black, x, y);
                x += col.Width;
            }

            y += 30;

            // Satırlar
            while (printRowIndex < guna2DataGridView1.Rows.Count)
            {
                DataGridViewRow row = guna2DataGridView1.Rows[printRowIndex];
                if (row.IsNewRow)
                {
                    printRowIndex++;
                    continue;
                }

                x = leftMargin;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (!cell.OwningColumn.Visible) continue;

                    string text = Convert.ToString(cell.Value);
                    e.Graphics.DrawString(text, cellFont, Brushes.Black, x, y);
                    x += cell.OwningColumn.Width;
                }

                y += 20;

                if (y > e.MarginBounds.Bottom - 20)
                {
                    e.HasMorePages = true;
                    return;
                }

                printRowIndex++;
            }

            printRowIndex = 0;
            e.HasMorePages = false;
        }

        // ================== OLAY METOTLARI ==================


        /// Yeni Hasta butonu - AddPatientForm'u açar.
       
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            using (AddPatientForm frm = new AddPatientForm())
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients();
                }
            }
        }

        
        /// Bugünkü muayeneler butonu - muayeneForms penceresini açar.
        
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            using (MuayeneForm frm = new MuayeneForm())
            {
                frm.ShowDialog();
            }
        }

       
        /// Seçili hastayı güncelle - EditPatientForm ile.
        
        private void button1_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellemek için bir hasta seçin!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = guna2DataGridView1.SelectedRows[0];

            int id;
            try
            {
                if (row.Cells["id"] != null && row.Cells["id"].Value != null)
                    id = Convert.ToInt32(row.Cells["id"].Value);
                else
                    id = Convert.ToInt32(row.Cells[0].Value);
            }
            catch
            {
                MessageBox.Show("Seçili hastanın ID bilgisi okunamadı.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (EditPatientForm frm = new EditPatientForm(id))
            {
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    LoadPatients();
                }
            }
        }

        
        /// Seçili hastayı sil.
     
        private void button3_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek için bir hasta seçin!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataGridViewRow row = guna2DataGridView1.SelectedRows[0];

            int id;
            try
            {
                if (row.Cells["id"] != null && row.Cells["id"].Value != null)
                    id = Convert.ToInt32(row.Cells["id"].Value);
                else
                    id = Convert.ToInt32(row.Cells[0].Value);
            }
            catch
            {
                MessageBox.Show("Seçili hastanın ID bilgisi okunamadı.", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult dr = MessageBox.Show(
                "Seçili hastayı silmek istediğinize emin misiniz?",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dr != DialogResult.Yes)
                return;

            try
            {
                using (var conn = DbHelper.GetConnection())
                {
                    string sql = "DELETE FROM patients WHERE id = @id";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@id", id);

                    int rows = cmd.ExecuteNonQuery();

                    if (rows > 0)
                    {
                        MessageBox.Show("Hasta kaydı başarıyla silindi.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

                        LoadPatients();
                    }
                    else
                    {
                        MessageBox.Show("Hasta kaydı silinemedi.", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hasta silinirken bir hata oluştu:\n" + ex.Message,
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        /// Arama kutusuna yazıldıkça filtreleme.
       
        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            SearchPatients(txtAra.Text);
        }


        /// Yazdır butonu.
        
        private void btnYazdir_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Yazdırılacak kayıt yok.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (PrintDialog dlg = new PrintDialog())
            {
                dlg.Document = printDocument;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    printRowIndex = 0;
                    printDocument.Print();
                }
            }
        }

        
        /// PDF indir butonu - Microsoft Print to PDF ile.
       
        private void btnPdfIndir_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("PDF'e aktarılacak kayıt yok.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printRowIndex = 0;
                printDocument.Print(); // Windows burada PDF kaydetme penceresi açacak
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "'Microsoft Print to PDF' yazıcısı bulunamadı veya bir hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // ========== Designer'ın daha önce bağladığı ama kullanmadığım eventler için boş metotlar ==========

        private void label1_Click(object sender, EventArgs e)
        {
            // Tasarımda label'a tıklama atandığı için hata vermesin diye boş.
        }

        

    

        private void button4_Click(object sender, EventArgs e)
        {
         
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("PDF'e aktarılacak kayıt yok.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                printDocument.PrinterSettings.PrinterName = "Microsoft Print to PDF";
                printRowIndex = 0;
                printDocument.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "'Microsoft Print to PDF' yazıcısı bulunamadı veya hata oluştu:\n" + ex.Message,
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        

        private void label2_Click(object sender, EventArgs e)
        {
            // kullanmıyoruz
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var cevap = MessageBox.Show(
                        "Oturumu kapatmak istediğinize emin misiniz?",
                        "Çıkış Yap",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

            if (cevap == DialogResult.Yes)
            {
                // Ana formu gizle
                this.Hide();

                // Yeni login penceresi aç
                LoginForm login = new LoginForm();
                login.Show();
            }
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
       
            if (guna2DataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Yazdırılacak kayıt yok.", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (PrintDialog dlg = new PrintDialog())
            {
                dlg.Document = printDocument;

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    printRowIndex = 0;
                    printDocument.Print();
                }
            }
        }

    }
}











