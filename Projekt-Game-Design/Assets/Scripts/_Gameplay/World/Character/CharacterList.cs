using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

/// <summary>
/// Class <c>CharacterList</c>
/// Is intended as a Component of a GameObject in a Location Scene 
/// stores two lists of GameObjects, players and enemies
/// </summary>
public class CharacterList : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerContainer;
		[SerializeField] public List<GameObject> friendlyContainer;
    [SerializeField] public List<GameObject> enemyContainer;
		[SerializeField] public List<GameObject> deadEnemies;

    // public static CharacterList FindInstant() {
	   //  return FindObjectOfType<CharacterList>();
    // }

    // public int GetEnemyCountByType(EnemyTypeSO enemyType) {
	   //  var count = 0;
	   //  
	   //  count = deadEnemies.Count;
    //
	   //  if ( enemyType != null ) {
		  //   foreach ( var enemy in deadEnemies ) {
			 //    if ( enemy != null ) {
				//     var component = enemy.GetComponent<EnemyCharacterSC>();
				// 		
				//     if ( component.Type == enemyType ) {
				// 	    count++;
				//     }  
			 //    }
		  //   }  
	   //  }
    //
	   //  return count;
    // }
}
