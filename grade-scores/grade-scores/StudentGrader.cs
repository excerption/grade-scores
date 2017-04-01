using System;
using System.Collections.Generic;
using System.Linq;
using GradeScores.Model;

namespace GradeScores
{
    public class StudentGrader
    {
        public List<Student> GradeScores(IEnumerable<Student> students)
        {
            if (students == null)
                return new List<Student>();

            var result = students
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.LastName.ToLower())
                .ThenBy(x => x.FirstName.ToLower());

            return result.ToList();
        }
    }
}
