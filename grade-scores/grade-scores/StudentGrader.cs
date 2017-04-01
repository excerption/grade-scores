using System;
using System.Collections.Generic;
using System.Linq;
using GradeScores.Model;

namespace GradeScores
{
    public class StudentGrader
    {
        // Returns orderd students collection from the existing collection.
        // Sort order is: Score desc, LastName asc, FirstName asc (case-insensitive)
        public List<Student> GradeScores(IEnumerable<Student> students)
        {
            if (students == null)
                return new List<Student>();

            // We have to use String.ToLower() here on comparsion because text ordering should be case-insensitive.
            // It could take an additional amount of time if we will sort one collection multiple times and in this case
            // we may pre-cache lowered first and last name in the Student class while reading the data. 
            var result = students
                .OrderByDescending(x => x.Score)
                .ThenBy(x => x.LastName.ToLower())
                .ThenBy(x => x.FirstName.ToLower());

            return result.ToList();
        }
    }
}
