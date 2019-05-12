using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDModel.UiModel
{
    public class PageNumberChangedEventArgs : EventArgs
    {
        public int PageNumber { get; set; }
    }
}
