using GradeScores.Interfaces;
using GradeScores.Wrappers;
using System;
using System.IO;

namespace GradeScores
{
    public class PathManager
    {
        private readonly IFile _fileWrapper;

        public PathManager()
        {
            _fileWrapper = new FileWrapper();
        }

        public PathManager(IFile wrapper)
        {
            _fileWrapper = wrapper;
        }

        public string CreateOutputPathFromInputPathWithPostfix(string inputPath, string postfix)
        {
            if (!_fileWrapper.Exists(inputPath))
                throw new FileNotFoundException($"File located at {inputPath} doesn't exists or you haven't enough permission to access it", inputPath);

            if (string.IsNullOrEmpty(postfix))
                throw new ArgumentNullException(nameof(postfix));

            FileInfo file = new FileInfo(inputPath);

            string inputFileName = Path.GetFileNameWithoutExtension(file.Name);

            // It is possible to have the invalid file name characters in the postfix string.
            // I could check it by using Path.GetInvalidFileNameChars method and check each character, but in this case
            // when I have fixed prefix and for code simplicity I omitted this check.
            string outputFileName = $"{inputFileName}-{postfix}.txt";

            string outputPath = Path.Combine(file.DirectoryName, outputFileName);

            return outputPath;
        }
    }
}
