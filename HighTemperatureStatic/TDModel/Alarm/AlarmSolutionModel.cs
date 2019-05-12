using System; 
using System.Text;
using System.Collections.Generic; 
using System.Data;
namespace TDModel
{
         //AlarmSolution
        public class AlarmSolutionModel
    {
                
          /// <summary>
        /// SolutionId
        /// </summary>        
        private int _solutionid;
        public int SolutionId
        {
            get{ return _solutionid; }
            set{ _solutionid = value; }
        }        
        /// <summary>
        /// SolutionName
        /// </summary>        
        private string _solutionname;
        public string SolutionName
        {
            get{ return _solutionname; }
            set{ _solutionname = value; }
        }        
                
        
   
    }
}

