using System;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

namespace GetAddress
{
    public partial class Form1 : Form
    {
        private string DS_address;      // Переменная для хранения адреса датчика

        public Form1()
        {
            InitializeComponent();
        }
        /*=== ЗАГРУЗКА ФОРМЫ ===*/
        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames(); // Получаем данные о портах
            comboBox1.Items.AddRange(ports);            // Добавляем их в список
            numericUpDown1.Maximum = 8;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Value = 1;
            numericUpDown1.Increment = 1;
        }
        /*=== Запрос на получение адреса датчика ===*/
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
        /*=== Чтение адреса датчика, полученной из контроллера ===*/
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            DS_address = serialPort1.ReadExisting();    // Получаем адрес в переменную
        }
        /*=== Подключение к COM порту ===*/
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
        /*=== Таймер. Вывод адреса в текстовое поле, запись адреса в файл ===*/
        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox1.Text = DS_address;                 // записываем адрес датчика в текстовое поле
            timer1.Enabled = false;                     // отключем таймер 1
            PutAdrInTxt();                              // Помещает адрес датчика в уникальный текстовый файл
        }
        /*=== Создание файла и запись туда адреса датчика ===*/
        void PutAdrInTxt()
        {
            try
            {
                string path = $@"Датчик{numericUpDown1.Value}.txt"; // Задаем название файла
                StreamWriter sw = new StreamWriter(path, false);    // Создаем новый поток для записи
                sw.WriteLine(DS_address);                           // Записываем полученный адрес
                sw.Close();                                         // Закрываем поток
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
