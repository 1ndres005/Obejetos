using UnityEngine;

public class Energia : Stat
{
    public FlowType FlowType { get; private set; }
    private float regenRate = 3f;

    public Energia(int min, int max, FlowType flowType) : base(min, max)
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
