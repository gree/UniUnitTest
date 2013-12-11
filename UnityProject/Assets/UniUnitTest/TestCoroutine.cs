using UnityEngine;
using System;
using System.Collections;
using System.Reflection;

namespace UniUnitTest
{
	/*
	 * テスト用のコルーチンを一つ保持する
	 * */
	public class TestCoroutine
	{
		private const float DEFAULT_TIMEOUT = 2.0f;
		
		private enum State
		{
			WaitStart,
			Progress,
			Success,
			Fail,
		}
		
		private TestCase mTest;
		private MethodInfo mMethod;
		private State mState;
		private MonoBehaviour mChild;
		
		public TestCoroutine(TestCase pTest, MethodInfo pMethod)
		{
			mTest = pTest;
			mMethod = pMethod;
			mState = State.WaitStart;
		}
		
		public IEnumerator Run()
		{
			TestRunner.Instance.RunningTest = mTest;
			mTest.RunningMethod.Bar = new Color(1f, 0.5f, 0.0f);
			if(mMethod.ReturnType == typeof(IEnumerator)) {
				yield return mTest.StartCoroutine(RunCoroutine());
			} else {
				mMethod.Invoke(mTest, null);
			}
		}
		
		/*
		 * テスト関数がコルーチンだった場合の実行処理
		 * */
		private IEnumerator RunCoroutine()
		{
			//コルーチンを一斉に止めるためゲームオブジェクトを作成
			mChild = new GameObject().AddComponent<MonoBehaviour>();
			mChild.gameObject.transform.parent = mTest.gameObject.transform;
			
			mState = State.Progress;
			mChild.StartCoroutine(WaitSuccsess());
			mChild.StartCoroutine(WaitError()); //Catch for exception inside the coroutine.
			
			float tTime = DEFAULT_TIMEOUT;
			Attribute tAttribute = Attribute.GetCustomAttribute(mMethod, typeof(Timeout));
			if(tAttribute is Timeout) {
				tTime = ((Timeout)tAttribute).Time;
			}
			mChild.StartCoroutine(TimeoutCounter(tTime));
			
			while(mState != State.Success && mState != State.Fail) {
				yield return null;
			}
			mChild.StopAllCoroutines();
			GameObject.Destroy(mChild.gameObject);
		}
		
		/*
		 * テスト関数の成功を待つ
		 * */
		private IEnumerator WaitSuccsess()
		{
			yield return mChild.StartCoroutine((IEnumerator)mMethod.Invoke(mTest, null));
			mState = State.Success;
		}
		
		/*
		 * テスト関数のタイムアウトを待つ
		 * */
		public IEnumerator TimeoutCounter(float pTime)
		{
			yield return new WaitForSeconds(pTime);
			mState = State.Fail;
			Debug.LogError("test is timeout! time:" + pTime);
		}

		private IEnumerator WaitError()
		{
			while(mTest.ErrorCount <= 0) {
				yield return null;
			}
			mState = State.Fail;
		}
	}
}