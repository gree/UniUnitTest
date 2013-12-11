using UnityEngine;
using System;
using System.Collections;

namespace UniUnitTest {
	public class TestRunner : MonoBehaviour
	{
		public static TestRunner Instance
		{
			get {
				return mInstance;
			}
		}
		private static TestRunner mInstance = null;
		
		//最初に実行するテストスイートを指定します 
		public TestBase RunSuite;
		public Texture2D SuiteWait, SuiteRunning, SuiteFailed, SuiteSuccessed, Running, Successed, Failed;
						
		private int count = 0;
		//現在実行中のテスト 
		private TestCase runnningTest;
		public TestCase RunningTest {
			get{ return runnningTest; }
			set{
				runnningTest = value;
				if(null!=runnningTest) {
					count++;
					runnningTest.Order = count;
					
				}
			}
		}
		
		void Awake()
		{
			if(mInstance != null) {
				throw new Exception("once instance only");
			}
			mInstance = this;
		}
		
		IEnumerator Start()
		{
			Application.RegisterLogCallback(HandleLog);
			if(null != RunSuite) {
				yield return StartCoroutine(RunSuite.Run());
			}
			RunningTest = null;
			
			if(0 < RunSuite.ErrorCount) {
				Debug.Log(string.Format("[UnitTest]Test Failed!! ({0}/{1})",
					RunSuite.ErrorCount,
					RunSuite.AllCount));
			} else {
				Debug.Log(string.Format("[UnitTest]Test Success! TestCount:{0}",
					RunSuite.AllCount));
			}
		}
		
		/*
			実際の画面でテストできるように
		*/
		Vector2 pos = new Vector2();
		void OnGUI()
		{
			pos = GUILayout.BeginScrollView(pos);
			if(RunSuite is TestSuite) {
				displayTestSuite((TestSuite)RunSuite, 1);
			} else {
				//GUILayout.Label(RunSuite.name);
			}
			GUILayout.EndScrollView();
		}
		
		private void displayTestSuite(TestSuite test_suite, int renge)
		{
			GUI.contentColor = test_suite.Bar;
			GUILayout.BeginHorizontal();
			GUILayout.Space(32 * (renge - 1));
			GUILayout.Label(test_suite.name);
			GUILayout.EndHorizontal();
			foreach(TestSuite test in test_suite.GetComponentsInChildren<TestSuite>()) {
				if(test_suite.transform == test.transform.parent) {
					displayTestSuite(test, renge + 1);
				}
			}
			foreach(TestCase test in test_suite.GetComponentsInChildren<TestCase>()) {
				if(test_suite.transform == test.transform.parent) {
					GUI.contentColor = test.Bar;
					GUILayout.BeginHorizontal();
					GUILayout.Space(32 * renge);
					GUILayout.Label(test.name);
					GUILayout.EndHorizontal();
				}
			}
			
		}
		
		private void HandleLog(string pLogString, string pStackTrace, LogType pType)
		{
			if(RunningTest == null) {
				return;
			}
			TestCase.LogData tData = new TestCase.LogData();
			tData.Message = pLogString;
			tData.StackTrace = pStackTrace;
			RunningTest.RunningMethod.AllLogs.Add(tData);
			switch (pType) {
				case LogType.Error:
				case LogType.Exception:
					RunningTest.RunningMethod.Errors.Add(tData);
					break;
				case LogType.Warning:
					break;
				case LogType.Log:
					break;
				default:
					break;
			}
		}
	}
}
