using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    Controller2D controller;
    
    public Dictionary<StatsEnum, float> Stats;
    Dictionary<string, Transform> positionsDic;
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
    public Controller2D Controller { get { return controller; } }

    public virtual void Awake()
    {
        controller = GetComponent<Controller2D>();
        Stats = new Dictionary<StatsEnum, float>();
        positionsDic = new Dictionary<string, Transform>();
        OnGetDamaged = delegate { };
        
        // Positions
        Transform positions = transform.Find("Positions");
        if (positions == null)
        {
            positions = new GameObject("Positions").transform;
            positions.SetParent(transform);
        }

        foreach (Transform position in positions)
        {
            positionsDic.Add(position.name, position);
        }
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

    public Vector2 GetPosition(string positionName)
    {
        if (positionsDic.ContainsKey(positionName))
            return positionsDic[positionName].position;
        
        Debug.LogWarning(transform.name + " : " + positionName + "이 Positions 내에 없습니다");
        return Vector2.zero;
    }
}