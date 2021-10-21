using UnityEngine;
using Events.ScriptableObjects;
using TMPro;

public class TextSpawner : MonoBehaviour {
	[SerializeField] private GameObject textPrefab;
	[SerializeField] private CreateFloatingTextEventChannelSO createTextEC;

	// Start is called before the first frame update
	private void Start() {
		createTextEC.OnEventRaised += SpawnTextMessage;
	}

	private void SpawnTextMessage(string text, Vector3 position, Color color) {
		GameObject newText = Instantiate(textPrefab, position, Quaternion.identity);

		TextMeshPro textMeshComponent = newText.GetComponentInChildren<TextMeshPro>();

		textMeshComponent.color = color;
		textMeshComponent.text = text;
	}
}