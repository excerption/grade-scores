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

            // We could create a layer of abstraction for student grader too for the cases where we want to have
            // different graders, but sorting students is responsibility of the StudentGrader class and it looks better
            // to add appropriate methods to this class with different rules of sorting.
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
