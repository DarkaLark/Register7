using System;
using UnityEngine;

public abstract class GameEventBase<T> : ScriptableObject
{
    private Action<T> _listeners;

    public void Raise(T value) => _listeners?.Invoke(value);

    public void Register(Action<T> listener)
    {
        _listeners += listener;
        ViewListeners();
    }
    public void Unregister(Action<T> listener) => _listeners -= listener;

    private void OnDisable() => _listeners = null;


    private void ViewListeners()
    {
        if (_listeners != null)
        {
            foreach (var del in _listeners.GetInvocationList())
            {
                Debug.Log($"Subscribed: {del.Method.DeclaringType.FullName}.{del.Method.Name}");
            }
        }
    }
}
