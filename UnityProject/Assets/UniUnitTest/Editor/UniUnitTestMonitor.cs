#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using UnityEditor;
using UniUnitTest;

[InitializeOnLoad]
public class UniUnitTestMonitor : EditorWindow {
	static UniUnitTestMonitor ()
	{
		EditorApplication.hierarchyWindowItemOnGUI += DrawThing;
	}
	
	static private bool drawInfo = true;
	private const int ICON_WIDTH = 16;
	
	static void DrawThing (int id, Rect area)
	{
		TestRunner testRunner = Component.FindObjectOfType<TestRunner>();
		if(testRunner == null) return;
		
		drawInfo = EditorPrefs.GetBool ("UniUnitTest.drawHierarchy", true);
		if (!drawInfo)
			return;

		var go = EditorUtility.InstanceIDToObject (id) as GameObject;
		
		if (go == null)
			return;
		
		area.width = ICON_WIDTH;
		area.height = ICON_WIDTH;
		area.x -= ICON_WIDTH;
		
		if (go.GetComponent<TestSuite>())
		{
			TestSuite tsTemp = go.GetComponent<TestSuite>();
			
			switch(tsTemp.ExecutionStatus)
			{
			case TestBase.ExecutionStatusEnum.Idle:
				GUI.DrawTexture(area, testRunner.SuiteWait);
				break;
				
			case TestBase.ExecutionStatusEnum.Running:
				GUI.DrawTexture(area, testRunner.SuiteRunning);
				break;
				
			case TestBase.ExecutionStatusEnum.Completed:
				if(0 >= tsTemp.ErrorCount) {
					GUI.DrawTexture(area, testRunner.SuiteSuccessed);
				} else {
					GUI.DrawTexture(area, testRunner.SuiteFailed);					
				}
				break;
			}
		}
		if (go.GetComponent<TestCase>() && Application.isPlaying)
		{
			TestCase tcTemp = go.GetComponent<TestCase>();
			if (tcTemp.ExecutionStatus == TestBase.ExecutionStatusEnum.Running) {
				GUI.DrawTexture(area, testRunner.Running);
			} else if (tcTemp.ExecutionStatus == TestBase.ExecutionStatusEnum.Completed) {
				GUI.DrawTexture(area, (tcTemp.ErrorCount > 0 ? testRunner.Failed : testRunner.Successed));
			}
			
		}
	}
}
#endif