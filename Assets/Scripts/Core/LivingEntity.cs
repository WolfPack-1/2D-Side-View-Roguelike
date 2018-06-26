using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    Controller controller;
    public Dictionary<StatsEnum, float> Stats;
    public delegate void LivingEntityFloatDelegate(float value);
    public event LivingEntityFloatDelegate OnGetDamaged;
    public bool IsAlive
    {
        get
        {
            if (Stats == null || !Stats.ContainsKey(StatsEnum.HP))
                return false;
            return Stats[StatsEnum.HP] > 0;
        }
    }
    public Controller Controller { get { return controller; } }
    public int Dir { get { return controller.Dir; } }

    public virtual void Awake()
    {
        controller = GetComponent<Controller>();
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

    public virtual Collider2D[] GetEntity(Vector2 position, int radius)
    {
        return GetEntity(position, radius, null);
    }

    public virtual Collider2D[] GetEntity(Vector2 position, int width, int height)
    {
        return GetEntity(position, width, height, null);
    }

    public virtual Collider2D[] GetEntity(Vector2 position, int radius, string layerName)
    {
        Area area;
        Transform areaTransform = transform.Find("Area");
        if (areaTransform != null)
        {
            area = areaTransform.GetComponent<Area>();
        }
        else
        {
            area = Area.Create(position, radius);
            area.transform.SetParent(transform);
            area.transform.localPosition = Vector3.zero;
        }
        return area.GetEntity(Area.AreaModeEnum.Circle, layerName);
    }

    public virtual Collider2D[] GetEntity(Vector2 position, int width, int height, string layerName)
    {
        Area area;
        Transform areaTransform = transform.Find("Area");
        if (areaTransform != null)
        {
            area = areaTransform.GetComponent<Area>();
        }
        else
        {
            area = Area.Create(position, width, height);
            area.transform.SetParent(transform);
            area.transform.localPosition = Vector3.zero;
        }

        return area.GetEntity(Area.AreaModeEnum.Box, layerName);
    }
}