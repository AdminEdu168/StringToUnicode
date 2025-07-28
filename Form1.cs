using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace StringToUnicode
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Text = StringToUnicodeEscape(textBox1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBox2.Text))
            {
                textBox1.Text = UnicodeEscapeToString(textBox2.Text);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 字符串转Unicode转义序列
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string StringToUnicodeEscape(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                sb.AppendFormat("\\u{0:x4}", (int)c);
            }
            return sb.ToString();
        }

        /// <summary>
        /// Unicode转义序列转字符串
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        string UnicodeEscapeToString(string input)
        {
            return Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
            {
                return ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString();
            });
        }

    }
}
