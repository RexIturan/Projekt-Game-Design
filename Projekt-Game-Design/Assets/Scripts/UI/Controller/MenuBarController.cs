using Input;
using UnityEngine;
using UnityEngine.UIElements;

public class MenuBarController : MonoBehaviour {

	[SerializeField] private InputReader inputReader;

///// Private Variables ////////////////////////////////////////////////////////////////////////////	
	
	private Button _menuButton;
	private Button _inventoryButton;

///// Private Functions ////////////////////////////////////////////////////////////////////////////

	private void BindElements() {
		// Holen des UXML Trees, zum getten der einzelnen Komponenten
		var root = GetComponent<UIDocument>().rootVisualElement;
		_menuButton = root.Q<Button>("IngameMenuButton");
		_inventoryButton = root.Q<Button>("InventoryButton");

		_menuButton.clicked += inputReader.SimulateOnMenu;
		_inventoryButton.clicked += inputReader.SimulateOnInventory;
		}

	private void UnbindElements() {
		_menuButton.clicked -= inputReader.SimulateOnMenu;
		_inventoryButton.clicked -= inputReader.SimulateOnInventory;
		
		_menuButton = null;
		_inventoryButton = null;
	}

///// Callbacks	////////////////////////////////////////////////////////////////////////////////////

///// Public Functions	////////////////////////////////////////////////////////////////////////////

///// Unity Functions	//////////////////////////////////////////////////////////////////////////////

	private void OnEnable() {
		BindElements();
	}

	private void OnDisable() {
		UnbindElements();
	}
}