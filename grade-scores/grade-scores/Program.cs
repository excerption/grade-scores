using System;
using GradeScores.Interfaces;
using GradeScores.Readers;
using GradeScores.Writers;
using GradeScores.Wrappers;

namespace GradeScores
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException(
                    "There are should be provided an argument to the grade-scores.exe with the path to the file with students data." +
                    "\nExample usage:\n" +
                    "grade-scorers.exe students.txt");
            }

            string inputPath = args[0];

            PathManager pathManager = new PathManager();
            string outputPath = pathManager.CreateOutputPathFromInputPathWithPostfix(inputPath, "graded");

            IStudentReader reader = new StudentCSVReader();
            IStudentWriter writer = new StudentCSVWriter();

            StudentGradeManager studentGradeManager = new StudentGradeManager(reader, writer);
            studentGradeManager.GradeScores(inputPath, outputPath);
        }
    }
}
