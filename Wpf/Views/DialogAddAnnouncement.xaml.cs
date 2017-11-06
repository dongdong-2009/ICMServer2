using FluentValidation;
using ICMServer.Models;
using ICMServer.Services;
using ICMServer.WPF.Models;
using ICMServer.WPF.ViewModels;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ICMServer.WPF.Views
{
    /// <summary>
    /// DialogAddAnnouncement.xaml 的互動邏輯
    /// </summary>
    public partial class DialogAddAnnouncement : Window
    {
        public DialogAddAnnouncement()
        {
            InitializeComponent();
            this.DataContext = new AnnouncementViewModel(
               ServiceLocator.Current.GetInstance<IValidator<AnnouncementViewModel>>(),
               ServiceLocator.Current.GetInstance<ICollectionModel<Announcement>>(),
               ServiceLocator.Current.GetInstance<IAnnouncementsRoomsModel>(),
               ServiceLocator.Current.GetInstance<IRoomsModel>(),
               ServiceLocator.Current.GetInstance<IDialogService>());
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ((AnnouncementViewModel)this.DataContext).Font = GetFont(this.textContent);
            ((AnnouncementViewModel)this.DataContext).ValidateCommand.Execute(null);
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private System.Drawing.Font GetFont(TextBox textBox)
        {
            System.Drawing.FontStyle style = new System.Drawing.FontStyle();
            if (textBox.FontWeight == FontWeights.Bold)
            {
                if (textBox.FontStyle == FontStyles.Italic)
                {
                    try
                    {
                        style = (System.Drawing.FontStyle)new FontStyleConverter().ConvertFromString("Bold, Italic");
                    }
                    catch (Exception) { }
                }
                else
                    style = System.Drawing.FontStyle.Bold;
            }
            else if (textBox.FontStyle == FontStyles.Italic)
                style = System.Drawing.FontStyle.Italic;
            return new System.Drawing.Font(textBox.FontFamily.ToString(), (float)textBox.FontSize, style);
        }

        private void SetFont(TextBox textBox, System.Drawing.Font font)
        {
            FontFamilyConverter ffc = new FontFamilyConverter();

            textBox.FontSize = font.Size;
            textBox.FontFamily = (FontFamily)ffc.ConvertFromString(font.Name);

            if (font.Bold)
                textBox.FontWeight = FontWeights.Bold;
            else
                textBox.FontWeight = FontWeights.Normal;

            if (font.Italic)
                textBox.FontStyle = FontStyles.Italic;
            else
                textBox.FontStyle = FontStyles.Normal;
        }

        private void ButtonSetFont_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FontDialog fontDialog = new System.Windows.Forms.FontDialog();
            fontDialog.Font = GetFont(textContent);
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SetFont(this.textContent, fontDialog.Font);
                ((AnnouncementViewModel)this.DataContext).Font = GetFont(this.textContent);
            }
        }
    }
}
