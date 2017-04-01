using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GradeScores.Interfaces;
using System.IO;

namespace GradeScores.Wrappers
{
    public class TextReaderFactory : ITextReaderFactory
    {
        public TextReader CreateTextReaderFromFilePath(string path)
        {
            return new StreamReader(path);
        }
    }
}
