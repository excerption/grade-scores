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
                // Probably would be better to read data async or even better - keep two methods 
                // to support both sync and async reading, but in our case we have just a console application
                // which doing a single task so we can use sync methods here.
                while ((line = reader.ReadLine()) != null)
                {
                    string[] columns = line.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                    // We should use String.Trim method because it is possible that we have both 
                    // comma separator and comma followed with space separator in the CSV file.
                    // Additionally we could trim only the first three columns, but in general case
                    // we would trim all data.
                    for (int i = 0; i < columns.Length; i++)
                    {
                        columns[i] = columns[i].Trim();
                    }

                    if (columns.Length < 3 || columns.Any(string.IsNullOrEmpty))
                    {
                        // We couldn't make any debug output in the release configuration,
                        // so it is better to use Debug class to output some specific information
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
