using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projectMtuci.Service.Helpers
{
    public class FileHelper
    {
        public void FileDeleteFromFoler(string FilePath)
        {
            if (File.Exists(Path.Combine("C:/Users/dvory/source/repos/projectMtuci/uploads/", FilePath)))
                File.Delete(Path.Combine("C:/Users/dvory/source/repos/projectMtuci/uploads/", FilePath));
            
        }
    }
}
