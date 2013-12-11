using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace UniUnitTest {
	public class TestSuite : TestBase
	{
		override public int ErrorCount
		{
			get {
				int tErrorCount = 0;
				foreach(TestBase tTest in ChildrenTestCases) {
					tErrorCount += tTest.ErrorCount;
				}
			
				return tErrorCount;
			}
		}
		
		override public int AllCount
		{
			get {
				int tCount = 0;
				foreach(TestBase tTest in ChildrenTestCases) {
					tCount += tTest.AllCount;
				}
			
				return tCount;
			}
		}
		
		private IEnumerable<TestBase> ChildrenTestCases
		{
			get {
				List<TestBase> tRet = new List<TestBase>();
				
				//TestBase[] tTests = GetComponentsInChildren<TestBase>();
				List<TestBase> tTests = GetComponentsInChildren<TestBase>().ToList();
				// shuffle the tests, to make sure that the order doesn't affect the result
				tTests.Shuffle();
				foreach(TestBase tTest in tTests) {
					if(this != tTest && tTest.gameObject.transform.parent == gameObject.transform) {
						tRet.Add(tTest);
					}
				}
				
				return tRet;
			}
		}
		
		override protected IEnumerator innerRun()
		{
			foreach(TestBase tTest in ChildrenTestCases) {
				tTest.ExecutionStatus = TestBase.ExecutionStatusEnum.Running;
				yield return tTest.StartCoroutine(tTest.Run());
				tTest.ExecutionStatus = TestBase.ExecutionStatusEnum.Completed;
			}
		}
	}

}