using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradeScores.Interfaces
{
    public interface IFile
    {
        bool Exists(string path);
    }
}
