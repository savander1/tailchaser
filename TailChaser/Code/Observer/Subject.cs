namespace TailChaser.Code.Observer
{
    public abstract class Subject
    {
        private Observer _observer;

        public void Attach(Observer observer)
        {
            _observer = observer;
        }

        public void Detach()
        {
            _observer = null;
        }

        public void Notify()
        {
            if (_observer != null)
            {
                _observer.Update();
            }
        }
    }
}
