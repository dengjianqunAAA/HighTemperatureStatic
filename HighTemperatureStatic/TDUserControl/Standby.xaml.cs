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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TDUserControl
{
    /// <summary>
    /// Standby.xaml 的交互逻辑
    /// </summary>
    public partial class Standby :  Grid, IComponentConnector
    {
        public Standby()
        {
            InitializeComponent();
        }
        public void BindData(int standby, string standbyName)
        {
            this.gridContainer1.Visibility = Visibility.Visible;
            this.gridContainer2.Visibility = Visibility.Collapsed;
            this.gridContainerN.Visibility = Visibility.Collapsed;
            this.plcItems.Children.Clear();
            Button element = new Button
            {
                Content = standbyName,
                Style = (Style)FindResource("PLCItemStyle")
            };
            this.plcItems.Children.Add(element);
            if (standby == 1)
            {
                this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/TDUserControl;component/Assets/coldStandby.png", UriKind.Absolute));
            }
            else if (standby == 2)
            {
                this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/TDUserControl;component/Assets/hotStandby.png", UriKind.Absolute));
            }
            this.Visibility = Visibility.Visible;
        }
    }
}
