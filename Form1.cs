using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace StringToUnicode
{
    public partial class Form1 : Form
    {
        string currentLanguage = CultureInfo.CurrentUICulture.Name;    // 获取当前UI用户界面语言
        dynamic language = new Language().GetLanguage(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\Language.json");      

        public Form1()
        {
            InitializeComponent();
            if (language == null || language[currentLanguage] == null) { currentLanguage = "en-US"; }

            if (language[currentLanguage] != null)
            {
                label1.Text = language[currentLanguage].Libel1;
                label2.Text = language[currentLanguage].Libel2;
                checkBox1.Text = language[currentLanguage].CheckBox1;
                button1.Text = language[currentLanguage].Button1;
                button2.Text = language[currentLanguage].Button2;
                button3.Text = language[currentLanguage].Button3;
                button4.Text = language[currentLanguage].Button4;
            }
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
                if (checkBox1.Checked)
                {
                    sb.AppendFormat("\\\\u{0:x4}", (int)c);
                }
                else
                {
                    sb.AppendFormat("\\u{0:x4}", (int)c);
                }
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
            bool contains = input.Contains("\\\\");
            if (contains)
            {
                return Regex.Replace(input, @"\\\\u([0-9A-Fa-f]{4})", match =>
                {
                    return ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString();
                });
            }
            else
            {
                return Regex.Replace(input, @"\\u([0-9A-Fa-f]{4})", match =>
                {
                    return ((char)int.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString();
                });
            }
        }

    }
}
