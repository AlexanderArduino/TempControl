using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace TempControl
{
    public partial class Form1 : Form
    {
        string temp1, temp2, temp3, temp4, temp5, temp6, temp7, temp8 = "";
        int flag = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames();     //Считываем доступные порты в масcив
            comboBox1.Text = "";                            //Очистка комбобокса
            comboBox1.Items.Clear();                        //очистка списка combobox1

            if (ports.Length != 0)                          // Если есть доступные порты
            {
                comboBox1.Items.AddRange(ports);            // Добавляем массив портов в список combox1
                comboBox1.SelectedIndex = 0;                // Выбираем порт под индексом 0 (первый в списке)
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "Подключить")
            {
                try
                {
                    serialPort1.PortName = comboBox1.Text;  // Имя порта то, что выбрано в комбобоксе
                    serialPort1.Open();                     // Попытаемся его открыть
                    button2.Text = "Отключить";             // меняем название кнопки
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (button2.Text == "Отключить")           // Если кнопка "Отключить"
            {
                serialPort1.Close();                        // Закрывает порт
                button2.Text = "Подключить";                // меняем название кнопки
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label1.Text = "Датчик 1";
            label2.Text = "Датчик 2";
            label3.Text = "Датчик 3";
            label4.Text = "Датчик 4";
            label5.Text = "Датчик 5";
            label6.Text = "Датчик 6";
            label7.Text = "Датчик 7";
            label8.Text = "Датчик 8";

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            textBox7.ReadOnly = true;
            textBox8.ReadOnly = true;

            timer1.Enabled = false;
            timer1.Interval = 1000;

                       
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                serialPort1.Write("GetTemp");
                textBox1.Text = temp1;
                textBox2.Text = temp2;
                textBox3.Text = temp3;
                textBox4.Text = temp4;
                textBox5.Text = temp5;
                textBox6.Text = temp6;
                textBox7.Text = temp7;
                textBox8.Text = temp8;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            temp1 = serialPort1.ReadTo("End1");
            temp2 = serialPort1.ReadTo("End2");
            temp3 = serialPort1.ReadTo("End3");
            temp4 = serialPort1.ReadTo("End4");
            temp5 = serialPort1.ReadTo("End5");
            temp6 = serialPort1.ReadTo("End6");
            temp7 = serialPort1.ReadTo("End7");
            temp8 = serialPort1.ReadTo("End8");
        }
    }
}
