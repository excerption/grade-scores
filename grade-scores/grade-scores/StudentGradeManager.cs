using System;
using GradeScores.Interfaces;

namespace GradeScores
{
    public class StudentGradeManager
    {
        private readonly StudentGrader _grader;
        private readonly IStudentReader _reader;
        private readonly IStudentWriter _writer;

        public StudentGradeManager(IStudentReader reader, IStudentWriter writer)
        {
            _reader = reader;
            _writer = writer;

            _grader = new StudentGrader();
        }

        public void GradeScores(string inputPath, string outputPath)
        {
            var students = _reader.ReadStudents(inputPath);
            var gradedStudents = _grader.GradeScores(students);
            _writer.WriteStudents(gradedStudents, outputPath);
        }
    }
}
