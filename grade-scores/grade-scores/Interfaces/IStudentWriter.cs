using System;
using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores.Interfaces
{
    public interface IStudentWriter
    {
        void WriteStudents(IEnumerable<Student> students, string outputPath);
    }
}
