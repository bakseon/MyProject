using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO.Ports;
//version2 
//version3
namespace SerialCommunication
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private SerialPort serial;
        public MainWindow()
        {
            InitializeComponent();
            this.serial = new SerialPort(portName: $"COM9", baudRate: 9600, parity: Parity.None, dataBits: 8, stopBits: StopBits.One);  //version_3 <== 친구야 이거 수정됐다.
            this.serial.Encoding = Encoding.UTF8;
        }

        private void Click_on_Open(object sender, RoutedEventArgs e)
        {
            this.serial.Open();
            if (this.serial.IsOpen)
            {
                MessageBox.Show(messageBoxText: $"시리얼 통신이 연결 되었습니다.");
                this.serial.DataReceived += Serial_DataReceived;
            }
            else
            {
                MessageBox.Show(messageBoxText: $"시리얼 통신이 안 되었습니다.");
                String.ValueOf("Hahahahahahahahaha")        //version_4 <== 
            }

        }
        private void Serial_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //throw new Not ImplementedException();
            var string_builder = new StringBuilder();

            string_builder.Append(value: $"{serial.ReadExisting()}");
            this.Dispatcher.Invoke(callback: () =>
            {
                if (String.IsNullOrEmpty(string_builder.ToString()))
                {
                    return;
                }
                else
                {
                    this.TextBox_Window.Text = string_builder.ToString();
                }
            
            
            });
        }

        private void Click_on_Close(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(messageBoxText: $"연결을 종료합니다.");
            this.serial.Close();
        }

        private void Click_on_Sending(object sender, RoutedEventArgs e)
        {
            var textMessage = this.TextBox_Send.Text.Trim();
            if (String.IsNullOrEmpty(value: textMessage))
            {
                return;
            }
            else
            {
                this.serial.WriteLine(text: textMessage);
                this.Dispatcher.Invoke(callback: () =>
                {
                    this.TextBox_Window.Text = $"내가 보낸 메세지{textMessage}";
                });
            }

        }
    }
}
