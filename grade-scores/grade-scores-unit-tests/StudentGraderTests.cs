using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradeScores;
using GradeScores.Model;
using System.Collections.Generic;

namespace GradeScoresUnitTests
{
    [TestClass]
    public class StudentGraderTests
    {
        [TestMethod]
        public void GradeScores_FilledArgument_ReturnsSortedList()
        {
            var studentGrader = new StudentGrader();

            var students = new List<Student>
            {
                new Student { FirstName = "TED", LastName = "BUNDY", Score = 88 },
                new Student { FirstName = "ALLAN", LastName = "SMITH", Score = 85 },
                new Student { FirstName = "MADISON", LastName = "KING", Score = 83 },
                new Student { FirstName = "FRANCIS", LastName = "SMITH", Score = 85 },
            };

            var gradedStudents = studentGrader.GradeScores(students);

            Assert.IsNotNull(gradedStudents);
            Assert.AreEqual(4, gradedStudents.Count);

            CollectionAssert.AreEqual(new List<Student>
            {
                new Student { FirstName = "TED", LastName = "BUNDY", Score = 88 },
                new Student { FirstName = "ALLAN", LastName = "SMITH", Score = 85 },
                new Student { FirstName = "FRANCIS", LastName = "SMITH", Score = 85 },
                new Student { FirstName = "MADISON", LastName = "KING", Score = 83 },
            },
            gradedStudents);
        }

        [TestMethod]
        public void GradeScores_CaseSensitiveArgument_ReturnsSortedList()
        {
            var studentGrader = new StudentGrader();

            var students = new List<Student>
            {
                new Student { FirstName = "g", LastName = "ABCDEF", Score = 1 },
                new Student { FirstName = "f", LastName = "Abcdef", Score = 1 },
                new Student { FirstName = "e", LastName = "abcdef", Score = 1 },
                new Student { FirstName = "d", LastName = "AbCdEf", Score = 1 },
                new Student { FirstName = "c", LastName = "abcdef", Score = 1 },
                new Student { FirstName = "b", LastName = "ABCDEF", Score = 1 },
                new Student { FirstName = "a", LastName = "aBcDeF", Score = 1 },
            };

            var gradedStudents = studentGrader.GradeScores(students);

            Assert.IsNotNull(gradedStudents);
            Assert.AreEqual(7, gradedStudents.Count);

            CollectionAssert.AreEqual(new List<Student>
            {
                new Student { FirstName = "a", LastName = "aBcDeF", Score = 1 },
                new Student { FirstName = "b", LastName = "ABCDEF", Score = 1 },
                new Student { FirstName = "c", LastName = "abcdef", Score = 1 },
                new Student { FirstName = "d", LastName = "AbCdEf", Score = 1 },
                new Student { FirstName = "e", LastName = "abcdef", Score = 1 },
                new Student { FirstName = "f", LastName = "Abcdef", Score = 1 },
                new Student { FirstName = "g", LastName = "ABCDEF", Score = 1 },
            },
            gradedStudents);
        }

        [TestMethod]
        public void GradeScores_NullArgument_ReturnsEmptyList()
        {
            var studentGrader = new StudentGrader();
            var gradedStudents = studentGrader.GradeScores(null);

            Assert.IsNotNull(gradedStudents);
            Assert.AreEqual(0, gradedStudents.Count);
        }

        [TestMethod]
        public void GradeScores_EmptyListArgument_ReturnsEmptyList()
        {
            var studentGrader = new StudentGrader();
            var gradedStudents = studentGrader.GradeScores(new List<Student>());

            Assert.IsNotNull(gradedStudents);
            Assert.AreEqual(0, gradedStudents.Count);
        }
    }
}
