using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Tilemaps;
using Object = UnityEngine.Object;

[CustomEditor(typeof(TileRules))]
[CanEditMultipleObjects]
public class TileRulesEditor : Editor
{
	
	class MenuItemData
	{
		public TilingRule Rule;
		public TilingRule.AutoTransformEnum NewValue;

		public MenuItemData(TilingRule rule, TilingRule.AutoTransformEnum newValue)
		{
			Rule = rule;
			NewValue = newValue;
		}
	}
	
	const string s_XIconString = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABoSURBVDhPnY3BDcAgDAOZhS14dP1O0x2C/LBEgiNSHvfwyZabmV0jZRUpq2zi6f0DJwdcQOEdwwDLypF0zHLMa9+NQRxkQ+ACOT2STVw/q8eY1346ZlE54sYAhVhSDrjwFymrSFnD2gTZpls2OvFUHAAAAABJRU5ErkJggg==";
	const string s_Arrow0 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACYSURBVDhPzZExDoQwDATzE4oU4QXXcgUFj+YxtETwgpMwXuFcwMFSRMVKKwzZcWzhiMg91jtg34XIntkre5EaT7yjjhI9pOD5Mw5k2X/DdUwFr3cQ7Pu23E/BiwXyWSOxrNqx+ewnsayam5OLBtbOGPUM/r93YZL4/dhpR/amwByGFBz170gNChA6w5bQQMqramBTgJ+Z3A58WuWejPCaHQAAAABJRU5ErkJggg==";
	const string s_Arrow1 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABqSURBVDhPxYzBDYAgEATpxYcd+PVr0fZ2siZrjmMhFz6STIiDs8XMlpEyi5RkO/d66TcgJUB43JfNBqRkSEYDnYjhbKD5GIUkDqRDwoH3+NgTAw+bL/aoOP4DOgH+iwECEt+IlFmkzGHlAYKAWF9R8zUnAAAAAElFTkSuQmCC";
	const string s_Arrow2 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAAC0SURBVDhPjVE5EsIwDMxPKFKYF9CagoJH8xhaMskLmEGsjOSRkBzYmU2s9a58TUQUmCH1BWEHweuKP+D8tphrWcAHuIGrjPnPNY8X2+DzEWE+FzrdrkNyg2YGNNfRGlyOaZDJOxBrDhgOowaYW8UW0Vau5ZkFmXbbDr+CzOHKmLinAXMEePyZ9dZkZR+s5QX2O8DY3zZ/sgYcdDqeEVp8516o0QQV1qeMwg6C91toYoLoo+kNt/tpKQEVvFQAAAAASUVORK5CYII=";
	const string s_Arrow3 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAAB2SURBVDhPzY1LCoAwEEPnLi48gW5d6p31bH5SMhp0Cq0g+CCLxrzRPqMZ2pRqKG4IqzJc7JepTlbRZXYpWTg4RZE1XAso8VHFKNhQuTjKtZvHUNCEMogO4K3BhvMn9wP4EzoPZ3n0AGTW5fiBVzLAAYTP32C2Ay3agtu9V/9PAAAAAElFTkSuQmCC";
	const string s_Arrow5 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABqSURBVDhPnY3BCYBADASvFx924NevRdvbyoLBmNuDJQMDGjNxAFhK1DyUQ9fvobCdO+j7+sOKj/uSB+xYHZAxl7IR1wNTXJeVcaAVU+614uWfCT9mVUhknMlxDokd15BYsQrJFHeUQ0+MB5ErsPi/6hO1AAAAAElFTkSuQmCC";
	const string s_Arrow6 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACaSURBVDhPxZExEkAwEEVzE4UiTqClUDi0w2hlOIEZsV82xCZmQuPPfFn8t1mirLWf7S5flQOXjd64vCuEKWTKVt+6AayH3tIa7yLg6Qh2FcKFB72jBgJeziA1CMHzeaNHjkfwnAK86f3KUafU2ClHIJSzs/8HHLv09M3SaMCxS7ljw/IYJWzQABOQZ66x4h614ahTCL/WT7BSO51b5Z5hSx88AAAAAElFTkSuQmCC";
	const string s_Arrow7 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAABQSURBVDhPYxh8QNle/T8U/4MKEQdAmsz2eICx6W530gygr2aQBmSMphkZYxqErAEXxusKfAYQ7XyyNMIAsgEkaYQBkAFkaYQBsjXSGDAwAAD193z4luKPrAAAAABJRU5ErkJggg==";
	const string s_Arrow8 = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAYdEVYdFNvZnR3YXJlAHBhaW50Lm5ldCA0LjAuNWWFMmUAAACYSURBVDhPxZE9DoAwCIW9iUOHegJXHRw8tIdx1egJTMSHAeMPaHSR5KVQ+KCkCRF91mdz4VDEWVzXTBgg5U1N5wahjHzXS3iFFVRxAygNVaZxJ6VHGIl2D6oUXP0ijlJuTp724FnID1Lq7uw2QM5+thoKth0N+GGyA7IA3+yM77Ag1e2zkey5gCdAg/h8csy+/89v7E+YkgUntOWeVt2SfAAAAABJRU5ErkJggg==";
	const string s_MirrorX = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAG1JREFUOE+lj9ENwCAIRB2IFdyRfRiuDSaXAF4MrR9P5eRhHGb2Gxp2oaEjIovTXSrAnPNx6hlgyCZ7o6omOdYOldGIZhAziEmOTSfigLV0RYAB9y9f/7kO8L3WUaQyhCgz0dmCL9CwCw172HgBeyG6oloC8fAAAAAASUVORK5CYII=";
	const string s_MirrorY = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwgAADsIBFShKgAAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAG9JREFUOE+djckNACEMAykoLdAjHbPyw1IOJ0L7mAejjFlm9hspyd77Kk+kBAjPOXcakJIh6QaKyOE0EB5dSPJAiUmOiL8PMVGxugsP/0OOib8vsY8yYwy6gRyC8CB5QIWgCMKBLgRSkikEUr5h6wOPWfMoCYILdgAAAABJRU5ErkJggg==";
	const string s_Rotated = "iVBORw0KGgoAAAANSUhEUgAAAA8AAAAPCAYAAAA71pVKAAAABGdBTUEAALGPC/xhBQAAAAlwSFlzAAAOwQAADsEBuJFr7QAAABh0RVh0U29mdHdhcmUAcGFpbnQubmV0IDQuMC41ZYUyZQAAAHdJREFUOE+djssNwCAMQxmIFdgx+2S4Vj4YxWlQgcOT8nuG5u5C732Sd3lfLlmPMR4QhXgrTQaimUlA3EtD+CJlBuQ7aUAUMjEAv9gWCQNEPhHJUkYfZ1kEpcxDzioRzGIlr0Qwi0r+Q5rTgM+AAVcygHgt7+HtBZs/2QVWP8ahAAAAAElFTkSuQmCC";

