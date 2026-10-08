using System;
using System.Data;
using System.Diagnostics;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace MediSistem
{
    public partial class MuayeneForm : Form
    {
        public MuayeneForm()
        {
            InitializeComponent();

            this.Text = "Bugünkü Muayeneler";

            // Form açılınca bugünün muayenelerini yükle
            LoadTodayExaminations();
        }

        /// Veritabanında son_ziyaret tarihi bugünün tarihi olan hastaları çeker.
       
        private void LoadTodayExaminations()
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
                        "FROM patients " +
                        "WHERE DATE(son_ziyaret) = CURDATE()";

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
                Debug.WriteLine(ex);
                MessageBox.Show(
                    "Bugünkü muayeneler yüklenirken hata oluştu.",
                    "Hata",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        
        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // hata yüzünden eklendi
        }
    }
}

