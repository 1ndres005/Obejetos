using UnityEngine;

public class Stat
{
    private int minValue;
    private int maxValue;
    private int currentValue;

    // Constructor que define los valores mínimo, máximo y establece el valor actual en el máximo
    public Stat(int min, int max)
    {
        minValue = min;
        maxValue = max;
        currentValue = max; // El valor inicial es el máximo
    }

    // Propiedad solo de lectura para obtener el valor actual
    public int CurrentValue => currentValue;

    // Propiedad solo de lectura para obtener el valor máximo
    public int MaxValue => maxValue;

    // Método para obtener el valor actual (similar a CurrentValue pero más explícito)
    public int GetValue() => currentValue;

    // Método para cambiar el valor actual sumando o restando una cantidad
    public void AffectValue(int amount)
    {
        currentValue = Mathf.Clamp(currentValue + amount, minValue, maxValue);
    }

    // Método para establecer el valor actual directamente, asegurando que no exceda los límites
    public void SetCurrentValue(int value)
    {
        currentValue = Mathf.Clamp(value, minValue, maxValue);
    }
}



