using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Code.Observer
{
    public class ConcreteSubject : Subject
    {
        public void Attach(string file)
        {
            var observer = new ConcreteObserver(this, file);
            Attach(observer);
        }

        public void GetFileContent()
        {
            
        }
    }
}
