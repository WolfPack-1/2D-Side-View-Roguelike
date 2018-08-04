using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{

    protected DataManager dataManager;
    
    Dictionary<string, Transform> positionsDic;
    public delegate void LivingEntityFloatDelegate(DamageInfo info);
    public event LivingEntityFloatDelegate OnGetDamaged;    
    
    float currentHp;
    float currentSteam;

    [SerializeField] float baseHp = 100;
    [SerializeField] float baseMoveSpeed = 5;
    [SerializeField] float baseSteam = 100;
    
    public float CurrentHp { get { return currentHp; } }
    public float MaxHp { get { return baseHp; } }
    public float CurrentSteam { get { return currentSteam; } }
    public float MaxSteam { get { return baseSteam; } }
    public float MoveSpeed { get { return baseMoveSpeed; } }
    public bool IsDead { get { return CurrentHp <= 0; } }

    public virtual void Awake()
    {
        dataManager = FindObjectOfType<DataManager>();
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
        currentHp = MaxHp;
        currentSteam = MaxSteam;
    }

    public virtual void Start()
    {
    }
    
    public virtual bool GetDamaged(DamageInfo info)
    {
        currentHp = Mathf.Clamp(currentHp - info.Damage, 0, float.MaxValue);
        
        if (OnGetDamaged != null)
            OnGetDamaged(info);
        return CurrentHp <= 0;
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
        if (string.IsNullOrEmpty(positionName))
            positionName = "Mid";
        if (positionsDic.ContainsKey(positionName))
            return positionsDic[positionName].position;
        
        this.Warning(transform.name + " : " + positionName + "이 Positions 내에 없습니다");
        return Vector2.zero;
    }
}