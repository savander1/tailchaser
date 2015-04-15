//  *******************************************************************************
//  * Copyright (c) 1999 - 2015.
//  * Global Relay Communications Inc.
//  * All rights reserved.
//  *******************************************************************************

using System;
using System.ComponentModel;

namespace TailChaser.UI.ViewModels.FilePane
{
    public class FilePaneViewModel : ViewModelBase
    {
        public event EventHandler<CancelEventArgs> OnClose;
        
        protected virtual void OnCloseRequested(CancelEventArgs e)
        {
            var handler = OnClose;
            if (handler != null) handler(this, e);
        }
    }
}
