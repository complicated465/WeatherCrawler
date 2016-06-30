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
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        internal void update(object allData)
        {
            throw new NotImplementedException();
        }
    }
}
