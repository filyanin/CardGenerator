using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGenerator
{
    public class Student
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Patronomyc { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string Code { get; set; }
        public string Birthday { get; set; }
        public string GroupName { get; set; }

        public void NormolizeStrings()
        {
            Name.Trim(',').Trim(' ').Trim(',').Trim(' ');
            Surname.Trim(',').Trim(' ').Trim(',').Trim(' ');
            Patronomyc.Trim(',').Trim(' ').Trim(',').Trim(' ');
            PhoneNumber.Trim(',').Trim(' ').Trim(',').Trim(' ');
            Adress.Trim(',').Trim(' ').Trim(',').Trim(' ');
            Code.Trim(',').Trim(' ').Trim(',').Trim(' ');
            Birthday.Trim(',').Trim(' ').Trim(',').Trim(' ');
        }

        private Student(string[] stringArray)
        {
            Code = stringArray[0];
            var fio = stringArray[1].Split(' ');
            Surname = fio[0];
            Name = fio[1];
            if (fio.Length == 2)
            {
                Patronomyc = "";
            }
            else
            {
                Patronomyc = fio[2];
            }
            Birthday = stringArray[2];
            Adress = stringArray[3];
            PhoneNumber = stringArray[4];
            GroupName = null;
            
        }

        private Student(string[] stringArray, string _groupName)
        {
            Code = stringArray[0];
            var fio = stringArray[1].Split(' ');
            Surname = fio[0];
            Name = fio[1];
            if (fio.Length == 2)
            {
                Patronomyc = "";
            }
            else
            {
                Patronomyc = fio[2];
            }
            Birthday = stringArray[2];
            Adress = stringArray[3];
            PhoneNumber = stringArray[4];
            GroupName = _groupName;

        }

        public static bool GenerateStudent(string currentDate, string fullString, string facultyCode, out Student student)
        {
            var stringArray = fullString.Split(';');

            if (stringArray[0].Substring(0, 2) != currentDate)
            {
                student = null;
                return false;
            }
            string tmpString = "";

            int i = 2;

            while (Char.IsLetter(stringArray[0][i]))
            {
                tmpString += stringArray[0][i];
                i++;
            }
            if (tmpString != facultyCode)
            {
                student = null;
                return false;
            }

            student = new Student(stringArray);
            student.NormolizeStrings();
            return true;
        }

        public static List<Student> GenerateStudentList(string currentDate, string facultyCode, string CsvPath, string EncodingName = "windows-1251")
        {
            List<Student> studentList = new List<Student>();
            Student tmpStudent;

            foreach (var line in CsvParser.getLinesFromCSV(CsvPath, EncodingName)){
                if (GenerateStudent(currentDate,line,facultyCode,out tmpStudent)){
                    studentList.Add(tmpStudent);
                    tmpStudent = null;
                }
                
            }
            return studentList;
        }

        public static int GenerateStudentList(string currentDate, string facultyCode, string CsvPath, out List<Student> _studentList, string EncodingName = "windows-1251")
        {
            List<Student> studentList = new List<Student>();
            Student tmpStudent;
            var lines = CsvParser.getLinesFromCSV(CsvPath, EncodingName);
            int count = lines.Count;

            foreach (var line in lines)
            {
                if (GenerateStudent(currentDate, line, facultyCode, out tmpStudent))
                {
                    studentList.Add(tmpStudent);
                    tmpStudent = null;
                }

            }
            _studentList = studentList;
            return count;
        }
    }
}
