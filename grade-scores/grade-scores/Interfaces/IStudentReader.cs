using System;
using System.Collections.Generic;
using GradeScores.Model;

namespace GradeScores.Interfaces
{
    // We are using the interface as a layer of abstraction above the reader classes.
    // This allows us to add different readers from different sources and any data format.
    // If any reader is inherited IStudentReader interface, so we can use it without knowledge
    // how it create the students collection. We can replace one reader with another one without changing 
    // clients of this code.
    // If our solution will grow up, it would be better to move interfaces and data readers to separate projects,
    // but we will keep it in our only project for simplicity.
    public interface IStudentReader
    {
        List<Student> ReadStudents(string inputPath);
    }
}
