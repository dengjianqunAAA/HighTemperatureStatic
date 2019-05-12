using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmSolutionImage
    public class AlarmSolutionImageModel
    {

        /// <summary>
        /// SolutionImageId
        /// </summary>        
        private int _solutionimageid;
        public int SolutionImageId
        {
            get { return _solutionimageid; }
            set { _solutionimageid = value; }
        }
        /// <summary>
        /// SolutionImagePath
        /// </summary>        
        private string _solutionimagepath;
        public string SolutionImagePath
        {
            get { return _solutionimagepath; }
            set { _solutionimagepath = value; }
        }



    }
}

