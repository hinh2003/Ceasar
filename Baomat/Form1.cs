using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baomat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox và khóa
            string plaintext = txtInput.Text;
            if (string.IsNullOrEmpty(plaintext))
            {
                MessageBox.Show("Vui lòng nhập dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!int.TryParse(txtKey.Text, out int key))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên làm khóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gọi hàm mã hóa Caesar và hiển thị kết quả
            string encryptedText = CaesarEncrypt(plaintext, key);
            txtOutput.Text = encryptedText;
        }

        // Hàm mã hóa Caesar
        private string CaesarEncrypt(string input, int key)
        {
            StringBuilder result = new StringBuilder();

            foreach (char ch in input)
            {
                if (char.IsLetter(ch))
                {
                    char encryptedChar = (char)(ch + key);
                    if (char.IsUpper(ch))
                    {
                        if (encryptedChar > 'Z')
                            encryptedChar = (char)(encryptedChar - 26);
                    }
                    else
                    {
                        if (encryptedChar > 'z')
                            encryptedChar = (char)(encryptedChar - 26);
                    }
                    result.Append(encryptedChar);
                }
                else
                {
                    result.Append(ch);
                }
            }

            return result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = openFileDialog.FileName;
                string fileContent = File.ReadAllText(filename);
                txtInput.Text = fileContent;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string encryptedText = txtOutput.Text;
            if (string.IsNullOrEmpty(encryptedText))
            {
                MessageBox.Show("Không có dữ liệu cần mã hóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            {

            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filename = saveFileDialog.FileName;
                // Ghi dữ liệu đã mã hóa vào tệp
                File.WriteAllText(filename, encryptedText);
                MessageBox.Show("Dữ liệu đã được lưu vào " + filename, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ TextBox và khóa
            string encryptedText = txtOutput.Text;
            if (!int.TryParse(txtKey.Text, out int key))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên làm khóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gọi hàm giải mã Caesar và hiển thị kết quả
            string decryptedText = CaesarDecrypt(encryptedText, key);
            txtDecrypted.Text = decryptedText;
        }

        // Hàm giải mã Caesar
        private string CaesarDecrypt(string input, int key)
        {
            StringBuilder result = new StringBuilder();

            foreach (char ch in input)
            {
                if (char.IsLetter(ch))
                {
                    char decryptedChar = (char)(ch - key);
                    if (char.IsUpper(ch))
                    {
                        if (decryptedChar < 'A')
                            decryptedChar = (char)(decryptedChar + 26);
                    }
                    else
                    {
                        if (decryptedChar < 'a')
                            decryptedChar = (char)(decryptedChar + 26);
                    }
                    result.Append(decryptedChar);
                }
                else
                {
                    result.Append(ch); // Giữ nguyên các ký tự không phải chữ cái
                }
            }

            return result.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }
    }
}
