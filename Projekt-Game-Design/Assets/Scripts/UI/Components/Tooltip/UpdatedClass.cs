using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An interface for classes that need to use the unity Update method,
/// but don' use Monobehaviour as their base class. 
/// The UpdateHelper will call the Update method specified in this interface 
/// once per update. 
/// </summary>
public interface UpdatedClass
{
		/// <summary>
		/// Is called once per update by the UpdateHelper. 
		/// </summary>
		void Update();
}
