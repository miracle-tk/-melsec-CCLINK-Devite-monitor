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
using System.Windows.Shapes;

namespace WpfApplication1.WPF
{
    /// <summary>
    /// Window1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Window1 : Window
    {
        public delegate void delClick(int channelNumber);
        public event delClick EventConnectButtonClick;
        public Window1()
        {
            InitializeComponent();
        }

        private void buttonConnect_Click(object sender, RoutedEventArgs e)
        {
            int index = 81;
            switch (comboboxChannelnumber.SelectedIndex)
            {
                case 0:
                    index = 81;
                    break;
                case 1:
                    index = 82;
                    break;
                case 2:
                    index = 83;
                    break;
                case 3:
                    index = 84;
                    break;
            }
            EventConnectButtonClick(index);
            this.Close();
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
