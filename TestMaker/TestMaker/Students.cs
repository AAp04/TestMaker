using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMaker
{
    public class Students
    {
        public string StudentId { get; set; }
        public string TestName { get; set; }
        public string UA1 { get; set; }
        public string UA2 { get; set; }
        public string UA3 { get; set; }
        public string UA4 { get; set; }
        public string UA5 { get; set; }

       public Students(string StudentId, string TestName, string UA1, string UA2, string UA3, string UA4, string UA5) {
            
            this.StudentId = StudentId;
            this.TestName = TestName;
            this.UA1 = UA1; 
            this.UA2 = UA2;
            this.UA3 = UA3;
            this.UA4 = UA4;
            this.UA5 = UA5;

        }

    }
}
