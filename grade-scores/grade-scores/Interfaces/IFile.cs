using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeScores.Interfaces
{
    // Interface allows us to create a wrapper above System.IO.File.Exists call. 
    // It will be very useful in testing purposes.
    public interface IFile
    {
        bool Exists(string path);
    }
}
