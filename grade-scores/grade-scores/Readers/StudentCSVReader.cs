using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using GradeScores.Interfaces;
using GradeScores.Model;
using GradeScores.Wrappers;

namespace GradeScores.Readers
{
    public class StudentCSVReader : IStudentReader
    {
        private readonly ITextReaderFactory _reader;
        private readonly IFile _fileWrapper;

        public StudentCSVReader()
        {
            _reader = new TextReaderFactory();
            _fileWrapper = new FileWrapper();
        }

        public StudentCSVReader(ITextReaderFactory reader, IFile fileWrapper)
        {
            _reader = reader;
            _fileWrapper = fileWrapper;
        }

        public List<Student> ReadStudents(string inputPath)
        {
            if (!_fileWrapper.Exists(inputPath))
                throw new FileNotFoundException($"File located at {inputPath} doesn't exists or you haven't enough permission to access it", inputPath);

            List<Student> students = new List<Student>();
            string line;
            int lineNumber = 1;

            using (var reader = _reader.CreateTextReaderFromFilePath(inputPath))
            {
                // It is better to support both sync and async reading, but in this case it is a console application
                // which is doing a single task so I used only sync methods.
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    for (int i = 0; i < columns.Length; i++)
                    {
                        columns[i] = columns[i].Trim();
                    }

                    if (columns.Length < 3 || columns.Any(string.IsNullOrEmpty))
                    {
                        Debug.WriteLine($"At line #{lineNumber} there are empty columns");
                        continue;
                    }

                    int score;

                    if (!int.TryParse(columns[2], out score))
                    {
                        Debug.WriteLine($"At line #{lineNumber}: couldn't convert score value to integer");
                        continue;
                    }

                    students.Add(new Student
                    {
                        FirstName = columns[0],
                        LastName = columns[1],
                        Score = score
                    });

                    lineNumber++;
                }
            }

            return students;
        }
    }
}
