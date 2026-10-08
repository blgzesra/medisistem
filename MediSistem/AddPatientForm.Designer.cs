using System;
using System.Drawing;
using System.Windows.Forms;
namespace MediSistem
{
   public partial class AddPatientForm : Form
    {

        private void AddPatientForm_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.sekreter_jpg1;
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
        }

        
        /// Gerekkli tasarım değişkeni
     
        private System.ComponentModel.IContainer components = null;

       
        /// kullanılan tüm kaynakları temizler
        
        /// <param name="disposing">Yönetilen kaynakların elden çıkarılması gerekiyorsa doğru; aksi takdirde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        
        /// Tasarımcı desteği için gerekli metod
        
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTc = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxAdSoyad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxTelefon = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxTani = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDoktor = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.comboBoxDurum = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dateTimePickerSonZiyaret = new System.Windows.Forms.DateTimePicker();
            this.buttonKaydet = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(65, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "TC Kimlik No:";
            // 
            // textBoxTc
            // 
            this.textBoxTc.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxTc.Location = new System.Drawing.Point(234, 79);
            this.textBoxTc.Name = "textBoxTc";
            this.textBoxTc.Size = new System.Drawing.Size(171, 31);
            this.textBoxTc.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(65, 153);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Ad Soyad:";
            // 
            // textBoxAdSoyad
            // 
            this.textBoxAdSoyad.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxAdSoyad.Location = new System.Drawing.Point(234, 145);
            this.textBoxAdSoyad.Name = "textBoxAdSoyad";
            this.textBoxAdSoyad.Size = new System.Drawing.Size(171, 31);
            this.textBoxAdSoyad.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(65, 202);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Telefon:";
            // 
            // textBoxTelefon
            // 
            this.textBoxTelefon.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxTelefon.Location = new System.Drawing.Point(234, 212);
            this.textBoxTelefon.Name = "textBoxTelefon";
            this.textBoxTelefon.Size = new System.Drawing.Size(171, 31);
            this.textBoxTelefon.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(65, 277);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(48, 23);
            this.label4.TabIndex = 6;
            this.label4.Text = "Tanı:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // textBoxTani
            // 
            this.textBoxTani.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxTani.Location = new System.Drawing.Point(234, 269);
            this.textBoxTani.Name = "textBoxTani";
            this.textBoxTani.Size = new System.Drawing.Size(171, 31);
            this.textBoxTani.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(65, 342);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Doktor:";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // textBoxDoktor
            // 
            this.textBoxDoktor.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.textBoxDoktor.Location = new System.Drawing.Point(234, 334);
            this.textBoxDoktor.Name = "textBoxDoktor";
            this.textBoxDoktor.Size = new System.Drawing.Size(171, 31);
            this.textBoxDoktor.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(65, 402);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 23);
            this.label6.TabIndex = 10;
            this.label6.Text = "Durum:";
            // 
            // comboBoxDurum
            // 
            this.comboBoxDurum.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBoxDurum.Font = new System.Drawing.Font("Segoe UI Semibold", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBoxDurum.FormattingEnabled = true;
            this.comboBoxDurum.Items.AddRange(new object[] {
            "Tedavi Görüyor",
            "Randevu Bekliyor",
            "Taburcu Edildi"});
            this.comboBoxDurum.Location = new System.Drawing.Point(234, 392);
            this.comboBoxDurum.Name = "comboBoxDurum";
            this.comboBoxDurum.Size = new System.Drawing.Size(171, 33);
            this.comboBoxDurum.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.Location = new System.Drawing.Point(65, 464);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(102, 23);
            this.label7.TabIndex = 12;
            this.label7.Text = "Son Ziyaret:";
            // 
            // dateTimePickerSonZiyaret
            // 
            this.dateTimePickerSonZiyaret.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePickerSonZiyaret.Location = new System.Drawing.Point(234, 457);
            this.dateTimePickerSonZiyaret.Name = "dateTimePickerSonZiyaret";
            this.dateTimePickerSonZiyaret.Size = new System.Drawing.Size(254, 30);
            this.dateTimePickerSonZiyaret.TabIndex = 13;
            // 
            // buttonKaydet
            // 
            this.buttonKaydet.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.buttonKaydet.Location = new System.Drawing.Point(511, 557);
            this.buttonKaydet.Name = "buttonKaydet";
            this.buttonKaydet.Size = new System.Drawing.Size(153, 59);
            this.buttonKaydet.TabIndex = 14;
            this.buttonKaydet.Text = "Kaydet";
            this.buttonKaydet.UseVisualStyleBackColor = true;
            this.buttonKaydet.Click += new System.EventHandler(this.buttonkaydet_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MediSistem.Properties.Resources.sekreter_jpg1;
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(511, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(649, 413);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // AddPatientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1172, 655);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.buttonKaydet);
            this.Controls.Add(this.dateTimePickerSonZiyaret);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.comboBoxDurum);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBoxDoktor);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBoxTani);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxTelefon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxAdSoyad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxTc);
            this.Controls.Add(this.label1);
            this.Name = "AddPatientForm";
            this.Text = "AddPatientForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxAdSoyad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxTelefon;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxTani;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDoktor;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox comboBoxDurum;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dateTimePickerSonZiyaret;
        private System.Windows.Forms.Button buttonKaydet;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}