using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CardGenerator
{
    public static class DocxParser
    {
        private static Xceed.Words.NET.DocX GenerateTemplate()
        {
            Xceed.Words.NET.DocX tmpDoc = Xceed.Words.NET.DocX.Create("tmpDoc.docx");
            tmpDoc.ApplyTemplate("CardFront.docx");
            return tmpDoc;

        }

        private static void FillStudent(Xceed.Words.NET.DocX tmpDoc, Student student, int i, string year, string header)
        {
            const string mark = "{STUD";
            const string markCode = "_NUMBER}";
            const string markName = "_FNAME}";
            const string markSecondName = "_FAMILY}";
            const string markLastName = "_SNAME}";
            const string markBirthday = "_BIRTHDAY}";
            const string markHome = "_HOME}";
            const string markPhone = "_PHONE}";
            const string markHeader = "_HEADER}";
            const string markYear = "{Y}";
            
            tmpDoc.ReplaceText(mark + i.ToString() + markName, student.Name);
            tmpDoc.ReplaceText(mark + i.ToString() + markSecondName, student.Surname);
            tmpDoc.ReplaceText(mark + i.ToString() + markLastName, student.Patronomyc);
            tmpDoc.ReplaceText(mark + i.ToString() + markBirthday, student.Birthday);
            tmpDoc.ReplaceText(mark + i.ToString() + markHome, student.Adress);
            tmpDoc.ReplaceText(mark + i.ToString() + markPhone, student.PhoneNumber);
            tmpDoc.ReplaceText(mark + i.ToString() + markHeader, header);
            tmpDoc.ReplaceText(mark + i.ToString() + markCode, student.Code);
            tmpDoc.ReplaceText(markYear, year);
        }

        private static Xceed.Words.NET.DocX FillTemplate(Student student, string year, string header)
        {
            var tmpDoc = GenerateTemplate();
            FillStudent(tmpDoc,student,1,year,header);
            return tmpDoc;
        }
        private static Xceed.Words.NET.DocX FillTemplate(Student student1, Student student2, string year, string header)
        {
            var tmpDoc = GenerateTemplate();
            FillStudent(tmpDoc, student1, 1, year, header);
            FillStudent(tmpDoc, student2, 2, year, header);
            return tmpDoc;
        }

        

        public static void Fill(List<Student> studentList, string year, string header,  string path = "", string fileName = "CardResult.docx")
        {
            while (System.IO.File.Exists(path + fileName))
            {
                fileName = fileName.Substring(0, fileName.Length - 5) + "_1.docx";
            }
            var resultDoc = Xceed.Words.NET.DocX.Create(fileName);
            int i = 1;
            int j = 0;
            Student tmpStudent1;
            Student tmpStudent2;
            while (j < studentList.Count)
            {
                tmpStudent1 = studentList[j];
                if (j+1 < studentList.Count)
                {
                    tmpStudent2 = studentList[j + 1];
                    var tmpDoc = FillTemplate(tmpStudent1, tmpStudent2, year, header);
                    resultDoc.InsertDocument(tmpDoc);
                    tmpDoc.Dispose();
                    j += 2;
                }

                else
                {
                    var tmpDoc = FillTemplate(tmpStudent1, year, header);
                    resultDoc.InsertDocument(tmpDoc);
                    tmpDoc.Dispose();
                    j++;
                }
            }
            resultDoc.SaveAs(path +"\\"+ fileName);
            resultDoc.SaveAs(fileName);
            resultDoc.Dispose();
            studentList.Clear();
            
        }

        public static void Fill(List<Student> studentList, string year, string header,out int currentCount, string path = "", string fileName = "CardResult.docx")
        {
            while (System.IO.File.Exists(path + fileName))
            {
                fileName = fileName.Substring(0, fileName.Length - 5) + "_1.docx";
            }

            var resultDoc = Xceed.Words.NET.DocX.Create(fileName);
            int i = 1;
            int j = 0;
            Student tmpStudent1;
            Student tmpStudent2;
            currentCount = 0;
            while (j < studentList.Count)
            {
                tmpStudent1 = studentList[j];
                if (j + 1 < studentList.Count)
                {
                    tmpStudent2 = studentList[j + 1];
                    var tmpDoc = FillTemplate(tmpStudent1, tmpStudent2, year, header);
                    resultDoc.InsertDocument(tmpDoc);
                    tmpDoc.Dispose();
                    j += 2;
                }

                else
                {
                    var tmpDoc = FillTemplate(tmpStudent1, year, header);
                    resultDoc.InsertDocument(tmpDoc);
                    tmpDoc.Dispose();
                    j++;
                }
                currentCount = j;
            }
            resultDoc.SaveAs(path + "\\" + fileName);
            //resultDoc.SaveAs(fileName);
            resultDoc.Dispose();
            studentList.Clear();

        }

    }
}
