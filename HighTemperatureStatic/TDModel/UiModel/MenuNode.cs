using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TDModel.Permissions;

namespace TDModel.UiModel
{
    public class MenuNode
    {
        private List<MenuNode> children;

        public PermissionsModel PermissionNode { get; set; }

        public Button Button { get; set; }

        public bool Selected { get; set; }

        public string Url { get; set; }

        public List<MenuNode> Children
        {
            get
            {
                return this.children ?? (this.children = new List<MenuNode>());
            }
            set
            {
                this.children = value;
            }
        }

        public FrameworkElement ChildPanel { get; set; }
    }
}
