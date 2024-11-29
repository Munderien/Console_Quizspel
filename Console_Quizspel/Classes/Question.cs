using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class Question
    {
        public int id { get; set; }    
        public string text { get; set; }

        public List<Answer> answers { get; set; }

        public Question() 
        {
            answers = new List<Answer>();
        }
    }
}
