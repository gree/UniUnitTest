using System;

namespace UniUnitTest
{
	/*
	 * この属性がついたTestCase継承クラスのメソットがテストされる
	 * */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class Test : Attribute
	{
	}
	
	/*
	 * コルーチンのテストの時にタイムアウト時間を設定する。この属性がない場合、デフォルト時間が使われる
	 * */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class Timeout : Attribute
	{
		public Timeout(float pTime)
		{
			Time = pTime;
		}
		
		public float Time;
	}
	
	/*
	 * テスト関数が実行される前に呼ばれる関数につける属性
	 * */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class Setup : Attribute
	{
	}
	
	/*
	 * テスト関数が実行された後に呼ばれる関数につける属性
	 * */
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	public class TearDown : Attribute
	{
	}
}