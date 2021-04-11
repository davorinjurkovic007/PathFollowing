using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFollowing.Data
{
    public class DataRequest
    {
        private int fileNumberRequest;
        
        public DataRequest(int fileNumberRequest)
        {
            this.fileNumberRequest = fileNumberRequest;
        }

        public string GetFilePath(int fileNumber)
        {
            return "0";
        }

        public char[,] GetBoard()
        {
            return new char[1, 1];
        }
    }
}
