using System;
using System.Collections;
using NUnit.Framework;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class CSVTest
{

	public class TestObject
	{
		public string StringField;
		public int IntField;
		public float FloatField;

		public enum Colour
		{
			Red = 1,
			Green = 2,
			Blue = 3,
			Purple = 15
		}

		public Colour EnumField;

		public TestObject()
		{
		}

		public TestObject(string s, int i, float f, Colour c)
		{
			StringField = s;
			IntField = i;
			FloatField = f;
			EnumField = c;
		}
	}

	int TestStruct<T>(string fileName) where T : new()
	{
		List<T> structs = CSVParser.LoadObjects<T>(fileName + ".csv");
		int lineCount = File.ReadAllLines("Assets/Resources/Data/CSV/" + fileName + ".csv").Length;
		Assert.IsNotEmpty(structs);
		Assert.IsTrue(structs.Count == lineCount - 1);

		foreach (T t in structs)
		{
			Debug.Log(t.ToString());
		}

		return structs.Count;
	}

	int TestScriptableObject<T>(string name) where T : ScriptableObject
	{
		T data = Resources.Load<T>("Data/ScriptableObject/" + name);
		MethodInfo methodInfo = data.GetType().GetMethod("Load");
		Assert.IsNotNull(data);
		Assert.IsNotNull(methodInfo);
		IList list = (IList) methodInfo.Invoke(data, null);
		Assert.IsNotNull(list);
		return list.Count;
	}

	[Test]
	public void TestNPCData()
	{
		Assert.AreEqual(
			TestStruct<NPCStruct>("NPC"),
			TestScriptableObject<NPCData>("NPC")
		);
	}

	[Test]
	public void TestAbnormalData()
	{
		Assert.AreEqual
		(
			TestStruct<AbnormalStruct>("Abnormal"),
			TestScriptableObject<AbnormalData>("Abnormal")
		);
	}

	[Test]
	public void TestGimmickData()
	{
		Assert.AreEqual
		(
			TestStruct<GimmickStruct>("Gimmick"),
			TestScriptableObject<GimmickData>("Gimmick")
		);
	}

	[Test]
	public void TestPlaceDivisionData()
	{
		Assert.AreEqual
		(
			TestStruct<PlaceDivisionStruct>("PlaceDivision"),
			TestScriptableObject<PlaceDivisionData>("PlaceDivision")
		);
	}

	[Test]
	public void TestLivingEntityData()
	{
		Assert.AreEqual
		(
			TestStruct<LivingEntityStruct>("PC"),
			TestScriptableObject<LivingEntityData>("LivingEntity")
		);
	}

	[Test]
	public void TestSkillData()
	{
		Assert.AreEqual
		(
			TestStruct<SkillStruct>("Skill"),
			TestScriptableObject<SkillData>("Skill")
		);
	}
	
	[Test]
	public void TestTubeData()
	{
		Assert.AreEqual
		(
			TestStruct<TubeStruct>("Tube"),
			TestScriptableObject<TubeData>("Tube")
		);
	}
}