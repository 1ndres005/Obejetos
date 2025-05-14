using UnityEngine;

public class Stat
{
    private int minValue;
    private int maxValue;
    private int currentValue;

    // Constructor que define los valores m�nimo, m�ximo y establece el valor actual en el m�ximo
    public Stat(int min, int max)
    {
        minValue = min;
        maxValue = max;
        currentValue = max; // El valor inicial es el m�ximo
    }

    // Propiedad solo de lectura para obtener el valor actual
    public int CurrentValue => currentValue;

    // Propiedad solo de lectura para obtener el valor m�ximo
    public int MaxValue => maxValue;

    // M�todo para obtener el valor actual (similar a CurrentValue pero m�s expl�cito)
    public int GetValue() => currentValue;

    // M�todo para cambiar el valor actual sumando o restando una cantidad
    public void AffectValue(int amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
    }

    // M�todo para establecer el valor actual directamente, asegurando que no exceda los l�mites
    public void SetCurrentValue(int value)
    {
        currentValue = Mathf.Clamp(value, minValue, maxValue);
    }
}



