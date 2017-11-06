using ICMServer.Models;
using System;
using System.Collections.Generic;
using System.Windows;

namespace ICMServer.Services
{
    public interface IDialogService
    {
        /// <summary>
        /// Diaplay OpenFileDialog
        /// </summary>
        /// <param name="Filter">ex. "XML files (*.xml)|*.xml|All files (*.*)|*.*";</param>
        /// <param name="DefaultFileName">ex. "addressbook.xml"</param>
        void ShowOpenFileDialog(string Filter, string DefaultFileName, Action<string> afterSelectedCallback = null);
        void ShowSaveFileDialog(string Filter, string DefaultFileName, Action<string> afterSelectedCallback = null);

        void ShowAlarmDialog(/*List<eventwarn> alarms*/);
        void ShowBackupDataDialog();
        void ShowConfigSystemSettingDialog();
        void ShowDisplayQRCodeDialog(string content);
        void ShowExportAddressBookDialog();
        void ShowImportAddressBookDialog();
        void ShowExportCardListDialog();
        void ShowImportCardListDialog(Action afterImportedCallback = null);
        void ShowInputIPAddressDialog(string DefaultIPAddress, Action<string> afterSelectedCallback = null);

        void ShowSelectDevicesToBeUpgraded(upgrade upgradeFile);
        //void ShowSelectDevicesDialog();
        void ShowSelectOnlineDeviceDialog(DeviceType deviceType, Action<string> afterSelectedCallback = null);
        void ShowSelectOnlineDeviceWithCameraDialog(Action<Device> afterSelectedCallback = null);
        void ShowSelectRoomAddressDialog(string DefaultRoomAddress, Action<string> afterSelectedCallback = null);
        void ShowSelectDeviceAddressDialog(string DefaultDeviceAddress, Action<string> afterSelectedCallback = null);

        void ShowAddAdvertisementDialog();

        void ShowEditAlarmDialog(eventwarn AlarmToBeEdited);
        
        void ShowAddAnnouncementDialog();
        void ShowViewAnnouncementDialog(Announcement AnnouncementToBeDisplayed);

        void ShowEditCommonEventDialog(eventcommon EventToBeEdited);

        void ShowAddDeviceDialog();
        void ShowEditDeviceDialog(Device DeviceToBeEdited);
        void ShowViewDeviceDialog(Device DeviceToBeDisplayed);

        void ShowAddResidentDialog();
        void ShowEditResidentDialog(holderinfo ResidentToBeEdited);

        void ShowAddSecurityCardDialog();
        void ShowEditSecurityCardDialog(iccard SecurityCardToBeEdited);

        void ShowAddSipAccountDialog(string roomAddress);
        void ShowSyncSipAccountsDialog();

        void ShowAddUpgradeFileDialog();

        MessageBoxResult ShowMessageBox(string messageBoxText);
        MessageBoxResult ShowMessageBox(
            string messageBoxText,
            string caption,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = 0,
            MessageBoxOptions options = 0);
        MessageBoxResult ShowMessageBox(
           Window owner,
           string messageBoxText);
       MessageBoxResult ShowMessageBox(
            Window owner,
            string messageBoxText,
            string caption,
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None, 
            MessageBoxResult defaultResult = 0,
            MessageBoxOptions options = 0);

        void ShowPlayVideoMessageDialog(leaveword VideoMessage);
        void ShowSipAccountManagementDialog();
        void ShowUserManagementDialog();
    }
}
