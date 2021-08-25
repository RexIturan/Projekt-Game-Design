using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Ist die Geschwindigkeit in der sich die Kamera bewegt
    public float panSpeed = 5f;

    // Geschwindigkeit des Mausrads
    public float scrollSpeed = 20f;

    // Gibt an, wie weit man an den Rand gehen muss, bevor die Maus die Kamera bewegt
    public float panBorderThickness = 10f;

    // Bestimmen die maximale Breite des Spielfeldes
    public float xLimit, zLimit;

    // Bestimmt wie weit man heraus und reinzoomen kann
    public float yMaxZoom, yMinZoom;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;

        if (Input.GetKey("up") || Input.mousePosition.y >= Screen.height - panBorderThickness)
        {
            pos.z += panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("left") || Input.mousePosition.x <= panBorderThickness)
        {
            pos.x -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("down") || Input.mousePosition.y <= panBorderThickness)
        {
            pos.z -= panSpeed * Time.deltaTime;
        }

        if (Input.GetKey("right") || Input.mousePosition.x >= Screen.width - panBorderThickness)
        {
            pos.x += panSpeed * Time.deltaTime;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        pos.y -= scroll * scrollSpeed * 100f * Time.deltaTime;

        pos.x = Mathf.Clamp(pos.x, -xLimit, xLimit);
        pos.y = Mathf.Clamp(pos.y, yMinZoom, yMaxZoom);
        pos.z = Mathf.Clamp(pos.z, -zLimit, zLimit);

        transform.position = pos;
    }
}