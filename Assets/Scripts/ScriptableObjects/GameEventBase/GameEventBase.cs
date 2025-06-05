using System;
using UnityEngine;

public abstract class GameEventBase<T> : ScriptableObject
{
    private Action<T> _listeners;

    public void Raise(T value) => _listeners?.Invoke(value);

    public void Register(Action<T> listener) => _listeners += listener;
    public void Unregister(Action<T> listener) => _listeners -= listener;
    
    private void OnDisable() =>_listeners = null;
}
