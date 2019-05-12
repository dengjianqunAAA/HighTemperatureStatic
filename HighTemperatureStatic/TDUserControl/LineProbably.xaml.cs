
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;


namespace TDUserControl
{
    /// <summary>
    /// LineProbably.xaml 的交互逻辑
    /// </summary>
    public partial class LineProbably : Grid, IComponentConnector
    {
        public LineProbably()
        {
            InitializeComponent();
        }

        //public void BindData(RealProductionData realProductionData)
        //{
        //    txtProductLine.Text = realProductionData.ProductLine;

        //    txtLproductModel.Text =realProductionData.LproductModel;

        //    txtRproductModel.Text = realProductionData.RproductModel;

        //    txtActiveOutput.Text = realProductionData.ActiveOutput;

        //    txtAcceptedGoods.Text = realProductionData.AcceptedGoods.ToString();

        //    txtUser.Text = realProductionData.OutStoveTime;
        //}
    }
}
