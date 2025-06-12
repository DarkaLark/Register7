using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Events/Game Event")]
public class GameEvent : ScriptableObject
{
    private Action _listeners;

    public void Raise() => _listeners?.Invoke();

    public void Register(Action _listener) => _listeners += _listener;
    public void Unregister(Action _listener) => _listeners -= _listener;

    private void OnDisable() =>_listeners = null;
}