using System;
using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores.Interfaces
{
    public interface IStudentReader
    {
        List<Student> ReadStudents(string inputPath);
    }
}
