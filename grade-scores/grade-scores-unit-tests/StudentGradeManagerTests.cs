using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradeScores;
using GradeScores.Interfaces;
using GradeScores.Model;
using System.Collections.Generic;
using Moq;

namespace GradeScoresUnitTests
{
    [TestClass]
    public class StudentGradeManagerTests
    {
        [TestMethod]
        public void GradeScores_NormalUsage_ReturnsSortedResultsFromWriter()
        {
            var repository = new MockRepository(MockBehavior.Default);

            var mockReader = repository.Create<IStudentReader>();
            mockReader.Setup(p => p.ReadStudents(It.IsAny<string>()))
                .Returns(() => new List<Student>
                {
                    new Student { FirstName = "Francis", LastName = "Smith", Score = 85 },
                    new Student { FirstName = "Ted", LastName = "Bundy", Score = 88 },
                    new Student { FirstName = "Allan", LastName = "Smith", Score = 85 },
                    new Student { FirstName = "Madison", LastName = "King", Score = 83 },
                });

            var mockWriter = repository.Create<IStudentWriter>();

            var manager = new StudentGradeManager(mockReader.Object, mockWriter.Object);
            manager.GradeScores("students.txt", "students-graded.txt");

            mockReader.Verify(p => p.ReadStudents("students.txt"), Times.Once(), "Incorrect input path");
            mockWriter.Verify(p => p.WriteStudents(It.IsAny<List<Student>>(), "students-graded.txt"), Times.Once(), "Incorrect output path");
            mockWriter.Verify(p => p.WriteStudents(It.IsNotNull<List<Student>>(), It.IsAny<string>()), Times.Once(), "Result students collection should not be null");
            mockWriter.Verify(p => p.WriteStudents(It.Is<List<Student>>(l => l.Count == 4), It.IsAny<string>()), Times.Once(), "Result students count must be equal to 4");
            mockWriter.Verify(p => p.WriteStudents(It.Is<List<Student>>(l => 
                l[0].FirstName == "Ted" && l[1].FirstName == "Allan" && l[2].FirstName == "Francis" && l[3].FirstName == "Madison"), 
                It.IsAny<string>()), Times.Once(), "Correct order of first names in the sorted list is: Ted, Allan, Francis, Madison");
        }
    }
}
