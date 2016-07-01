using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WeatherForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            WeatherCrawler myWeatherCrawler = new WeatherCrawler(this);
            myWeatherCrawler.craw();
            webBrowser1.Visible = false;
            comboBox1.TextChanged += ComboBox1_TextChanged;
            //webBrowser1.DocumentCompleted += WebBrowser1_DocumentCompleted;
        }

        private void ComboBox1_TextChanged(object sender, EventArgs e)
        {
            Data abc = comboBox1.SelectedItem as Data;
            comboBox2.DataSource = abc.inner;
            comboBox2.DisplayMember = "sName";
            comboBox2.ValueMember = "sid";
        }

        private void WebBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
           // string abc = webBrowser1.Document.Body.InnerHtml;
            //int a = 0;
           // string htmlA = webBrowser1.DocumentText;
            //string htmlB = webBrowser1.DocumentText;
        }

        private void label1_Click(object sender, EventArgs e)
        {
         
        }

        internal void update(List<Data> dataList)
        {
            comboBox1.DataSource = dataList;
            comboBox1.DisplayMember = "sName";
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WeatherCrawler myWeatherCrawler = new WeatherCrawler(this);
            myWeatherCrawler.craw(comboBox2.SelectedValue.ToString());

        }

        public void updateWeather(string[] weatehr)
        {
            label3.Text = "時間:"+weatehr[0];
            label4.Text = "溫度:" + weatehr[1];
            label5.Text = "濕度:" + weatehr[2];
            label6.Text = "降雨量:" + weatehr[3];
            label7.Text = "日出:" + weatehr[4];
            label8.Text = "日末:" + weatehr[5];

        }

     
    }
}
