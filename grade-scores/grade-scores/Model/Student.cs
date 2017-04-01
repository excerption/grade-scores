using System;

namespace GradeScores.Model
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // We conisder that score is just integer number and can be both positive and negative.
        // In reality if we talk about student scores it likely should be positive number between 0 and 100. 
        // In this case we have to add additional constraints on setter, but we haven't this restrictions in the task,
        // so we will use integer without any additional constraints.
        public int Score { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as Student;

            if (other == null)
                return false;

            return FirstName == other.FirstName && LastName == other.LastName && Score == other.Score;
        }

        public override int GetHashCode()
        {
            int hash = 31;
            hash = hash * 7 + FirstName.GetHashCode();
            hash = hash * 7 + LastName.GetHashCode();
            hash = hash * 7 + Score.GetHashCode();

            return hash;
        }
    }
}
