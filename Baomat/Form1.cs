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
            string encryptedText = CaesarEncryptVietnamese(plaintext, key);
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
        private string CaesarEncryptVietnamese(string input, int key)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            key = key % 89; // Đảm bảo khóa nằm trong phạm vi của bảng chữ cái tiếng Việt

            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (char.IsLetter(ch))
                {
                    bool isUpper = char.IsUpper(ch);
                    char baseLetter = isUpper ? 'A' : 'a';

                    // Định vị vị trí của ký tự trong bảng chữ cái tiếng Việt
                    int indexInVietnameseAlphabet = Array.IndexOf(isUpper ? VietnameseAlphabetUpperCase : VietnameseAlphabetLowerCase, ch);

                    if (indexInVietnameseAlphabet >= 0)
                    {
                        int newIndex = (indexInVietnameseAlphabet + key) % 89;
                        result[i] = isUpper ? VietnameseAlphabetUpperCase[newIndex] : VietnameseAlphabetLowerCase[newIndex];
                    }
                    else
                    {
                        result[i] = ch; // Ký tự không phải là chữ cái tiếng Việt, giữ nguyên
                    }
                }
                else
                {
                    result[i] = ch; // Ký tự không phải là chữ cái, giữ nguyên
                }
            }

            return new string(result);
        }

        private readonly char[] VietnameseAlphabetUpperCase = "AĂÂBCDĐEÊGHIKLMNOÔƠPQRSTUƯVXY".ToCharArray();
        private readonly char[] VietnameseAlphabetLowerCase = "aăâbcdđeêghiklmnoôơpqrstuưvxy".ToCharArray();


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
            string encryptedText;
            if (string.IsNullOrEmpty(txtOutput.Text))
            {
                encryptedText = txtInput.Text;

            }
            else
            {
                encryptedText = txtOutput.Text;
            }
            if (!int.TryParse(txtKey.Text, out int key))
            {
                MessageBox.Show("Vui lòng nhập một số nguyên làm khóa.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Gọi hàm giải mã Caesar và hiển thị kết quả
            string decryptedText = CaesarDecrypt(encryptedText, key);
            txtDecrypted.Text = decryptedText;
        }


        private string CaesarDecrypt(string input, int key)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));

            key = key % 89; // Đảm bảo khóa nằm trong phạm vi của bảng chữ cái tiếng Việt

            char[] result = new char[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                char ch = input[i];
                if (char.IsLetter(ch))
                {
                    bool isUpper = char.IsUpper(ch);
                    char baseLetter = isUpper ? 'A' : 'a';

                    // Định vị vị trí của ký tự trong bảng chữ cái tiếng Việt
                    int indexInVietnameseAlphabet = Array.IndexOf(isUpper ? VietnameseAlphabetUpperCase : VietnameseAlphabetLowerCase, ch);

                    if (indexInVietnameseAlphabet >= 0)
                    {
                        int newIndex = (indexInVietnameseAlphabet - key + 89) % 89; // Trừ đi khóa và thực hiện phép chia lấy dư
                        result[i] = isUpper ? VietnameseAlphabetUpperCase[newIndex] : VietnameseAlphabetLowerCase[newIndex];
                    }
                    else
                    {
                        result[i] = ch; // Ký tự không phải là chữ cái tiếng Việt, giữ nguyên
                    }
                }
                else
                {
                    result[i] = ch; // Ký tự không phải là chữ cái, giữ nguyên
                }
            }

            return new string(result);
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }
    }
}
