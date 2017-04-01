using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GradeScores.Interfaces;

namespace GradeScores.Wrappers
{
    public class TextWriterFactory : ITextWriterFactory
    {
        public TextWriter CreateTextWriterFromFilePath(string path)
        {
            return new StreamWriter(path);
        }
    }
}
