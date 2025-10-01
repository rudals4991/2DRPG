using System;
using System.Collections.Generic;
using UnityEngine;

public static class DIContainer
{
    private static readonly Dictionary<Type, object> container = new();
    public static void Register<T>(T instance) where T : class
    {
        container[typeof(T)] = instance;
    }
    public static T Resolve<T>() where T : class
    {
        container.TryGetValue(typeof(T), out var instance);
        return instance as T;
    }
}