	static Texture2D[] arrows;

	public static Texture2D[] Arrows
	{
		get
		{
			if (arrows == null)
			{
				arrows = new Texture2D[10];
				arrows[0] = Base64ToTexture(s_Arrow0);
				arrows[1] = Base64ToTexture(s_Arrow1);
				arrows[2] = Base64ToTexture(s_Arrow2);
				arrows[3] = Base64ToTexture(s_Arrow3);
				arrows[5] = Base64ToTexture(s_Arrow5);
				arrows[6] = Base64ToTexture(s_Arrow6);
				arrows[7] = Base64ToTexture(s_Arrow7);
				arrows[8] = Base64ToTexture(s_Arrow8);
				arrows[9] = Base64ToTexture(s_XIconString);
			}

			return arrows;
		}
	}

	static Texture2D[] autoTransforms;

	public static Texture2D[] AutoTransforms
	{
		get
		{
			if (autoTransforms == null)
			{
				autoTransforms = new Texture2D[3];
				autoTransforms[0] = Base64ToTexture(s_Rotated);
				autoTransforms[1] = Base64ToTexture(s_MirrorX);
				autoTransforms[2] = Base64ToTexture(s_MirrorY);
			}

			return autoTransforms;
		}
	}

	ReorderableList reorderableList;
	public TileRules tile { get { return (target as TileRules); } }
	Rect listRect;

	const float defaultElementHeight = 48f;
	const float paddingBetweenRules = 13f;
	const float singleLineHeight = 16f;
	const float labelWidth = 53f;

	public void OnEnable()
	{
		if (tile.TilingRules == null)
			tile.TilingRules = new List<TilingRule>();

		reorderableList = new ReorderableList(tile.TilingRules, typeof(TilingRule), true, true, true, true);
		reorderableList.drawHeaderCallback = OnDrawHeader;
		reorderableList.drawElementCallback = OnDrawElement;
		reorderableList.elementHeightCallback = GetElementHeight;
		reorderableList.onReorderCallback = ListUpdated;
	}

	void ListUpdated(ReorderableList list)
	{
		SaveTile();
	}

