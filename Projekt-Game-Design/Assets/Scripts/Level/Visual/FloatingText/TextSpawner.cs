using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Events.ScriptableObjects;
using TMPro;

public class TextSpawner : MonoBehaviour
{
    [SerializeField] private GameObject textPrefab;
    [SerializeField] private CreateFloatingTextEventChannelSO createTextEC;

    // Start is called before the first frame update
    void Start()
    {
        createTextEC.OnEventRaised += SpawnTextMessage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnTextMessage(string text, Vector3 position, Color color)
    {
        GameObject newText = Instantiate(textPrefab, position, Quaternion.identity);

        TextMeshPro textMeshComponent = newText.GetComponentInChildren<TextMeshPro>();

        textMeshComponent.color = color;
        textMeshComponent.text = text;
    }
}
