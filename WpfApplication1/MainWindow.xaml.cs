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
using WpfApplication1.Class;
using WpfApplication1.WPF;
using System.Windows.Threading;
using System.Data;
using System.Windows.Controls.Primitives;
using WpfApplication1.Class.Inheritance;


namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        CClink _cclink;
        DispatcherTimer timer = new DispatcherTimer();

        public MainWindow()
        {
            InitializeComponent();
            _cclink = new CClink();
            _cclink.SomeEvent += _cclink_SomeEvent;

            //타이머
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += timer_Tick;
            timer.Start();
        }



        void timer_Tick(object sender, EventArgs e)
        {
            if (!_cclink.isOpen()) return;
            _cclink.Refrash();
            CreateGrid(Global.GlobalVariable.CCLinkVariable.CurrentTarget);
        }

        void _cclink_SomeEvent(string msg)
        {
            MessageBox.Show(msg);
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            // Begin dragging the window

            if (e.ClickCount == 1)
            {
                this.DragMove();
            }

        }
        private void ButtonOpenButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenButton.Visibility = System.Windows.Visibility.Collapsed;
            ButtonCloseButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void ButtonCloseButton_Click(object sender, RoutedEventArgs e)
        {
            ButtonOpenButton.Visibility = System.Windows.Visibility.Visible;
            ButtonCloseButton.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow w = new MainWindow();
            w.Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }



        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Window1 w = new Window1();
            w.EventConnectButtonClick += w_EventConnectButtonClick;
            w.Show();
        }


        void w_EventConnectButtonClick(int channelNumber)
        {
            _cclink.Open(channelNumber);
        }








        private void CreateGrid(AccessibleObject _object)
        {
            _cclink.Refrash();
            //if (_object == null || Global.GlobalVariable.CCLinkVariable.CurrentTarget == null) return;
            DynamicGrid.Children.Clear();
            DynamicGrid.ColumnDefinitions.Clear();
            DynamicGrid.RowDefinitions.Clear();
            int columnsCount = 8;
            int rowsCount = 16;

            int nameIndex = _object.StartAddress;
            int valueIndex = _object.StartAddress;

            string name = Global.GlobalVariable.CCLinkVariable.CurrentTarget.GetType().Name;


            //DynamicGrid.ShowGridLines = true;


            for (int i = 0; i < columnsCount; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                DynamicGrid.ColumnDefinitions.Add(cd);
            }
            for (int i = 0; i < rowsCount; i++)
            {
                RowDefinition gridRow1 = new RowDefinition();
                DynamicGrid.RowDefinitions.Add(gridRow1);
            }
            for (int i = 0; i < columnsCount; i++)
            {
                for (int j = 0; j < rowsCount; j++)
                {

                    Grid gd = new Grid();
                    gd.MouseDown += DynamicGrid_MouseLeftButtonDown;
                    gd.HorizontalAlignment = System.Windows.HorizontalAlignment.Stretch;
                    gd.VerticalAlignment = System.Windows.VerticalAlignment.Stretch;
                    Grid.SetRow(gd, j);

                    Grid.SetColumn(gd, i);

                    if (i % 2 == 0)
                    {
                        TextBlock tb = new TextBlock();
                        tb.Name = "text";
                        gd.Name = "N" + nameIndex.ToString();
                        tb.Text = name + nameIndex++.ToString();
                        tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        gd.Children.Add(tb);

                        gd.Background = new SolidColorBrush(Colors.Gray);
                    }
                    else
                    {
                        gd.Name = "V" + valueIndex.ToString();
                        TextBlock tb = new TextBlock();
                        tb.Name = "text";
                        tb.Foreground = new SolidColorBrush(Colors.Black);
                        string text = "";

                        switch (name)
                        {
                            case "X":
                                text = _cclink.bitXArray[valueIndex++].ToString();
                                break;
                            case "Y":
                                text = _cclink.bitYArray[valueIndex++].ToString();
                                break;
                            case "SB":
                                text = _cclink.bitSBArray[valueIndex++].ToString();
                                break;
                            case "SW":
                                text = _cclink.wordSWArray[valueIndex++].ToString();
                                break;
                            case "RAB":
                                text = _cclink.wordRABArray[valueIndex++].ToString();
                                break;
                            case "WW":
                                text = _cclink.wordWwArray[valueIndex++].ToString();
                                break;
                            case "WR":
                                text = _cclink.wordWrArray[valueIndex++].ToString();
                                break;
                            case "SPB":
                                text = _cclink.wordSPBArray[valueIndex++].ToString();
                                break;
                        }

                        tb.Text = text;
                        tb.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        tb.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        gd.MouseLeftButtonDown += DynamicGrid_MouseLeftButtonDown;

                        gd.Children.Add(tb);


                    }

                    DynamicGrid.Children.Add(gd);
                }

            }




            // Display grid into a Window
        }



        private void ListViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (!_cclink.isOpen()) return;

            ListViewItem LVI = (ListViewItem)((ListView)sender).SelectedItem;
            int startAddress = 0;
            int maximunCount = 0;
            switch (LVI.Name)
            {
                case "X":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._X;
                    break;
                case "Y":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._Y;
                    break;
                case "SB":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._SB;
                    break;
                case "SW":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._SW;
                    break;
                case "RAB":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._RAB;
                    break;
                case "WW":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._WW;
                    break;
                case "WR":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._WR;
                    break;
                case "SPB":
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget = Global.GlobalVariable.CCLinkVariable._SPB;
                    break;
                case "RIGHT":
                    if (Global.GlobalVariable.CCLinkVariable.CurrentTarget == null) return;
                    startAddress = Global.GlobalVariable.CCLinkVariable.CurrentTarget.StartAddress;
                    maximunCount = Global.GlobalVariable.CCLinkVariable.CurrentTarget.MaximumCount;

                    if (startAddress + 32 > maximunCount) return;
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget.StartAddress += 32;

                    break;
                case "LEFT":
                    if (Global.GlobalVariable.CCLinkVariable.CurrentTarget == null) return;
                    startAddress = Global.GlobalVariable.CCLinkVariable.CurrentTarget.StartAddress;
                    maximunCount = Global.GlobalVariable.CCLinkVariable.CurrentTarget.MaximumCount;

                    if (startAddress - 32 < 0) return;
                    Global.GlobalVariable.CCLinkVariable.CurrentTarget.StartAddress -= 32;

                    break;
            }
            CreateGrid(Global.GlobalVariable.CCLinkVariable.CurrentTarget);
        }
        static bool flag = false;
        private void DynamicGrid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (flag)
                {
                    flag = false;
                    return;
                }
                flag = true;
                
                Grid gd = (Grid)sender;
                string name = (gd.Name).Substring(0, 1);
                if (name.Equals("N")) return;
                int startAddress = Int32.Parse((gd.Name).Substring(1, (gd.Name).Length - 1));
                int value = 0;

                string strFlase = "False";

                switch (Global.GlobalVariable.CCLinkVariable.CurrentTarget.GetType().Name)
                {
                    case "X":
                        value = ((TextBlock)gd.Children[0]).Text == strFlase ? 1 : 0;
                        _cclink.SetValue(CJ_Controls.Communication.COM_Melsec.DeviceType.X, startAddress, value);
                        break;
                    case "Y":
                        value = ((TextBlock)gd.Children[0]).Text == strFlase ? 1 : 0;
                        _cclink.SetValue(CJ_Controls.Communication.COM_Melsec.DeviceType.Y, startAddress, value);
                        break;
                    case "SB":
                        value = ((TextBlock)gd.Children[0]).Text == strFlase ? 1 : 0;
                        _cclink.SetValue(CJ_Controls.Communication.COM_Melsec.DeviceType.SB, startAddress, value);
                        break;
                    case "SW":

                        test(gd);

                        break;
                    case "RAB":
                        break;
                    case "WW":
                        break;
                    case "WR":
                        break;
                    case "SPB":
                        break;

                }
            }
        }
        static bool testFlag;
        static Grid staticdGrid;
        void test(Grid grid)
        {
            if (staticdGrid == null)
            {
                staticdGrid = grid;
            }
            else
            {
                ((TextBlock)staticdGrid.Children[0]).Visibility = Visibility.Visible;
                staticdGrid.Children.Remove(staticdGrid.Children[1]);
                staticdGrid = null;
                
            }

            TextBlock tb = (TextBlock)grid.Children[0];
            tb.Visibility = Visibility.Hidden;


            TextBox tbox = new TextBox();
            tbox.Text = "0";
            tbox.Foreground = new SolidColorBrush(Colors.Black);

            grid.Children.Add(tbox);
        }



    }



}
