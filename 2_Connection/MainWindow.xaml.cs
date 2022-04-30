using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Exercise_2
{ // Создайте WPF приложение, разместите в окне TextBox и две кнопки. При нажатии на первую
  //  кнопку в TextBox выводится сообщение «Подключен к базе данных» при этом в обработчике
  //  установите задержку в 3-5 сек для имитации подключения к БД, также данная кнопка запускает
  //  таймер, который с периодичностью в несколько секунд выводит в TextBox сообщение «Данные
  //  получены». При нажатии на вторую кнопку по аналогии с первой отключаемся от базы(с
  //  задержкой), выводим сообщение и останавливаем таймер.

    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        int counter = 0;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Tick += Timer_Tick;
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            Thread.Sleep(100);
            textBoxInfo.Text += await ReciveDataAsync();
        }

        private async void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            textBoxInfo.Text = await EnableAsync();
            textBoxInfo.Text = await GetingDataAsync();
            timer.Start(); 
        }
        private async void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop(); 
            textBoxInfo.Text = await DisableAsync();
        }
        private async Task<string> GetingDataAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(1000);
                return "Получаем данные ";
            });
        }

        private async Task<string> EnableAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                return "Подключен к базе данных";
            });
        }
        private async Task<string> DisableAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                Thread.Sleep(3000);
                return "Отключен";
            });
        }

        private async Task<string> ReciveDataAsync()
        {
            return await Task.Factory.StartNew(() =>
            {
                counter++;
                return "*";
            });
        }
    }
}
