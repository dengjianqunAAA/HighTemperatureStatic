using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
namespace TDModel
{
    //AlarmLocationImage
    public class AlarmLocationImageModel
    {

        /// <summary>
        /// AlarmLocationImageId
        /// </summary>        
        private int _alarmlocationimageid;
        public int AlarmLocationImageId
        {
            get { return _alarmlocationimageid; }
            set { _alarmlocationimageid = value; }
        }
        /// <summary>
        /// AlarmLocationImagePath
        /// </summary>        
        private string _alarmlocationimagepath;
        public string AlarmLocationImagePath
        {
            get { return _alarmlocationimagepath; }
            set { _alarmlocationimagepath = value; }
        }



    }
}

