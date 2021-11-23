using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActivator : MonoBehaviour
{
	private void Awake() {
		//todo why cant i debug here? :(
		var eventsystem = GameObject.Find("UI EventSystem");

		var enableUI = !( eventsystem is null );

		GameObject[] gos = GameObject.FindObjectsOfType(typeof(GameObject)) as GameObject[];
		foreach ( GameObject go in gos ) {
			if ( go.layer == 5 ) {
				go.SetActive(enableUI);
				//do something
			}
		} 
	}
}
