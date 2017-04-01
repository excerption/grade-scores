using System;
using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores.Interfaces
{
    public interface IStudentWriter
    {
        // We are using the interface as a layer of abstraction above the writer classes.
        // It serves for the same purposes as IStudentReader interface.
        void WriteStudents(IEnumerable<Student> students, string outputPath);
    }
}
