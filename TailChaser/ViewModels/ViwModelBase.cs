using System;
using System.ComponentModel;
using TailChaser.Exceptions;

namespace TailChaser.ViewModels
{
    public abstract class ViwModelBase : INotifyPropertyChanged, IDisposable
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected bool ThrowOnInvalidPropertyName;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }

        public void VerifyPropertyName(string propertyName)
        {
            if (TypeDescriptor.GetProperties(this)[propertyName] == null && ThrowOnInvalidPropertyName) 
            {
                throw new InvalidPropertyNameException(propertyName);
            }
        }

        protected virtual ViwModelBase ViewModelBase()
        {
            return this;
        }

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {
            
        }
    }
}
