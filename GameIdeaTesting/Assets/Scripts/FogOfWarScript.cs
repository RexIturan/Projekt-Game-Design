using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWarScript : MonoBehaviour
{
    public GameObject fogOfWarPlane;

    public LayerMask fogLayerMask;

    public float radius = 5f;

    private float radiusSqrt => radius*radius;
    private Mesh fogMesh;
    private Vector3[] vertices;
    private Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        GameEvents.current.playerMoved += playerMoved;
    }

    void playerMoved(Transform transform)
    {
        Vector3 vector = transform.position;
        vector.y += 200;
        Ray r = new Ray(vector, transform.position - vector);
        RaycastHit hit;
        if (Physics.Raycast(r, out hit, 1000, fogLayerMask, QueryTriggerInteraction.Collide))
        {
            // Super super schlecht optimiert, aber es ist ja nur ein Proof of Concept....
            // TODO: Muss unbedingt verbessert werden...
            for (int i = 0; i < vertices.Length; i++)
            {
                float dist = Vector3.SqrMagnitude(vertices[i] - hit.point);
                if (dist < radiusSqrt)
                {
                    // Damit es leicht abgesmootht wird zu den Rändern hin
                    float alpha = Mathf.Min(colors[i].a, dist / radiusSqrt);
                    colors[i].a = alpha;
                }
            }
            UpdateColors();
        }
    }

    void Initialize()
    {
        // Mesh holen, sowie die Anzahl an Vertices
        fogMesh = fogOfWarPlane.GetComponent<MeshFilter>().mesh;
        vertices = fogMesh.vertices;
        colors = new Color[vertices.Length];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.black;
        }
        
        // Weltkoordinaten vor generieren, damit die Performance besser ist
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i] = fogOfWarPlane.transform.TransformPoint(vertices[i]);
        }
        
        
        UpdateColors();
        Debug.Log("Fog of War wurde initialisiert");
    }

    void UpdateColors()
    {
        fogMesh.colors = colors;
    }
}
