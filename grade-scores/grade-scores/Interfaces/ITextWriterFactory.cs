using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GradeScores.Interfaces
{
    // Interface allows us to create a factory to provide StreamWriter object created from correct source.
    // It will be very useful in testing purposes.
    public interface ITextWriterFactory
    {
        TextWriter CreateTextWriterFromFilePath(string path);
    }
}
