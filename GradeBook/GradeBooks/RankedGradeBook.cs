﻿using System;
using System.Linq;

using GradeBook.Enums;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name, bool isWeighted) : base(name, isWeighted)
        {
            this.Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            var averageGrades = new List<double>();
            foreach (Student student in Students)
            {
                averageGrades.Add(student.AverageGrade);
            }
            averageGrades.Sort();
            averageGrades.Reverse();
            int indexOfGrade = averageGrades.FindIndex(grade => grade == averageGrade);
            double AIndex = Students.Count * 0.20 -1;
            double BIndex = Students.Count * 0.40 -1 ;
            double CIndex = Students.Count * 0.60 -1 ;
            double DIndex = Students.Count * 0.80 -1;

            if (Students.Count < 5) throw InvalidOperationException();

            if (indexOfGrade <= AIndex) return 'A';
            if (indexOfGrade > AIndex && indexOfGrade <= BIndex) return 'B';
            if (indexOfGrade > BIndex && indexOfGrade <= CIndex) return 'C';
            if (indexOfGrade > CIndex && indexOfGrade <= DIndex) return 'D';

            return 'F';
        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calcuate a studen'ts overall grade.");
                    }
            if (Students.Count >= 5) base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calcuate a studen'ts overall grade.");
            }
            if (Students.Count >= 5) base.CalculateStudentStatistics(name);
        }


        private Exception InvalidOperationException()
        {
            throw new NotImplementedException();
        }
    }
}
