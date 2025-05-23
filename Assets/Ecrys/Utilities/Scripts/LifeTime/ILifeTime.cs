﻿using System;
using System.Threading;

public interface ILifeTime
{
    /// <summary>
    /// cleanup action, call when life time terminated
    /// </summary>
    ILifeTime AddCleanUpAction(Action cleanAction);

    /// <summary>
    /// add child disposable object
    /// </summary>
    ILifeTime AddDispose(IDisposable item);

    /// <summary>
    /// save object from GC
    /// </summary>
    ILifeTime AddRef(object o);

    /// <summary>
    /// is lifetime terminated
    /// </summary>
    bool IsTerminated { get; }
        
    CancellationToken Token { get; }
}