	float GetElementHeight(int index)
	{
		if (tile.TilingRules != null && tile.TilingRules.Count > 0)
		{
			switch (tile.TilingRules[index].Output)
			{
				case TilingRule.OutputSpriteEnum.Random:
					return defaultElementHeight + singleLineHeight * (tile.TilingRules[index].Sprites.Length + 2) + paddingBetweenRules;
				case TilingRule.OutputSpriteEnum.Animation:
					return defaultElementHeight + singleLineHeight * (tile.TilingRules[index].Sprites.Length + 2) + paddingBetweenRules;
			}
		}

		return defaultElementHeight + paddingBetweenRules;
	}

	void OnDrawElement(Rect rect, int index, bool isactive, bool isfocused)
	{
		TilingRule rule = tile.TilingRules[index];

		float yPos = rect.yMin + 2f;
		float height = rect.height - paddingBetweenRules;
		float matrixWidth = defaultElementHeight;

		Rect inspectorRect = new Rect(rect.xMin, yPos, rect.width - matrixWidth * 2f - 20f, height);
		Rect matrixRect = new Rect(rect.xMax - matrixWidth * 2f - 10f, yPos, matrixWidth, defaultElementHeight);
		Rect spriteRect = new Rect(rect.xMax - matrixWidth - 5f, yPos, matrixWidth, defaultElementHeight);

		EditorGUI.BeginChangeCheck();
		RuleInspectorOnGUI(inspectorRect, rule);
		RuleMatrixOnGUI(matrixRect, rule);
		SpriteOnGUI(spriteRect, rule);
		if (EditorGUI.EndChangeCheck())
			SaveTile();
	}

	void SaveTile()
	{
		EditorUtility.SetDirty(target);
		SceneView.RepaintAll();
	}

	void OnDrawHeader(Rect rect)
	{
		GUI.Label(rect, "Tiling Rules");
	}

	public override void OnInspectorGUI()
	{
		tile.DefaultSprite = EditorGUILayout.ObjectField("Default Sprite", tile.DefaultSprite, typeof(Sprite), false) as Sprite;
		tile.DefaultColliderType = (Tile.ColliderType) EditorGUILayout.EnumPopup("Default Collider", tile.DefaultColliderType);
		EditorGUILayout.Space();

		if (reorderableList != null && tile.TilingRules != null)
			reorderableList.DoLayoutList();
	}

	static void RuleMatrixOnGUI(Rect rect, TilingRule tilingRule)
	{
		Handles.color = EditorGUIUtility.isProSkin ? new Color(1f, 1f, 1f, 0.2f) : new Color(0f, 0f, 0f, 0.2f);
		int index = 0;
		float w = rect.width / 3f;
		float h = rect.height / 3f;

		for (int y = 0; y <= 3; y++)
		{
			float top = rect.yMin + y * h;
			Handles.DrawLine(new Vector3(rect.xMin, top), new Vector3(rect.xMax, top));
		}

		for (int x = 0; x <= 3; x++)
		{
			float left = rect.xMin + x * w;
			Handles.DrawLine(new Vector3(left, rect.yMin), new Vector3(left, rect.yMax));
		}

		Handles.color = Color.white;

		for (int y = 0; y <= 2; y++)
		{
			for (int x = 0; x <= 2; x++)
			{
				Rect r = new Rect(rect.xMin + x * w, rect.yMin + y * h, w - 1, h - 1);
				if (x != 1 || y != 1)
				{
					switch (tilingRule.Neighbors[index])
					{
						case TilingRule.NeighborEnum.This:
							GUI.DrawTexture(r, Arrows[y * 3 + x]);
							break;
						case TilingRule.NeighborEnum.NotThis:
							GUI.DrawTexture(r, Arrows[9]);
							break;
					}

					if (Event.current.type == EventType.MouseDown && r.Contains(Event.current.mousePosition))
					{
						tilingRule.Neighbors[index] = (TilingRule.NeighborEnum) (((int) tilingRule.Neighbors[index] + 1) % 3);
						GUI.changed = true;
						Event.current.Use();
					}

					index++;
				}
				else
				{
					switch (tilingRule.AutoTransform)
					{
						case TilingRule.AutoTransformEnum.Rotated:
							GUI.DrawTexture(r, AutoTransforms[0]);
							break;
						case TilingRule.AutoTransformEnum.MirrorX:
							GUI.DrawTexture(r, AutoTransforms[1]);
							break;
						case TilingRule.AutoTransformEnum.MirrorY:
							GUI.DrawTexture(r, AutoTransforms[2]);
							break;
					}

					if (Event.current.type == EventType.MouseDown && r.Contains(Event.current.mousePosition))
					{
						tilingRule.AutoTransform = (TilingRule.AutoTransformEnum) (((int) tilingRule.AutoTransform + 1) % 4);
						GUI.changed = true;
						Event.current.Use();
					}
				}
			}
		}
	}

