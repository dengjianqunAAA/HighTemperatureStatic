
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace TDUserControl
{
    /// <summary>
    /// PLCList.xaml 的交互逻辑
    /// </summary>
    public partial class PLCList : Grid, IComponentConnector
    {
        public PLCList()
        {
            InitializeComponent();
            this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/TDUserControl;component/Assets/plc_circle1.png", UriKind.Absolute));
            this.gridContainer1.Visibility = Visibility.Visible;
        }

        public void BindData(int plcStates,string plcName)
        {
            this.gridContainer1.Visibility = Visibility.Visible;
            this.gridContainer2.Visibility = Visibility.Collapsed;
            this.gridContainerN.Visibility = Visibility.Collapsed;
            this.plcItems.Children.Clear();
            Button element = new Button
            {
                Content = plcName,
                Style = (Style)FindResource("PLCItemStyle")
            };
            this.plcItems.Children.Add(element);
            if (plcStates == 1)
            {

                this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/TDUserControl;component/Assets/plc_circle1.png", UriKind.Absolute));
            }
            else
            {

                this.imgPLCState.Source = new BitmapImage(new Uri("pack://application:,,,/TDUserControl;component/Assets/plc_circle2.png", UriKind.RelativeOrAbsolute));
            }
            this.Visibility = Visibility.Visible;
        }
    }
}
