using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    public Dictionary<StatsEnum, float> Stats;

    public delegate void LivingEntityFloatDelegate(float value);

    public event LivingEntityFloatDelegate OnGetDamaged;


    public virtual void Awake()
    {
        Stats = new Dictionary<StatsEnum, float>();
        OnGetDamaged = delegate { };
    }

    public virtual bool AddStat(StatsEnum statsEnum, float value)
    {
        if (Stats.ContainsKey(statsEnum))
        {
            Stats[statsEnum] = value;
            return false;
        }

        Stats.Add(statsEnum, value);
        return true;
    }

    public virtual bool GetDamaged(float damage)
    {
        Debug.Assert(Stats.ContainsKey(StatsEnum.HP));
        Stats[StatsEnum.HP] = Mathf.Clamp(Stats[StatsEnum.HP] - damage, 0, float.MaxValue);

        if (OnGetDamaged != null)
            OnGetDamaged(damage);
        return Stats[StatsEnum.HP] <= 0;
    }

}