using GalaSoft.MvvmLight.Messaging;
using ICMServer.Net;
using ICMServer.WPF.Messages;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// FormLogManagement.xaml 的互動邏輯
    /// </summary>
    public partial class FormLogManagement : UserControl
    {
        public FormLogManagement()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            HttpServer.Instance.ReceivedAlarmEvent += ReceivedAlarm;
            HttpServer.Instance.ReceivedCallOutEvent += ReceivedCallOutEvent;
            HttpServer.Instance.ReceivedCommonEvent += ReceivedCommonEvent;
            HttpServer.Instance.ReceivedOpenDoorEvent += ReceivedOpenDoorEvent;
        }

        private void ReceivedAlarm(object sender, HttpReceivedAlarmEventArgs e)
        {
            Messenger.Default.Send(new ReceivedAlarm(e.Alarms));
        }

        private void ReceivedCallOutEvent(object sender, EventArgs e)
        {
            Messenger.Default.Send(new ReceivedCallOutEvent());
        }

        private void ReceivedCommonEvent(object sender, EventArgs e)
        {
            Messenger.Default.Send(new ReceivedCommonEvent());
        }

        private void ReceivedOpenDoorEvent(object sender, EventArgs e)
        {
            Messenger.Default.Send(new ReceivedOpenDoorEvent());
        }
        
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            HttpServer.Instance.ReceivedAlarmEvent -= ReceivedAlarm;
            HttpServer.Instance.ReceivedCallOutEvent -= ReceivedCallOutEvent;
            HttpServer.Instance.ReceivedCommonEvent -= ReceivedCommonEvent;
            HttpServer.Instance.ReceivedOpenDoorEvent -= ReceivedOpenDoorEvent;
        }
    }
}
