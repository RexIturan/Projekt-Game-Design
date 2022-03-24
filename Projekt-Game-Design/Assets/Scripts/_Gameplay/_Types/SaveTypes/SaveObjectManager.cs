using System;
using System.Collections.Generic;
using SaveSystem.V2.Data;
using UnityEngine;
using Util.Extensions;

namespace GDP01.Gameplay.SaveTypes {
	public class SaveObjectManager : MonoBehaviour {
		protected List<D> SaveComponents<C, D>(List<C> components, List<D> componetDataList)
			where D : SaveObjectCreatorData
			where C : ISaveState<D> {
			
			var dataList = componetDataList;

			HashSet<int> idsUsed = new HashSet<int>();

			foreach ( var component in components ) {
				D data = component.Save();

				int existingIdx = dataList.FindIndex(d => d.Id == data.Id);

				//id was already saved
				if ( idsUsed.Contains(existingIdx) ) {
					idsUsed.Add(dataList.Count);
					data.Id = dataList.Count;
					dataList.Add(data);
				}
				else {
					//id wasnt saved but there is a data block that has to be overridden
					if ( existingIdx != -1 ) {
						dataList[existingIdx] = data;
						idsUsed.Add(existingIdx);
					}
					// id wasnt saved and there is not data available
					else {
						idsUsed.Add(dataList.Count);
						data.Id = dataList.Count;
						dataList.Add(data);
					}
				}
			}
			return dataList;
		}

		protected void LoadComponents<C, D>(
			ref List<C> components, 
			ref List<D> currentDataList, 
			List<D> newDataList, 
			GameObject defaultPrefab, 
			Transform parent, 
			Func<D, Transform, C> createComponent = null)
			where D : SaveObjectCreatorData
			where C : SaveObjectFactory<C, D>, ISaveState<D> {
			
			//clear and remove all _doors
			components.ClearMonoBehaviourGameObjectReferences();
			components = new List<C>();

			//cache door data
			currentDataList = newDataList ?? new List<D>();

			foreach ( var data in currentDataList ) {

				if ( data is { } ) {
					data.Prefab = data.Prefab ? data.Prefab : defaultPrefab;
					C component;
					
					if ( createComponent is { } ) {
						component = createComponent.Invoke(data, parent);
					}
					else {
						component = CreateComponent<C, D>(data, parent);
					}
					
					components.Add(component);
				}
			}
		}

		protected C CreateComponent<C, D>(D data, Transform parent)
			where C : SaveObjectFactory<C, D>, ISaveState<D> 
			where D : SaveObjectCreatorData {
			
			C component = SaveObjectFactory<C, D>.CreateAndLoad(data);

			component.transform.SetParent(parent != null ? parent : transform);
			
			//todo check id
			
			return component;
		}
	}
}