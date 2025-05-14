using UnityEngine;

public class Mana : Stat
{
    public FlowType FlowType { get; private set; }
    private float regenRate = 5f; // puntos por segundo

    public Mana(int min, int max, FlowType flowType) : base(min, max)
    {
        FlowType = flowType;
    }

    public void Regenerate()
    {
        if (FlowType == FlowType.Time)
        {
            AffectValue((int)(regenRate * Time.deltaTime));
        }
    }
}
