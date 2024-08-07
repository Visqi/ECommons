﻿using FFXIVClientStructs.FFXIV.Component.GUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ECommons.Automation.UIInput;
/// <summary>
/// Event data.
/// </summary>
public sealed unsafe class EventData : IDisposable
{
    private nint Bytes;
    private bool disposedValue;

    /// <summary>
    /// Initializes a new instance of the <see cref="EventData"/> class.
    /// </summary>
    private EventData()
    {
        Bytes = Marshal.AllocHGlobal(sizeof(AtkEvent));
        Data = (AtkEvent*)Bytes;
        if(Data == null)
            throw new ArgumentNullException("EventData could not be created, null");
    }

    /// <summary>
    /// Gets the data pointer.
    /// </summary>
    public AtkEvent* Data { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventData"/> class.
    /// </summary>
    /// <param name="target">Target.</param>
    /// <param name="listener">Event listener.</param>
    /// <returns>Event data.</returns>
    public static EventData ForNormalTarget(void* target, void* listener)
    {
        var data = new EventData();
        data.Data->Target = (AtkEventTarget*)target;
        data.Data->Listener = (AtkEventListener*)listener;
        return data;
    }

    private void Dispose(bool disposing)
    {
        if(!disposedValue)
        {
            if(disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            Marshal.FreeHGlobal(Bytes);
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
    ~EventData()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}