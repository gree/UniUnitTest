using UnityEngine;
using System.Collections;

namespace UniUnitTest
{
	public class TestBase : MonoBehaviour
	{
		public enum ExecutionStatusEnum
		{
			Idle = 0,
			Running = 1,
			Completed = 2
		}
		public Color Bar;
		private string mOriginName;
		public ExecutionStatusEnum ExecutionStatus;
		
		public void Awake()
		{
			Bar = Color.white;
		}
		
		public IEnumerator Run()
		{
			mOriginName = name;
			name = mOriginName;
			Bar = new Color(1f, 0.5f, 0f);
			ExecutionStatus = ExecutionStatusEnum.Running;
			yield return StartCoroutine(innerRun());
			if(0 == ErrorCount) {
				Bar = Color.green;
				name = mOriginName;
			} else {
				Bar = Color.red;
				name = mOriginName;
			}
			ExecutionStatus = ExecutionStatusEnum.Completed;
		}
		
		protected virtual IEnumerator innerRun()
		{
			yield return null;
		}
		
		public virtual int ErrorCount
		{
			get{return 0;}
		}
		
		public virtual int AllCount
		{
			get{return 0;}
		}
	}
}
