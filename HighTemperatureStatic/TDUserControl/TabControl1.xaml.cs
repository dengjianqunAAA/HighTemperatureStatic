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
using TDModel;

namespace TDUserControl
{
    /// <summary>
    /// TabControl.xaml 的交互逻辑
    /// </summary>
    public partial class TabControl1 :  IComponentConnector
    {
        public delegate void DgTabControl(object obj);
        public event DgTabControl EBDgTabControl;
        public TabControl1(List<TabItem> tabItem)
        {
            InitializeComponent();
            this.TabItemData = tabItem;
            this.DataContext = this;
        }
        public List<TabItem> TabItemData { get; set; }      
        /// <summary>
        /// 设定TabItem字体颜色
        /// </summary>
        /// <param name="tabItem"></param>
        /// <param name="color"></param>
        public void ControlsForeground(string tabItemName, string color)
        {
            TabItem tabItem = TabItemData.FirstOrDefault(x => x.Name == tabItemName);
           
            if (tabItem != null)
            {
                TabItem ti = this.TabControl.ItemContainerGenerator.ContainerFromItem(tabItem) as TabItem;

                if (ti != null)
                {
                    LinearGradientBrush brush = new LinearGradientBrush();
                    GradientStop gs = new GradientStop();
                    gs.Offset = 0;
                    gs.Color = (Color)ColorConverter.ConvertFromString(color);
                    brush.GradientStops.Add(gs);
                    tabItem.Foreground = brush;
                }     
            }
        }
        //private int selectedIndex;
        ///// <summary>
        ///// TabControl选中的页面数
        ///// </summary>
        //public int SelectedIndex
        //{
        //    get { return this.TabControl.SelectedIndex; }
        //}

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (EBDgTabControl != null)
            {
                EBDgTabControl(sender);
            }
        }
    }
}
