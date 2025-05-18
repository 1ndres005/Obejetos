using UnityEngine;

public abstract class Stat
{
    protected int minValue;
    protected int maxValue;
    protected int currentValue;

    public Stat(int min, int max)
    {
        minValue = min;
        maxValue = max;
        currentValue = max;
    }

    public int CurrentValue => currentValue;
    public int MaxValue => maxValue;

    public int GetValue() => currentValue;

    public virtual void AffectValue(int amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
    }

    public virtual void SetCurrentValue(int value)
    {
        currentValue = Mathf.Clamp(value, minValue, maxValue);
    }

    // M�todo opcional para sobrescribir comportamiento como regeneraci�n
    public virtual void UpdateStat() { }
}



