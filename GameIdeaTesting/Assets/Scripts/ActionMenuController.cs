using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameEvents.current.onPlayerClicked += showMenu;
        GameEvents.current.onGridClicked += hideMenu;
        // Muss zuerst aktiv sein, damit der Trigger auch zugewiesen werden kann
        // Danach muss er wieder deaktiviert werden
        gameObject.SetActive(false);
    }

    private void showMenu()
    {
        gameObject.transform.position = Input.mousePosition;
        gameObject.SetActive(true);
        Debug.Log("Durch das EventSystem wird das ActionMenu nun angezeigt");
        //Debug.Log(playerData.getNameID() + "wurde angeklickt. Durch das EventSystem");
        
    }
    
    private void hideMenu()
    {
        gameObject.SetActive(false);
        Debug.Log("Durch das EventSystem wird das ActionMenu nun versteckt");
        //Debug.Log(playerData.getNameID() + "wurde angeklickt. Durch das EventSystem");
        
    }
}
