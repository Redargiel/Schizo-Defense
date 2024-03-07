using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Rychlost pohybu kamery
    public float borderSize = 10000f; // Velikost borderu
    public float rotationSpeed = 3f; // Rychlost ot��en� kamery pomoc� my�i

    private bool isRotating = false; // Prom�nn� ur�uj�c�, zda se m� kamera ot��et

    void Update()
    {
        // Pohyb kamery
        Vector3 movement = Vector3.zero;

        // Z�sk�n� vstupu z kl�vesnice pro pohyb kamery
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        // Pohyb kamery nahoru a dol�
        movement.y = Input.GetAxis("Mouse ScrollWheel");

        // Normalizace pohybu, aby se pohyboval stejnou rychlost� ve v�ech sm�rech
        movement.Normalize();

        // Aplikace rychlosti pohybu
        movement *= movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Ovl�d�n� ot��en� kamery pomoc� my�i
        if (Input.GetMouseButtonDown(1)) // Pokud je stisknuto prav� tla��tko my�i
        {
            isRotating = true; // Nastav�me, �e se m� kamera ot��et
            Cursor.lockState = CursorLockMode.Locked; // Zamkneme kurzor
            Cursor.visible = false; // Skryjeme kurzor
        }
        else if (Input.GetMouseButtonUp(1)) // Pokud je uvoln�no prav� tla��tko my�i
        {
            isRotating = false; // Nastav�me, �e se kamera nem� ot��et
            Cursor.lockState = CursorLockMode.None; // Uvoln�me kurzor
            Cursor.visible = true; // Zobraz�me kurzor
        }

        // Ot��en� kamery pouze p�i pohybu my��
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, mouseX, Space.World);
            transform.Rotate(Vector3.left, mouseY, Space.Self);
        }

        // Omezen� pohybu kamery na ur�en� border mapy
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, -borderSize, borderSize);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -borderSize, borderSize);

        // Aktualizace pozice kamery s omezen�m pohybem
        transform.position = currentPosition;
    }
}