	static void OnSelect(object userdata)
	{
		MenuItemData data = (MenuItemData) userdata;
		data.Rule.AutoTransform = data.NewValue;
	}
	
	void SpriteOnGUI(Rect rect, TilingRule tilingRule)
	{
		tilingRule.Sprites[0] = EditorGUI.ObjectField(new Rect(rect.xMax - rect.height, rect.yMin, rect.height, rect.height), tilingRule.Sprites[0], typeof (Sprite), false) as Sprite;
	}
	
	static void RuleInspectorOnGUI(Rect rect, TilingRule tilingRule)
		{
			float y = rect.yMin;
			EditorGUI.BeginChangeCheck();
			GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Rule");
			tilingRule.AutoTransform = (TilingRule.AutoTransformEnum)EditorGUI.EnumPopup(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.AutoTransform);
			y += singleLineHeight;
			GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Collider");
			tilingRule.ColliderType = (Tile.ColliderType)EditorGUI.EnumPopup(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.ColliderType);
			y += singleLineHeight;
			GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Output");
			tilingRule.Output = (TilingRule.OutputSpriteEnum)EditorGUI.EnumPopup(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.Output);
			y += singleLineHeight;

			if (tilingRule.Output == TilingRule.OutputSpriteEnum.Animation)
			{
				GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Speed");
				tilingRule.AnimationSpeed = EditorGUI.FloatField(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.AnimationSpeed);
				y += singleLineHeight;
			}
			if (tilingRule.Output == TilingRule.OutputSpriteEnum.Random)
			{
				GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Noise");
				tilingRule.PerlinScale = EditorGUI.Slider(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.PerlinScale, 0.001f, 0.999f);
				y += singleLineHeight;
			}

			if (tilingRule.Output != TilingRule.OutputSpriteEnum.Single)
			{
				GUI.Label(new Rect(rect.xMin, y, labelWidth, singleLineHeight), "Size");
				EditorGUI.BeginChangeCheck();
				int newLength = EditorGUI.IntField(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.Sprites.Length);
				if (EditorGUI.EndChangeCheck())
					Array.Resize(ref tilingRule.Sprites, Math.Max(newLength, 1));
				y += singleLineHeight;

				for (int i = 0; i < tilingRule.Sprites.Length; i++)
				{
					tilingRule.Sprites[i] = EditorGUI.ObjectField(new Rect(rect.xMin + labelWidth, y, rect.width - labelWidth, singleLineHeight), tilingRule.Sprites[i], typeof(Sprite), false) as Sprite;
					y += singleLineHeight;
				}
			}
		}
	
	public override Texture2D RenderStaticPreview(string assetPath, Object[] subAssets, int width, int height)
	{
		if (tile.DefaultSprite != null)
		{
			Type t = GetType("UnityEditor.SpriteUtility");
			if (t != null)
			{
				MethodInfo method = t.GetMethod("RenderStaticPreview", new Type[] {typeof (Sprite), typeof (Color), typeof (int), typeof (int)});
				if (method != null)
				{
					object ret = method.Invoke("RenderStaticPreview", new object[] {tile.DefaultSprite, Color.white, width, height});
					if (ret is Texture2D)
						return ret as Texture2D;
				}
			}
		}
		return base.RenderStaticPreview(assetPath, subAssets, width, height);
	}
	
	static Type GetType(string TypeName)
	{
		var type = Type.GetType(TypeName);
		if (type != null)
			return type;

		if (TypeName.Contains("."))
		{
			var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));
			var assembly = Assembly.Load(assemblyName);
			if (assembly == null)
				return null;
			type = assembly.GetType(TypeName);
			if (type != null)
				return type;
		}

		var currentAssembly = Assembly.GetExecutingAssembly();
		var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
		foreach (var assemblyName in referencedAssemblies)
		{
			var assembly = Assembly.Load(assemblyName);
			if (assembly != null)
			{
				type = assembly.GetType(TypeName);
				if (type != null)
					return type;
			}
		}
		return null;
	}

	static Texture2D Base64ToTexture(string base64)
	{
		Texture2D t = new Texture2D(1, 1);
		t.hideFlags = HideFlags.HideAndDontSave;
		t.LoadImage(System.Convert.FromBase64String(base64));
		return t;
	}

}