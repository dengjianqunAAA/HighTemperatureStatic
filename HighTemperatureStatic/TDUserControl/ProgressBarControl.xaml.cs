﻿using System;
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

namespace TDUserControl
{
    /// <summary>
    /// ProgressBarControl.xaml 的交互逻辑
    /// </summary>
    public partial class ProgressBarControl : UserControl
    {
        public ProgressBarControl()
        {
            InitializeComponent();

        }
        private double value;
        public double Value
        {
            get { return value; }
            set { MyProgressBar.Value = value ; }
        }
    }
}
