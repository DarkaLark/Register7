using System;
using System.Collections.Generic;

public static class GameFlags
{
    public static event Action<string> OnFlagSet;
    private static HashSet<string> _flags = new HashSet<string>();

    public static void SetFlag(string flag)
    {
        if (_flags.Add(flag))
        {
            OnFlagSet?.Invoke(flag);
        }
    }

    public static bool HasFlag(string flag) => _flags.Contains(flag);

    public static void ResetGameFlags()
    {
        _flags.Clear();
    }
}