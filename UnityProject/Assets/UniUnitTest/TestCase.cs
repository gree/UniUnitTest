using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace UniUnitTest
{
	public class TestCase : TestBase
	{
		[Serializable]
		public class MethodData
		{
			public string Name;
			public Color Bar;
			public List<LogData> AllLogs = new List<LogData>();
			public List<LogData> Errors  = new List<LogData>();
		}
		
		[Serializable]
		public class LogData
		{
			public string Message;
			public string StackTrace;
		}
		
		public int Order;
		public MethodData RunningMethod;
		public List<MethodData> Logs = new List<MethodData>();
		
		override public int ErrorCount
		{
			get {
				int tRet = 0;
				foreach(MethodData tLog in Logs) {
					tRet += tLog.Errors.Count;
				}
				return tRet;
			}
		}
		
		override public int AllCount
		{
			get {
				return Logs.Count;
			}
		}
		
		
		/*
		 * このクラスのテスト関数をすべて実行
		 * */
		override protected IEnumerator innerRun()
		{
			List<MethodInfo> tMethods = GetType().GetMethods().ToList();//BindingFlags.Public | );
			// shuffle the tests, so we make sure there's no dependency between them
			tMethods.Shuffle();
			foreach(MethodInfo tMethod in tMethods) {
				Attribute tAttribute = Attribute.GetCustomAttribute(tMethod, typeof(Test));
				if(tAttribute is Test) {
					
					//記録をとるための箱を作成
					AddRunMethodLog(tMethod.Name, new MethodData());
					
					//setup
					yield return StartCoroutine(SetUp(tMethods));
					
					//テスト一つ
					TestCoroutine tCoroutine = new TestCoroutine(this, tMethod);
					yield return StartCoroutine(tCoroutine.Run());
					
					//tearDown
					yield return StartCoroutine(TearDown(tMethods));
					
					ApplyRunningMethodBar();
				}
			}
		}
		
		/*
		 * 記録をとるための箱を作成
		 * */
		private void AddRunMethodLog(string pName, MethodData pMethodData)
		{
			RunningMethod = pMethodData;
			pMethodData.Name = pName;
			Logs.Add(pMethodData);
		}
		
		/*
		 * メソッドの成功・失敗判定のバーの色を変更
		 * */
		private void ApplyRunningMethodBar()
		{
			if(RunningMethod.Errors.Count == 0) {
				RunningMethod.Bar = Color.green;
			} else {
				RunningMethod.Bar = Color.red;
			}
		}
		
		/*
		 * Setupの付いているメソッドをすべて呼ぶ
		 * */
		public IEnumerator SetUp(List<MethodInfo> pMethods)
		{
			foreach(MethodInfo tMethod in pMethods) {
				Attribute tAttribute = Attribute.GetCustomAttribute(tMethod, typeof(Setup));
				if(tAttribute is Setup) {
					TestCoroutine tCoroutine = new TestCoroutine(this, tMethod);
					yield return StartCoroutine(tCoroutine.Run());
				}
			}
		}
		
		/*
		 * TearDownの付いているメソッドをすべて呼ぶ
		 * */
		public IEnumerator TearDown(List<MethodInfo> pMethods)
		{
			foreach(MethodInfo tMethod in pMethods) {
				Attribute tAttribute = Attribute.GetCustomAttribute(tMethod, typeof(TearDown));
				if(tAttribute is TearDown) {
					TestCoroutine tCoroutine = new TestCoroutine(this, tMethod);
					yield return StartCoroutine(tCoroutine.Run());
				}
			}
		}
	}
}
