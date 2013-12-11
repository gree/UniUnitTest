using UnityEngine;
using System;
using System.Collections;
using UniUnitTest;

public class Test1 : TestCase
{
	[Setup]
	public void SetUp1()
	{
		Debug.Log("-SetUp1");
	}
	
	[Setup]
	public IEnumerator SetUp2()
	{
		yield return null;
		Debug.Log("-SetUp2");
	}
	
	[TearDown]
	public void TearDown1()
	{
		Debug.Log("-TearDown1");
	}
	
	[TearDown]
	public IEnumerator TearDown2()
	{
		yield return null;
		Debug.Log("-TearDown2");
	}
	
	[Test]
	public void SuccessFunction()
	{
		Debug.Log("SuccessFunction");
	}
	
	[Test]
	public IEnumerator SuccessCoroutine()
	{
		Debug.Log("SuccessCoroutine1");
		yield return null;
		Debug.Log("SuccessCoroutine2");
	}
	
	/*この3つのテストは必ず失敗します
	[Test]
	public void FailFunction()
	{
		Debug.Log("FailFunction");
		throw new Exception();
	}
	
	[Test, Timeout(0.1f)]
	public IEnumerator TimeoutTest()
	{
		Debug.Log("TimeoutTest");
		yield return new WaitForSeconds(1f);
	}
	
	[Test]
	public IEnumerator FailCoroutine()
	{
		Debug.Log("SuccessCoroutine1");
		yield return null;
		throw new Exception();
	}
	//*/
	
}
