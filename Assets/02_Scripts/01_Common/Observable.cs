using System.Collections.Generic;
using System;

public class Observable<T>
{
	private T value;
	public event Action<T> ValueChanged;

	public T Value
	{
		get { return value; }
		set { Set(value); }
	}

	public Observable(T value, Action<T> onValueChanged = null)
	{
		this.value = value;

		if (onValueChanged != null)
		{
			ValueChanged += onValueChanged;
		}
	}

	public void Set(T value)
	{
		if (EqualityComparer<T>.Default.Equals(this.value, value)) { return; }
		this.value = value;
		Invoke();
	}

	public void Invoke()
	{
		ValueChanged?.Invoke(value);
	}

	public void AddListener(Action<T> action)
	{
		ValueChanged += action;
	}

	public void RemoveListener(Action<T> action)
	{
		ValueChanged -= action;
	}
}
