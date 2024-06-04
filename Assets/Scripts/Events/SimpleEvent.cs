using System;

public class SimpleEvent
{
    private event Action listeners;

    public void AddListener(Action listener, bool notifySender = false)
    {
        listeners += listener;

        if (notifySender) listener?.Invoke();
    }

    public void RemoveListener(Action listener)
    {
        listeners -= listener;
    }

    public void Invoke()
    {
        listeners?.Invoke();
    }
}

public class SimpleEvent<T>
{
    private event Action<T> listeners;

    private T TValue;

    public void AddListener(Action<T> listener, bool notifySender = false)
    {
        listeners += listener;

        if (notifySender && TValue != null) listener?.Invoke(TValue);
    }

    public void RemoveListener(Action<T> listener)
    {
        listeners -= listener;
    }

    public void Invoke(T tValue)
    {
        TValue = tValue;

        listeners?.Invoke(tValue);
    }
}

public class SimpleEvent<T, Y>
{
    private event Action<T, Y> listeners;

    private T TValue;
    private Y YValue;

    public void AddListener(Action<T, Y> listener, bool notifySender = false)
    {
        listeners += listener;

        if (notifySender && (TValue != null && YValue != null)) listener?.Invoke(TValue, YValue);
    }

    public void RemoveListener(Action<T, Y> listener)
    {
        listeners -= listener;
    }

    public void Invoke(T tValue, Y yValue)
    {
        TValue = tValue;
        YValue = yValue;

        listeners?.Invoke(TValue, YValue);
    }
}
