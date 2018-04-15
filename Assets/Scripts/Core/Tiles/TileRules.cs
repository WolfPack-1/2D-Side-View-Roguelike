using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[Serializable]
[CreateAssetMenu]
public class TileRules : TileBase
{

    public Sprite DefaultSprite;
    public Tile.ColliderType DefaultColliderType;
    public List<TilingRule> TilingRules;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = DefaultSprite;
        tileData.colliderType = DefaultColliderType;

        if (TilingRules.Count > 1)
        {
            tileData.flags = TileFlags.LockTransform;
            tileData.transform = Matrix4x4.identity;
        }

        foreach (TilingRule rule in TilingRules)
        {
            Matrix4x4 transform = Matrix4x4.identity;
            if (RuleMatches(rule, position, tilemap, ref transform))
            {
                switch (rule.Output)
                {
                    case TilingRule.OutputSpriteEnum.Single:
                    case TilingRule.OutputSpriteEnum.Animation:
                        tileData.sprite = rule.Sprites[0];
                        break;
                    case TilingRule.OutputSpriteEnum.Random:
                        int index = Mathf.Clamp(Mathf.RoundToInt(Mathf.PerlinNoise((position.x + 1000000f) * rule.PerlinScale, (position.y + 1000000f) * rule.PerlinScale) * rule.Sprites.Length), 0, rule.Sprites.Length - 1);
                        tileData.sprite = rule.Sprites[index];
                        break;
                }
                tileData.transform = transform;
                tileData.colliderType = rule.ColliderType;
                break;
            }
        }
    }
    
    public override bool GetTileAnimationData(Vector3Int position, ITilemap tilemap, ref TileAnimationData tileAnimationData)
    {
        foreach (TilingRule rule in TilingRules)
        {
            Matrix4x4 transform = Matrix4x4.identity;
            if (RuleMatches(rule, position, tilemap, ref transform) && rule.Output == TilingRule.OutputSpriteEnum.Animation)
            {
                tileAnimationData.animatedSprites = rule.Sprites;
                tileAnimationData.animationSpeed = rule.AnimationSpeed;
                return true;
            }
        }
        return false;
    }
		
    public override void RefreshTile(Vector3Int location, ITilemap tileMap)
    {
        if (TilingRules != null && TilingRules.Count > 0)
        {
            for (int y = -1; y <= 1; y++)
            {
                for (int x = -1; x <= 1; x++)
                {
                    base.RefreshTile(location + new Vector3Int(x, y, 0), tileMap);
                }
            }
        }
        else
        {
            base.RefreshTile(location, tileMap);
        }
    }
    
    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, ref Matrix4x4 transform)
    {
        // Check rule against rotations of 0, 90, 180, 270
        for (int angle = 0; angle <= (rule.AutoTransform == TilingRule.AutoTransformEnum.Rotated ? 270 : 0); angle += 90)
        {
            if (RuleMatches(rule, position, tilemap, angle))
            {
                transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, -angle), Vector3.one);
                return true;
            }
        }

        // Check rule against x-axis mirror
        if ((rule.AutoTransform == TilingRule.AutoTransformEnum.MirrorX) && RuleMatches(rule, position, tilemap, true, false))
        {
            transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(-1f, 1f, 1f));
            return true;
        }

        // Check rule against y-axis mirror
        if ((rule.AutoTransform == TilingRule.AutoTransformEnum.MirrorY) && RuleMatches(rule, position, tilemap, false, true))
        {
            transform = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(1f, -1f, 1f));
            return true;
        }

        return false;
    }
    
    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, int angle)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    Vector3Int rotated = GetRotatedPos(offset, angle);
                    int index = GetIndexOfOffset(rotated);
                    TileBase tile = tilemap.GetTile(position + offset);
                    if (rule.Neighbors[index] == TilingRule.NeighborEnum.This && tile != this || rule.Neighbors[index] == TilingRule.NeighborEnum.NotThis && tile == this)
                    {
                        return false;
                    }	
                }
            }
				
        }
        return true;
    }

    public bool RuleMatches(TilingRule rule, Vector3Int position, ITilemap tilemap, bool mirrorX, bool mirrorY)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                if (x != 0 || y != 0)
                {
                    Vector3Int offset = new Vector3Int(x, y, 0);
                    Vector3Int mirrored = GetMirroredPos(offset, mirrorX, mirrorY);
                    int index = GetIndexOfOffset(mirrored);
                    TileBase tile = tilemap.GetTile(position + offset);
                    if (rule.Neighbors[index] == TilingRule.NeighborEnum.This && tile != this || rule.Neighbors[index] == TilingRule.NeighborEnum.NotThis && tile == this)
                    {
                        return false;
                    }
                }
            }
        }
			
        return true;
    }
    
    int GetIndexOfOffset(Vector3Int offset)
    {
        int result = offset.x + 1 + (-offset.y + 1) * 3;
        if (result >= 4)
            result--;
        return result;
    }
    
    public Vector3Int GetRotatedPos(Vector3Int original, int rotation)
    {
        switch (rotation)
        {
            case 0:
                return original;
            case 90:
                return new Vector3Int(-original.y, original.x, original.z);
            case 180:
                return new Vector3Int(-original.x, -original.y, original.z);
            case 270:
                return new Vector3Int(original.y, -original.x, original.z);
        }
        return original;
    }

    public Vector3Int GetMirroredPos(Vector3Int original, bool mirrorX, bool mirrorY)
    {
        return new Vector3Int(original.x * (mirrorX ? -1 : 1), original.y * (mirrorY ? -1 : 1), original.z);
    }
    
}

[Serializable]
public class TilingRule
{
    public enum AutoTransformEnum
    {
        Fixed,
        Rotated,
        MirrorX,
        MirrorY
    }

    public enum NeighborEnum
    {
        DontCare,
        This,
        NotThis
    }

    public enum OutputSpriteEnum
    {
        Single,
        Random,
        Animation
    }

    public NeighborEnum[] Neighbors;
    public Sprite[] Sprites;
    public float AnimationSpeed;
    public float PerlinScale;
    public AutoTransformEnum AutoTransform;
    public OutputSpriteEnum Output;
    public Tile.ColliderType ColliderType;


    public TilingRule()
    {
        Output = OutputSpriteEnum.Single;
        Neighbors = new NeighborEnum[8];
        Sprites = new Sprite[1];
        AnimationSpeed = 1f;
        PerlinScale = 0.5f;
        ColliderType = Tile.ColliderType.None;

        for (int i = 0; i < Neighbors.Length; i++)
            Neighbors[i] = NeighborEnum.DontCare;
    }
}