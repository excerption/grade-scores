using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GradeScores;
using Moq;
using GradeScores.Interfaces;
using GradeScores.Wrappers;
using System.IO;

namespace GradeScoresUnitTests
{
    [TestClass]
    public class PathManagerTests
    {
        [TestMethod]
        public void CreateOutputPathFromInputPathWithPostfix_RelativePathInput_RelativePathOutput()
        {
            IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);

            var manager = new PathManager(mockFileWrapper);
            string outputPath = manager.CreateOutputPathFromInputPathWithPostfix("students.txt", "graded");

            Assert.AreEqual(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students-graded.txt"), outputPath);
        }

        [TestMethod]
        public void CreateOutputPathFromInputPathWithPostfix_AbsolutePathInput_AbslolutePathOutput()
        {
            IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);

            var manager = new PathManager(mockFileWrapper);
            string outputPath = manager.CreateOutputPathFromInputPathWithPostfix(@"C:\Students data\students.txt", "graded");

            Assert.AreEqual(@"C:\Students data\students-graded.txt", outputPath);
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "File located at WRONG_PATH doesn't exists or you haven't enough permission to access it")]
        public void CreateOutputPathFromInputPathWithPostfix_UnexistingPath_ThrowFileNotFoundException()
        {
            var manager = new PathManager(new FileWrapper());
            manager.CreateOutputPathFromInputPathWithPostfix("WRONG_PATH", "graded");
        }

        [TestMethod]
        [ExpectedException(typeof(FileNotFoundException), "File located at doesn't exists or you haven't enough permission to access it")]
        public void CreateOutputPathFromInputPathWithPostfix_PathIsNull_ThrowFileNotFoundException()
        {
            var manager = new PathManager(new FileWrapper());
            manager.CreateOutputPathFromInputPathWithPostfix(null, "graded");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "postfix")]
        public void CreateOutputPathFromInputPathWithPostfix_PostfixIsEmpty_ThrowArgumentNullException()
        {
            IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);

            var manager = new PathManager(mockFileWrapper);
            string outputPath = manager.CreateOutputPathFromInputPathWithPostfix("students.txt", string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "postfix")]
        public void CreateOutputPathFromInputPathWithPostfix_PostfixIsNull_ThrowArgumentNullException()
        {
            IFile mockFileWrapper = Mock.Of<IFile>(p => p.Exists(It.IsAny<string>()) == true);

            var manager = new PathManager(mockFileWrapper);
            string outputPath = manager.CreateOutputPathFromInputPathWithPostfix("students.txt", null);
        }
    }
}
