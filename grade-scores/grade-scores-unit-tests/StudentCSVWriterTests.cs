using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using GradeScores.Model;
using System.Text;
using Moq;
using GradeScores.Interfaces;
using GradeScores.Writers;

namespace GradeScoresUnitTests
{
    [TestClass]
    public class StudentCSVWriterTests
    {
        [TestMethod]
        public void WriteStudents_SampleStudentsCollection_WriteToStreamInCorrectFormat()
        {
            StringBuilder sb = new StringBuilder();

            using (StringWriter writer = new StringWriter(sb))
            {
                ITextWriterFactory mockWriterFactory = Mock.Of<ITextWriterFactory>(p =>
                    p.CreateTextWriterFromFilePath(It.IsAny<string>()) == writer);

                StudentCSVWriter studentWriter = new StudentCSVWriter(mockWriterFactory);
                studentWriter.WriteStudents(new List<Student>
                {
                    new Student { FirstName = "TED", LastName = "BUNDY", Score = 88 },
                    new Student { FirstName = "ALLAN", LastName = "SMITH", Score = 85 },                   
                    new Student { FirstName = "FRANCIS", LastName = "SMITH", Score = 85 },
                    new Student { FirstName = "MADISON", LastName = "KING", Score = 83 },
                },
                It.IsAny<string>());
            }

            string outputString = sb.ToString();

            Assert.IsFalse(string.IsNullOrEmpty(outputString));

            var output = outputString.Replace("\r\n", "\n").Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Assert.IsNotNull(output);
            Assert.AreEqual(4, output.Length);

            CollectionAssert.AreEqual(new string[]
            {
                "BUNDY, TED, 88",
                "SMITH, ALLAN, 85",
                "SMITH, FRANCIS, 85",
                "KING, MADISON, 83",
            }, output);
        }
    }
}
