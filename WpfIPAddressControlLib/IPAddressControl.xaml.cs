using ICMServer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ITE.WPF.Controls
{
    /// <summary>
    /// IP地址输入框
    /// </summary>
    public partial class IPAddressControl : UserControl
    {

        #region Fields

        /// <summary>
        /// IP地址的依赖属性
        /// </summary>
        public static readonly DependencyProperty IPProperty = DependencyProperty.Register(
                "IP",
                typeof(string),
                typeof(IPAddressControl),
                new FrameworkPropertyMetadata(DefaultIP, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, IPChangedCallback));



        public bool ReadOnly
        {
            get { return (bool)GetValue(ReadOnlyProperty); }
            set { SetValue(ReadOnlyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ReadOnly.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ReadOnlyProperty =
            DependencyProperty.Register("ReadOnly", typeof(bool), typeof(IPAddressControl), new PropertyMetadata(false));

        

        /// <summary>
        /// IP地址的正则表达式
        /// </summary>
        public static readonly Regex IpRegex = new
            Regex(@"^((2[0-4]\d|25[0-5]|(1\d{2})|([1-9]?[0-9]))\.){3}(2[0-4]\d|25[0-4]|(1\d{2})|([1-9][0-9])|([1-9]))$");
        
        /// <summary>
        /// 默认IP地址
        /// </summary>
        private const string DefaultIP = "0.0.0.0";
        
        private static readonly Regex PartIprRegex = new Regex(@"^(\.?(2[0-4]\d|25[0-5]|(1\d{2})|([1-9]?[0-9]))\.?)+$");
        
        /// <summary>
        /// 输入框的集合
        /// </summary>
        private readonly List<NumbericTextBox> numbericTextBoxs = new List<NumbericTextBox>();

        /// <summary>
        /// 当前活动的输入框
        /// </summary>
        private NumbericTextBox currentNumbericTextBox;

        
        #endregion Fields

        #region Constructors
        public IPAddressControl()
        {
            InitializeComponent();
            this.numbericTextBoxs.Add(this.IPPart1);
            this.numbericTextBoxs.Add(this.IPPart2);
            this.numbericTextBoxs.Add(this.IPPart3);
            this.numbericTextBoxs.Add(this.IPPart4);
            this.UpdateParts(this);

            foreach (var numbericTextBox in this.numbericTextBoxs)
            {
                numbericTextBox.PreviewKeyDown += numbericTextBox_PreviewKeyDown;
                numbericTextBox.PreviewTextChanged += this.NumbericTextBox_OnPreviewTextChanged;
            }

            foreach (var numbericTextBox in this.numbericTextBoxs)
            {
                numbericTextBox.TextChanged += this.TextBoxBase_OnTextChanged;
            }
        }


        #endregion Constructors
        
        #region Properties

        public string IP
        {
            get
            {
                return (string)GetValue(IPProperty);
            }

            set
            {
                SetValue(IPProperty, value);
            }
        }

        #endregion Properties

        #region Private Methods

        /// <summary>
        /// IP值改变的响应
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="dependencyPropertyChangedEventArgs"></param>
        private static void IPChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            if (dependencyPropertyChangedEventArgs.NewValue == null)
            {
                //    throw new Exception("IP can not be null");
                return;
            }

            var control = dependencyObject as IPAddressControl;
            if (control != null)
            {
                control.UpdateParts(control);
            }
        }

        private void UpdateParts(IPAddressControl control)
        {
            string[] parts = control.IP.Split(new[] { '.' });
            int count = Math.Min(parts.Length, this.numbericTextBoxs.Count);
            for (int i = 0; i < count; ++i)
                this.numbericTextBoxs[i].Text = parts[i];
        }

        private static bool IsForwardKey(KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.Right:
            case Key.Down:
                return true;
            }   

            return false;
        }

        private static bool IsReverseKey(KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.Left:
            case Key.Up:
                return true;
            }

            return false;
        }
        private static bool IsBackspaceKey(KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.Back:
                return true;
            }

            return false;
        }

        private bool IsCedeFocusKey(KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.OemPeriod:
            case Key.Decimal:
            case Key.Space:
                if (currentNumbericTextBox != null
                 && currentNumbericTextBox.Text.Length != 0
                 && currentNumbericTextBox.SelectionLength == 0
                 && currentNumbericTextBox.SelectionStart != 0)
                {
                    return true;
                }
                break;
            }

            return false;
        }

        private static bool IsNumericKey(KeyEventArgs e)
        {
            if (e.Key < Key.NumPad0 || e.Key > Key.NumPad9)
            {
                if (e.Key < Key.D0 || e.Key > Key.D9)
                {
                    return false;
                }
            }

            return true;
        }

        private static bool IsEditKey(KeyEventArgs e)
        {
            if (e.Key == Key.Back ||
                e.Key == Key.Delete)
            {
                return true;
            }
            else if (((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) &&
                      (e.Key == Key.C ||
                       e.Key == Key.V ||
                       e.Key == Key.X))
            {
                return true;
            }

            return false;
        }

        private static bool IsEnterKey(KeyEventArgs e)
        {
            if (e.Key == Key.Enter ||
                e.Key == Key.Return)
            {
                return true;
            }

            return false;
        }

        void CedeFocus(Action action)
        {
            switch (action)
            {
            case Action.Home:
                this.numbericTextBoxs[0].TakeFocus(Action.Home);
                return;

            case Action.End:
                this.numbericTextBoxs[this.numbericTextBoxs.Count - 1].TakeFocus(Action.End);
                return;

            case Action.Trim:
                int fieldIndex = this.numbericTextBoxs.IndexOf(this.currentNumbericTextBox);
                if (fieldIndex == 0)
                {
                    return;
                }

                this.numbericTextBoxs[fieldIndex - 1].TakeFocus(Action.Trim);
                return;
            }
        }

        void CedeFocus(Direction direction, Selection selection)
        {
            int fieldIndex = this.numbericTextBoxs.IndexOf(this.currentNumbericTextBox);
            int fieldCount = this.numbericTextBoxs.Count;
            if ((direction == Direction.Reverse && fieldIndex == 0) ||
                (direction == Direction.Forward && fieldIndex == (fieldCount - 1)))
            {
                return;
            }
            int next = (direction == Direction.Forward) ? (fieldIndex + 1) : (fieldIndex - 1);
            this.numbericTextBoxs[next].TakeFocus(direction, selection);
        }
        #endregion Private Methods

        #region Event Handling

        void numbericTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
            case Key.Home:
                CedeFocus(Action.Home);
                return;

            case Key.End:
                CedeFocus(Action.End);
                return;
            }

            if (IsCedeFocusKey(e))
            {
                CedeFocus(Direction.Forward, Selection.All);
                e.Handled = true;
            }
            else if (IsForwardKey(e))
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)    // Ctrl key is pressed
                {
                    CedeFocus(Direction.Forward, Selection.All);
                    e.Handled = true;
                    return;
                }
                else if (currentNumbericTextBox != null
                      && currentNumbericTextBox.SelectionLength == 0
                      && currentNumbericTextBox.SelectionStart == currentNumbericTextBox.Text.Length)
                {
                    CedeFocus(Direction.Forward, Selection.None);
                    e.Handled = true;
                    return;
                }
            }
            else if (IsReverseKey(e))
            {
                if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                {
                    CedeFocus(Direction.Reverse, Selection.All);
                    e.Handled = true;
                }
                else if (currentNumbericTextBox != null
                      && currentNumbericTextBox.SelectionLength == 0
                      && currentNumbericTextBox.SelectionStart == 0)
                {
                    CedeFocus(Direction.Reverse, Selection.None);
                    e.Handled = true;
                }
            }
            else if (IsBackspaceKey(e))
            {
                if (!ReadOnly && (currentNumbericTextBox != null
                              && (currentNumbericTextBox.Text.Length == 0 
                              || (currentNumbericTextBox.SelectionStart == 0 && currentNumbericTextBox.SelectionLength == 0))))
                {
                    CedeFocus(Action.Trim);
                    e.Handled = true;
                }
            }
            else if (IsNumericKey(e))
            {
                if (currentNumbericTextBox != null
                 && currentNumbericTextBox.Text.Length == 3
                 && currentNumbericTextBox.SelectionLength == 0
                 && currentNumbericTextBox.SelectionStart == 3)
                {
                    CedeFocus(Direction.Forward, Selection.None);
                }
            }
            else if (!IsNumericKey(e) &&
                     !IsEditKey(e) &&
                     !IsEnterKey(e))
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 获得焦点的响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_OnGotFocus(object sender, RoutedEventArgs e)
        {
            this.currentNumbericTextBox = e.OriginalSource as NumbericTextBox;
        }

        private void NumbericTextBox_OnPreviewTextChanged(object sender, TextChangedEventArgs e)
        {
            var numbericTextBox = sender as NumbericTextBox;
            Contract.Assert(numbericTextBox != null);

            if (PartIprRegex.IsMatch(numbericTextBox.Text))
            {
                var ips = numbericTextBox.Text.Split('.');

                if (ips.Length == 1)
                {
                    return;
                }

                int index = this.numbericTextBoxs.IndexOf(numbericTextBox);
                int pointer2Ips = 0;
                for (int i = index; i < this.numbericTextBoxs.Count; i++)
                {
                    while (pointer2Ips < ips.Length && string.IsNullOrEmpty(ips[pointer2Ips]))
                    {
                        pointer2Ips++;
                    }

                    if (pointer2Ips >= ips.Length)
                    {
                        return;
                    }

                    this.numbericTextBoxs[i].Text = ips[pointer2Ips];
                    pointer2Ips++;
                }
            }
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            string[] parts = (from textbox in this.numbericTextBoxs
                              select textbox.Text).ToArray();
            var ip = string.Join(".", parts);
            //DebugLog.TraceMessage(string.Format("ip {0}", ip));
            //if (IpRegex.IsMatch(ip))
            {
                this.IP = ip;
                //DebugLog.TraceMessage(string.Format("ip {0}", ip));
            }
        }

        #endregion     Event Handling
    }


    public enum Direction
    {
        Forward,
        Reverse
    }

    public enum Selection
    {
        None,
        All
    }
    public enum Action
    {
        None,
        Trim,
        Home,
        End
    }
}
