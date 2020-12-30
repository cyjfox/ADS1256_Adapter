using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.IO;

namespace ADS1256_Adapter
{
    public partial class Form1 : Form
    {
        private static FileStream recordFile;

        public Form1()
        {
            InitializeComponent();
        }

        private void btn_Start_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            
            recordFile = File.Create("./Record_" + now.ToString("yyyyMMddHHmmssffff") + ".txt");
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(SerialPort.GetPortNames());
            CommCtrlEnumer.ComItem comItem = CommCtrlEnumer.GetComNum();

            label2.Text = comItem.ComName;

            serialPort1.PortName = "Com" + comItem.ComNum;
            serialPort1.BaudRate = 9600;
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            Console.WriteLine("Data Received:"); ;
            serialPort1.Open();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();

            //sp.NewLine = "\n";
            //string indata = sp.ReadLine();
            //Console.Write(indata);
            //indata = sp.ReadLine();
            //Console.Write(indata);
            byte[] data = Encoding.ASCII.GetBytes(indata); ;
            recordFile.Write(data, 0,data.Length);
        }

        //public String buildFileName()
        //{
            /*
            //new一个时间对象date
            DateTime now = DateTime.Now;
            now.ToFileTime();
            //格式化
            SimpleDateFormate sdf = new SimpleDateFormate("yyyyMMddHHmmssSSS");
            //格式化时间，并且作为文件名
            return sdf.format(date);
            */
        //}
    }
}
