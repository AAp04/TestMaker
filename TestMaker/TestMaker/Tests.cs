using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker
{
    public class Tests
    {
        public string TestName { get; set; }
        public string Q1 { get; set; }
        public string Q2 { get; set; }
        public string Q3 { get; set; }
        public string Q4 { get; set; }
        public string Q5 { get; set; }
        public string A1 { get; set; }
        public string A2 { get; set; }
        public string A3 { get; set; }
        public string A4 { get; set; }
        public string A5 { get; set; }

        public Tests(string testName, string q1, string q2, string q3, string q4, string q5, string a1, string a2, string a3, string a4, string a5)
        {
            TestName = testName;
            Q1 = q1;
            Q2 = q2;
            Q3 = q3;
            Q4 = q4;
            Q5 = q5;
            A1 = a1;
            A2 = a2;
            A3 = a3;
            A4 = a4;
            A5 = a5;
        }

    }
}
