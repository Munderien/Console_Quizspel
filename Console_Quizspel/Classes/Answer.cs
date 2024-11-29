using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Quizspel.Classes
{
    public class Answer
    {
        public string Text { get; }
        public bool Correct { get; }

        public Answer(string text, bool correct) {
            this.Text = text;
            this.Correct = correct;
        }
    }
}
