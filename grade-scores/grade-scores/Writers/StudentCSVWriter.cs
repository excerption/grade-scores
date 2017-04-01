using System.Collections.Generic;
using GradeScores.Interfaces;
using GradeScores.Model;
using System.IO;
using GradeScores.Wrappers;

namespace GradeScores.Writers
{
    public class StudentCSVWriter : IStudentWriter
    {
        private readonly ITextWriterFactory _writer;

        public StudentCSVWriter()
        {
            _writer = new TextWriterFactory();
        }

        public StudentCSVWriter(ITextWriterFactory writer)
        {
            _writer = writer;
        }

        public void WriteStudents(IEnumerable<Student> students, string outputPath)
        {
            // There are could be some exceptions with file creation. 
            // Path could be wrong or maybe we have not permissions to create the file.
            // We could check it with File.Create method and handle all required exceptions,
            // but in our case because we haven't any specific exceptions handler we could omit this check,
            // because TextWriter will throw built-in exceptions for all this cases.
            using (var writer = _writer.CreateTextWriterFromFilePath(outputPath))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.LastName}, {student.FirstName}, {student.Score}");
                }
            };
        }
    }
}
