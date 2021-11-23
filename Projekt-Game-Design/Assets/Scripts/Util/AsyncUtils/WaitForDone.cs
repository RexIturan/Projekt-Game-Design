using System;
using UnityEngine;



namespace Util.AsyncUtils {
	/// <summary>
	///
	/// Usage:
	/// yield return new WaitForDone(timeout, ()=> condition);
	/// 
	/// https://forum.unity.com/threads/coroutine-question-can-you-combine-waitforseconds-with-a-condition.107041/
	/// </summary>
	public sealed class WaitForDone : CustomYieldInstruction
	{
		private readonly Func<bool> m_Predicate;
		private float m_timeout;
		
		private bool WaitForDoneProcess()
		{
			m_timeout -= Time.deltaTime;
			return m_timeout <= 0f || m_Predicate();
		}
 
		public override bool keepWaiting => !WaitForDoneProcess();
 
		public WaitForDone(float timeout, Func<bool> predicate)
		{
			m_Predicate = predicate;
			m_timeout = timeout;
		}
	}
}