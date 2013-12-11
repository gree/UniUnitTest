#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using UniUnitTest;

public class TestEditor : EditorWindow
{
	/*
		外部からコマンドライン実行する時に使います。
		シーンを指定して外部にこの関数を公開してください。
		例:
			[MenuItem("Script/Unit Test Run")]
			public static void FunctionalUnitTestRun()
			{
				AssetDatabase.Refresh();
				TestEditor.TestRun("Assets/Scenes/Test/Test.unity");
			}
	*/
	static public void TestRun(string scene_path)
	{
		EditorApplication.OpenScene(scene_path);
		EditorApplication.isPlaying = true;
		EditorWindow.GetWindow<TestEditor>(false, "TestEditor");
	}
	
	void Update()
	{
		if(null != TestRunner.Instance && null == TestRunner.Instance.RunningTest) {
			EditorApplication.isPlaying = false;
			if(0 < TestRunner.Instance.RunSuite.ErrorCount) {
				EditorApplication.Exit(1);
			} else {
				EditorApplication.Exit(0);
			}
		}
	}
	
}
#endif
