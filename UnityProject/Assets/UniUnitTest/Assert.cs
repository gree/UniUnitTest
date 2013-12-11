using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace UniUnitTest {

	static public class Assert
	{
		public static void GreaterThan<T>(T obj1, T obj2, string msg ="") where T : IComparable
		{
			if(obj1.CompareTo(obj2) > 0) {
				return;
			}
			throw new UniUnitTest.AssertException(
				AssertErrorTypeEnum.Failed,
				String.Format("[UnitTest] Assertion failed! {2}  {0}>={1}", obj1, obj2, msg));
		}

		public static void LessThan<T>(T obj1, T obj2, string msg ="") where T : IComparable
		{
			if(obj1.CompareTo(obj2) < 0) {
				return;
			}
			throw new UniUnitTest.AssertException(
				AssertErrorTypeEnum.Failed,
				String.Format("[UnitTest] Assertion failed! {2}  {0}>={1}", obj1, obj2, msg));
		
		}
		
		
		/*
			大体一致することを調べる（小数の場合はこちらで）
		*/
		static public void EqualDouble(double l, double r, string msg = "", double margin = 0.05)
		{
			if(Math.Abs(l - r) < margin) {
				return;
			}
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, String.Format("[UnitTest] Assertion failed! {2}  {0}!={1}", l, r, msg));
		}
		
		/*
			大体一致することを調べる（小数の場合はこちらで）
		*/
		static public void EqualFloat(float l, float r, string msg = "", float margin = 0.05f)
		{
			if(Math.Abs(l - r) < margin) {
				return;
			}
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, String.Format("[UnitTest] Assertion failed! {2}  {0}!={1}", l, r, msg));
		}
		

		/*
			一致することを調べる
		*/
		static public void Equal<T>(T obj1, T obj2, string msg ="")
		{
			if(obj1 == null && obj2 == null) {
				return;
			}
			if(obj1 == null || obj1 == null) {
				throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, String.Format("[UnitTest] Assertion failed! {2}  {0}!={1}", obj1, obj2, msg));
			}
			if(obj1.Equals(obj2)){
				return;
			}
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, String.Format("[UnitTest] Assertion failed! {2}  {0}!={1}", obj1, obj2, msg));
		}

		/*
			リスト要素すべてが一致することを調べる
		*/
		public static void EqualList<T>(List<T> list1, List<T> list2)
		{
			for(int i = 0; i < list1.Count; i++)
			{
				Equal(list1[i], list2[i]);
			}
		}

		static public void NotEqual<T>(T obj1, T obj2, string msg ="")
		{
			if(!obj1.Equals(obj2)){
				return;
			}
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, String.Format("[UnitTest] Assertion failed! {2}  {0}=={1}", obj1, obj2, msg));
		}
		
		static public void True(bool isTrue)
		{
			if(isTrue){
				return;
			}
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, "[UnitTest] Assertion test failed!");
		}
		
		/*
		 * Assert Fail. This will throw exception without additional message.
		 */
		static public void Fail()
		{
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed);
		}
		
		/*
		 * Assert Fail. This will throw exception with additional message
		 */
		static public void Fail(string in_failmsg)
		{
			throw new UniUnitTest.AssertException(AssertErrorTypeEnum.Failed, "[UnitTest] Assertion failed! {0}",in_failmsg);
		}
	
	}

}
