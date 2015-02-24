using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TailChaser.Code.Observer
{
    public class ConcreteObserver : Observer
    {
        private readonly ConcreteSubject _subject;
        private readonly string _name;

        public ConcreteObserver(ConcreteSubject subject, string name)
        {
            _subject = subject;
            _name = name;
        }

        public override void Update()
        {
            _subject.GetFileContent();
        }
    }
}
//flowdocuments and rtfs. http://www.dofactory.com/net/observer-design-pattern