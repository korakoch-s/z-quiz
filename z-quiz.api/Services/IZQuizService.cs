using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using z_quiz.api.Models;

namespace z_quiz.api.Services
{
    interface IZQuizService
    {
        Tester Register(string name);
        Tester Load(string name);
        ICollection<Question> Quiz();
        void Save(Tester tester);
        Tester Submit(Tester tester);
    }
}
