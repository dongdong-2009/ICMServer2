using GalaSoft.MvvmLight.Threading;
using ICMServer.Models;
using ICMServer.WPF.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ICMServer.Services
{
    public class DialogService : IDialogService
    {
        static DialogAlarm _alarmDialog = null;

        public void ShowAlarmDialog(/*List<eventwarn> alarms*/)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(
                () => 
                {
                    if (_alarmDialog == null)
                    {
                        //_alarmDialog = new DialogAlarm(alarms);
                        _alarmDialog = new DialogAlarm();
                        _alarmDialog.ShowDialog();
                        _alarmDialog = null;
                    }
                });
        }

        public void ShowBackupDataDialog()
        {
            new DialogBackupData().ShowDialog();
        }

        public void ShowConfigSystemSettingDialog()
        {
            new DialogConfigSystemSetting().ShowDialog();
        }

        public void ShowDisplayQRCodeDialog(string content)
        {
            new DialogDisplayQRCode(content).ShowDialog();
        }

        public void ShowExportAddressBookDialog()
        {
            new DialogExportAddressBook().ShowDialog();
        }

        public void ShowImportAddressBookDialog()
        {
            new DialogImportAddressBook().ShowDialog();
        }

        public void ShowExportCardListDialog()
        {
            new DialogExportCardList().ShowDialog();
        }

        public void ShowImportCardListDialog(Action afterImportedCallback)
        {
            new DialogImportCardList(afterImportedCallback).ShowDialog();
        }

        public void ShowInputIPAddressDialog(string defaultIPAddress, Action<string> afterSelectedCallback = null)
        {
            DialogInputIPAddress dialog = new DialogInputIPAddress();
            if (dialog.ShowDialog() == true && afterSelectedCallback != null)
                afterSelectedCallback.Invoke(dialog.IPAddress);
        }

        public void ShowSelectDevicesToBeUpgraded(upgrade upgradeFile)
        {
            new DialogSelectDevicesToBeUpgraded(upgradeFile).ShowDialog();
        }

        //public void ShowSelectDevicesDialog()
        //{
        //    new DialogSelectDevices().ShowDialog();
        //}

        public void ShowSelectOnlineDeviceDialog(DeviceType deviceType, Action<string> afterSelectedCallback = null)
        {
            DialogSelectOnlineDevice dialog = new DialogSelectOnlineDevice(deviceType);
            if (dialog.ShowDialog() == true && afterSelectedCallback != null)
                afterSelectedCallback.Invoke(dialog.DeviceAddress);
        }

        public void ShowSelectOnlineDeviceWithCameraDialog(Action<Device> afterSelectedCallback = null)
        {
            DialogSelectOnlineDeviceWithCamera dialog = new DialogSelectOnlineDeviceWithCamera();
            if (dialog.ShowDialog() == true && afterSelectedCallback != null)
                afterSelectedCallback.Invoke(dialog.Device);
        }

        public void ShowSelectRoomAddressDialog(string defaultRoomAddress, Action<string> afterSelectedCallback = null)
        {
            DialogSelectRoomAddress dialog = new DialogSelectRoomAddress();
            dialog.RoomAddress = defaultRoomAddress;
            dialog.ShowDialog();
            if (afterSelectedCallback != null)
                afterSelectedCallback.Invoke(dialog.RoomAddress);
        }

        public void ShowSelectDeviceAddressDialog(string defaultDeviceAddress, Action<string> afterSelectedCallback = null)
        {
            DialogSelectDeviceAddress dialog = new DialogSelectDeviceAddress();
            dialog.DeviceAddress = defaultDeviceAddress;
            dialog.ShowDialog();
            if (afterSelectedCallback != null)
                afterSelectedCallback.Invoke(dialog.DeviceAddress);
        }

        public void ShowOpenFileDialog(string filter, string defaultFileName, Action<string> afterSelectedCallback = null)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (!string.IsNullOrWhiteSpace(filter))
                dialog.Filter = filter;
            if (!string.IsNullOrWhiteSpace(defaultFileName))
                dialog.FileName = defaultFileName;

            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                if (afterSelectedCallback != null)
                    afterSelectedCallback.Invoke(dialog.FileName);
            }
        }

        public void ShowSaveFileDialog(string filter, string defaultFileName, Action<string> afterSelectedCallback = null)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (!string.IsNullOrWhiteSpace(filter))
                dialog.Filter = filter;
            if (!string.IsNullOrWhiteSpace(defaultFileName))
                dialog.FileName = defaultFileName;

            bool? result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                if (afterSelectedCallback != null)
                    afterSelectedCallback.Invoke(dialog.FileName);
            }
        }

        public void ShowAddAdvertisementDialog()
        {
            new DialogAddAdvertisement().ShowDialog();
        }

        public void ShowEditAlarmDialog(eventwarn alarmToBeEdited)
        {
            if (alarmToBeEdited == null)
                return;

            new DialogEditAlarm(alarmToBeEdited).ShowDialog();
        }

        public void ShowAddAnnouncementDialog()
        {
            new DialogAddAnnouncement().ShowDialog();
        }

        public void ShowViewAnnouncementDialog(Announcement announcementToBeDisplayed)
        {
            if (announcementToBeDisplayed == null)
                return;

            new DialogViewAnnouncement(announcementToBeDisplayed).ShowDialog();
        }

        public void ShowEditCommonEventDialog(eventcommon eventToBeEdit)
        {
            if (eventToBeEdit == null)
                return;
        }

        public void ShowAddDeviceDialog()
        {
            new DialogAddDevice().ShowDialog();
        }

        public void ShowEditDeviceDialog(Device deviceToBeEdited)
        {
            if (deviceToBeEdited == null)
                return;

            new DialogEditDevice(deviceToBeEdited).ShowDialog();
        }

        public void ShowViewDeviceDialog(Device deviceToBeDisplayed)
        {
            if (deviceToBeDisplayed == null)
                return;

            new DialogViewDevice(deviceToBeDisplayed).ShowDialog();
        }

        public void ShowAddResidentDialog()
        {
            new DialogAddResident().ShowDialog();
        }

        public void ShowEditResidentDialog(holderinfo residentToBeEdited)
        {
            if (residentToBeEdited == null)
                return;

            new DialogEditResident(residentToBeEdited).ShowDialog();
        }

        public void ShowAddSecurityCardDialog()
        {
            new DialogAddSecurityCard().ShowDialog();
        }

        public void ShowEditSecurityCardDialog(iccard securityCardToBeEdited)
        {
            if (securityCardToBeEdited == null)
                return;

            new DialogEditSecurityCard(securityCardToBeEdited).ShowDialog();
        }

        public void ShowAddSipAccountDialog(string roomAddress)
        {
            new DialogAddSipAccount(roomAddress).ShowDialog();
        }

        public void ShowAddUpgradeFileDialog()
        {
            new DialogAddUpgradeFile().ShowDialog();
        }

        public MessageBoxResult ShowMessageBox(string messageBoxText)
        {
            return MessageBox.Show(messageBoxText);
        }

        public MessageBoxResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = 0,
            MessageBoxOptions options = 0)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon, defaultResult, options);
        }

        public MessageBoxResult ShowMessageBox(
           Window owner,
           string messageBoxText)
        {
            return MessageBox.Show(owner, messageBoxText);
        }

        public MessageBoxResult ShowMessageBox(
            Window owner,
            string messageBoxText,
            string caption,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = 0,
            MessageBoxOptions options = 0)
        {
            return MessageBox.Show(owner, messageBoxText, caption, button, icon, defaultResult, options);
        }

        public void ShowPlayVideoMessageDialog(leaveword videoMessage)
        {
            if (videoMessage == null)
                return;

            new DialogPlayVideoMessage(videoMessage).ShowDialog();
        }

        public void ShowSipAccountManagementDialog()
        {
            new DialogSipAccountManagement().ShowDialog();
        }

        public void ShowSyncSipAccountsDialog()
        {
            new DialogSyncSipAccounts().ShowDialog();
        }

        public void ShowUserManagementDialog()
        {
            new DialogUserManagement().ShowDialog();
        }
    }
}
