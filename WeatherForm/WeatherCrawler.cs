using HtmlAgilityPack;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WeatherForm
{
    class WeatherCrawler
    {
        private readonly Form1 _form;
        public List<Data> dataList = new List<Data>();
        public WeatherCrawler(Form1 form)
        {
            this._form = form;
        }

        public void craw()
        {

            if ((File.Exists(System.Windows.Forms.Application.StartupPath + @"\WeatherList.txt")))
            {
                using (TextFieldParser parser = new TextFieldParser((System.Windows.Forms.Application.StartupPath + @"\WeatherList.txt")))
                {
                    parser.TextFieldType = FieldType.Delimited;
                    parser.SetDelimiters("\t\t");

                    
                   
                    while (!parser.EndOfData)
                    {
                        string[] nowFields = parser.ReadFields();

                        //Processing row
                        Data temp = new Data();
                        temp.sName = nowFields[0];
                        temp.sid = nowFields[1];
                        temp.inner = new List<Data>();
                        for (int i = 2; i < nowFields.Length; i=i+2)
                        {
                            Data temp1 = new Data();

                            temp1.sName = nowFields[i];
                            temp1.sid = nowFields[i + 1];
                            temp.inner.Add(temp1);
                        }
                        dataList.Add(temp);
                    }
                }


            }
            else
            {

                WebClient client = new WebClient();

                StreamWriter sw1 = new StreamWriter(System.Windows.Forms.Application.StartupPath + @"\WeatherList.txt", false, Encoding.UTF8);

                MemoryStream ms = new MemoryStream(client.DownloadData("http://www.cwb.gov.tw/V7/forecast/town368/Map/Weather/7Day/taiwan_0701_D.htm?layer=1"));

                HtmlDocument doc = new HtmlDocument();
                doc.Load(ms, Encoding.UTF8);
                HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("div[1]/div[1]/div");
                int i = 1;
                foreach (HtmlNode node in nodes)
                {
                    if (i % 2 == 1)
                    {
                        string asid = "";
                        Data temp = new Data();
                        string sName = node.SelectSingleNode("a").InnerHtml;
                        string sid = node.SelectSingleNode("a").Attributes["href"].Value;
                        temp.sName = sName.Split('>')[1].Trim();
                        temp.sid = sid.Split('=')[3].Trim();
                        sw1.Write(temp.sName + "\t\t" + temp.sid);
                        if (temp.sid.Substring(0, 2) == "10" || temp.sid.Substring(0, 2) == "09")
                        {
                            asid = temp.sid.Substring(0, 5);
                        }
                        else
                        {
                            asid = temp.sid.Substring(0, 2);
                        }
                        ms = new MemoryStream(client.DownloadData(string.Format("http://www.cwb.gov.tw/V7/forecast/town368/Map/Weather/7Day/{0}_0701_D.htm", asid)));
                        HtmlDocument doc1 = new HtmlDocument();
                        doc1.Load(ms, Encoding.UTF8);

                        HtmlNodeCollection nodes1 = doc1.DocumentNode.SelectNodes("div[1]/div[1]/div");
                        foreach (HtmlNode node1 in nodes1)
                        {
                            HtmlNodeCollection nodes2 = node1.SelectNodes("./div");
                            foreach (HtmlNode node2 in nodes2)
                            {
                                temp = new Data();
                                temp.sName = sName.Split('>')[1].Trim();
                                temp.sid = sid.Split('=')[3].Trim();
                                string ssid = node2.Attributes["class"].Value;
                                if (!ssid.Contains("Ta")) continue;
                                string ssName = node2.SelectSingleNode("./p/a").InnerHtml;



                        /*        temp.ssName = ssName.Split('>')[1].Trim();
                                if (ssid.Split(new string[] { "Ta" }, StringSplitOptions.RemoveEmptyEntries)[1].Length > 8) continue;
                                temp.ssid = ssid.Split(new string[] { "Ta" }, StringSplitOptions.RemoveEmptyEntries)[1].Substring(0, 7);
                                sw1.Write("\t\t" + temp.ssName + "\t\t" + temp.ssid);
                                dataList.Add(temp);*/

                            }
                        }
                        sw1.Write("\n");

                        if (i == 44) break;

                    }
                    if (i == 44) break;
                    i++;
                    Console.WriteLine(node.InnerText.Trim());
                }
                sw1.Close();
            }
            





            Console.WriteLine("sdfsadf");

            // _form.update("ffff");
            _form.update(dataList);
        }

        public void craw(string url)
        {
           
            WebClient client = new WebClient();

           // StreamWriter sw1 = new StreamWriter(System.Windows.Forms.Application.StartupPath + @"\WeatherData.txt", false, Encoding.UTF8);

            MemoryStream ms = new MemoryStream(client.DownloadData(string.Format("http://www.cwb.gov.tw/V7/forecast/town368/GT/{0}.htm",url)));

            HtmlDocument doc = new HtmlDocument();
            doc.Load(ms, Encoding.UTF8);
            HtmlNode nodes = doc.DocumentNode.SelectSingleNode("div[1]/table[1]/tr[2]");
            string[] aa = nodes.InnerText.Trim().Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            _form.updateWeather(aa);
        }

    }
}