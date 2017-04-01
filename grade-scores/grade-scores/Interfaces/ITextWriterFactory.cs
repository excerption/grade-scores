using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GradeScores.Interfaces
{
    public interface ITextWriterFactory
    {
        TextWriter CreateTextWriterFromFilePath(string path);
    }
}
