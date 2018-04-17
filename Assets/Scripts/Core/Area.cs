﻿using System;
using UnityEngine;

public class Area : MonoBehaviour
{
    public enum AreaModeEnum { Box, Circle }

    [SerializeField] Vector2Int size;
    [SerializeField] int radius;

    public Vector2Int Size { get { return size; } }
    public int Radius { get { return radius; } }

    public Area SetSize(int width, int height)
    {
        size.x = width;
        size.y = height;
        radius = 0;
        return this;
    }

    public Area SetRadius(int radius)
    {
        this.radius = radius;
        size = Vector2Int.zero;
        return this;
    }

    public virtual Collider2D[] GetEntity(AreaModeEnum areaModeEnum)
    {
        return GetEntity(areaModeEnum, null);
    }

    public virtual Collider2D[] GetEntity(AreaModeEnum areaModeEnum, string layerName)
    {
        LayerMask layerMask = LayerMask.NameToLayer(layerName);
        switch (areaModeEnum)
        {
            case AreaModeEnum.Box:
                if (size == Vector2.zero)
                {
                    Debug.LogError("Size가 설정되지 않았습니다");
                    return null;
                }
                return GetEntityByBox(layerMask);
            case AreaModeEnum.Circle:
                if (radius <= 0)
                {
                    Debug.LogError("Radius가 설정되지 않았습니다");
                    return null;
                }
                return GetEntityByCircle(layerMask);
            default:
                throw new ArgumentOutOfRangeException("areaModeEnum", areaModeEnum, null);
        }
    }

    Collider2D[] GetEntityByBox(LayerMask layerMask)
    {
        return Physics2D.OverlapBoxAll(transform.position, size, 0, 1 << layerMask.value);
    }

    Collider2D[] GetEntityByCircle(LayerMask layerMask)
    {
        return Physics2D.OverlapCircleAll(transform.position, radius, 1 << layerMask.value);
    }

    public static Area Create(Vector2 position, int width, int height)
    {
        return Create(position).SetSize(width, height);
    }

    public static Area Create(Vector2 position, int radius)
    {
        return Create(position).SetRadius(radius);   
    }

    public static Area Create(Vector2 position)
    {
        GameObject areaGameObject = new GameObject("Area");
        return areaGameObject.AddComponent<Area>();
    }
}