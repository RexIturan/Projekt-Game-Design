using UnityEngine;
using Events.ScriptableObjects;
using TMPro;

public class TextSpawner : MonoBehaviour {
	[SerializeField] private GameObject textPrefab;
	[SerializeField] private CreateFloatingTextEventChannelSO createTextEC;
	[SerializeField] private Color spawnColor;
	[SerializeField] private string spawnText;

	public Color SpawnColor => spawnColor;
	public string SpawnText => spawnText;
	
	// Start is called before the first frame update
	private void Start() {
		createTextEC.OnEventRaised += SpawnTextMessage;
	}

	public void SpawnTextMessage(string text, Vector3 position, Color color) {
		//todo set parent
		GameObject newText = Instantiate(textPrefab, position, Quaternion.identity);

		TextMeshPro textMeshComponent = newText.GetComponentInChildren<TextMeshPro>();

		textMeshComponent.color = color;
		textMeshComponent.text = text;

		// Destroy floating text after 5 seconds
		Destroy(newText, 5);
	}
}