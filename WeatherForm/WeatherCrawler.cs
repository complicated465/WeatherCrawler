using HtmlAgilityPack;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherForm
{
    class WeatherCrawler
    {
        private readonly Form1 _form;

        public WeatherCrawler(Form1 form)
        {
            this._form = form;
        }

        public void craw()
        {
            int count = 0;
            int offset = 0;
            int limit = 100;
            int first = 0;
            WebClient client = new WebClient();
           
            StreamWriter sw1 = new StreamWriter(System.Windows.Forms.Application.StartupPath + @"\WeatherData.txt", false, Encoding.UTF8);

            MemoryStream ms = new MemoryStream(client.DownloadData("http://www.cwb.gov.tw/V7/forecast/town368/"));

            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);



            
            sw1.Close();
            
            


            Console.WriteLine("sdfsadf");

            // _form.update("ffff");
            //_form.update(allData);
        }

    }
}