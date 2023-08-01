using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace GetAddress
{
    public partial class Form1 : Form
    {
        private string DS_address;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;                  // Включаем таймер 1
            try
            {
                serialPort1.WriteLine("GetAdr");    // Отправляем команду "получить адрес"
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            DS_address = serialPort1.ReadExisting();    // Получаем адрес в переменную
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Подключить")
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;
                    serialPort1.Open();
                    button2.Text = "Отключить";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (button2.Text == "Отключить")
            {
                serialPort1.Close();
                button2.Text = "Подключить";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames(); // Получаем данные о портах
            comboBox1.Items.AddRange(ports);            // Добавляем их в список
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = DS_address;                 // записываем адрес датчика в текстовое поле
            timer1.Enabled = false;                     // отключем таймер 1
        }
    }
}
