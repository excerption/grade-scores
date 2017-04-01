using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using GradeScores;
using GradeScores.Model;
using GradeScores.Readers;
using GradeScores.Interfaces;
using Moq;

namespace GradeScoresUnitTests
{
    [TestClass]
    public class StudentCSVReaderTests
    {
        [TestMethod]
        public void ReadStudents_SampleFile_ReturnsStudentsCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TED, BUNDY, 88");
            sb.AppendLine("ALLAN, SMITH, 85");
            sb.AppendLine("MADISON, KING, 83");
            sb.AppendLine("FRANCIS, SMITH, 85");

            using (StringReader reader = new StringReader(sb.ToString()))
            {
                IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);
                ITextReaderFactory mockReaderFactory = Mock.Of<ITextReaderFactory>(p =>
                    p.CreateTextReaderFromFilePath(It.IsAny<string>()) == reader);

                StudentCSVReader studentReader = new StudentCSVReader(mockReaderFactory, mockFileWrapper);
                var students = studentReader.ReadStudents(It.IsAny<string>());

                Assert.IsNotNull(students);
                Assert.AreEqual(4, students.Count);

                CollectionAssert.AreEqual(new List<Student>
                {
                    new Student { FirstName = "TED", LastName = "BUNDY", Score = 88 },
                    new Student { FirstName = "ALLAN", LastName = "SMITH", Score = 85 },
                    new Student { FirstName = "MADISON", LastName = "KING", Score = 83 },
                    new Student { FirstName = "FRANCIS", LastName = "SMITH", Score = 85 },
                }, 
                students);
            }
        }

        [TestMethod]
        public void ReadStudents_FileWithDifferentSeparationAndEndWrongLines_ReturnsStudentsCollection()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("TED, BUNDY, 88");
            sb.AppendLine("ALLAN, SMITH, 85");
            sb.AppendLine();
            sb.AppendLine(" ");
            sb.AppendLine("MADISON,KING,83");
            sb.AppendLine("FRANCIS,SMITH,85");
            sb.AppendLine("Test1, Test2");
            sb.AppendLine("Test3, 86");
            sb.AppendLine("Test4, Test5, Score");
            sb.AppendLine();

            using (StringReader reader = new StringReader(sb.ToString()))
            {
                IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);
                ITextReaderFactory mockReaderFactory = Mock.Of<ITextReaderFactory>(p =>
                    p.CreateTextReaderFromFilePath(It.IsAny<string>()) == reader);

                StudentCSVReader studentReader = new StudentCSVReader(mockReaderFactory, mockFileWrapper);
                var students = studentReader.ReadStudents(It.IsAny<string>());

                Assert.IsNotNull(students);
                Assert.AreEqual(4, students.Count);

                CollectionAssert.AreEqual(new List<Student>
                {
                    new Student { FirstName = "TED", LastName = "BUNDY", Score = 88 },
                    new Student { FirstName = "ALLAN", LastName = "SMITH", Score = 85 },
                    new Student { FirstName = "MADISON", LastName = "KING", Score = 83 },
                    new Student { FirstName = "FRANCIS", LastName = "SMITH", Score = 85 },
                },
                students);

            }
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "File located at WRONG_PATH doesn't exists or you haven't enough permission to access it")]
        public void ReadStudents_FileIsNotExists_ThrowFileNotFoundException()
        {
            StudentCSVReader studentReader = new StudentCSVReader();
            studentReader.ReadStudents("WRONG_PATH");
        }
    }
}
