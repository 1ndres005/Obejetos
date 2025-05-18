using UnityEngine;

public class Mana : Stat
{
    public FlowType FlowType { get; private set; }
    private float regenRate = 5f;

    public Mana(int min, int max, FlowType flowType) : base(min, max)
    {
        FlowType = flowType;
    }

    public override void UpdateStat()
    {
        if (FlowType == FlowType.Time)
        {
            AffectValue((int)(regenRate * Time.deltaTime));
        }
    }
}
