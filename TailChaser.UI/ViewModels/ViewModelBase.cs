﻿using System;
using System.ComponentModel;
using TailChaser.UI.Exceptions;

namespace TailChaser.UI.ViewModels
{
    public abstract class ViewModelBase : IDisposable, INotifyPropertyChanged 
    {
        public string Name { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event Action RequestClose;

        protected virtual void OnRequestClose()
        {
            var handler = RequestClose;
            if (handler != null) handler();
        }

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

        public void Dispose()
        {
            OnDispose();
        }

        protected virtual void OnDispose()
        {

        }
    }
}