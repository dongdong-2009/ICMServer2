using ICMServer.Models;
using ICMServer.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAlarm.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAlarm : Window
    {
        SoundPlayer _player = new SoundPlayer();

        public DialogAlarm(/*List<eventwarn> alarms*/)
        {
            InitializeComponent();
            //this.DataContext = new DialogAlarmViewModel(alarms);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _player.SoundLocation = @"Sounds\alarm.wav";
            try
            {
                _player.LoadAsync();
                _player.PlayLooping();
            }
            catch (Exception) { }
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _player.Stop();
        }
    }
}
