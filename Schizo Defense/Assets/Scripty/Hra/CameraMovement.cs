using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Rychlost pohybu kamery
    public float borderSize = 10000f; // Velikost borderu
    public float rotationSpeed = 3f; // Rychlost otáèení kamery pomocí myši

    private bool isRotating = false; // Promìnná urèující, zda se má kamera otáèet

    void Update()
    {
        // Pohyb kamery
        Vector3 movement = Vector3.zero;

        // Získání vstupu z klávesnice pro pohyb kamery
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        // Pohyb kamery nahoru a dolù
        movement.y = Input.GetAxis("Mouse ScrollWheel");

        // Normalizace pohybu, aby se pohyboval stejnou rychlostí ve všech smìrech
        movement.Normalize();

        // Aplikace rychlosti pohybu
        movement *= movementSpeed * Time.deltaTime;
        transform.Translate(movement, Space.Self);

        // Ovládání otáèení kamery pomocí myši
        if (Input.GetMouseButtonDown(1)) // Pokud je stisknuto pravé tlaèítko myši
        {
            isRotating = true; // Nastavíme, že se má kamera otáèet
            Cursor.lockState = CursorLockMode.Locked; // Zamkneme kurzor
            Cursor.visible = false; // Skryjeme kurzor
        }
        else if (Input.GetMouseButtonUp(1)) // Pokud je uvolnìno pravé tlaèítko myši
        {
            isRotating = false; // Nastavíme, že se kamera nemá otáèet
            Cursor.lockState = CursorLockMode.None; // Uvolníme kurzor
            Cursor.visible = true; // Zobrazíme kurzor
        }

        // Otáèení kamery pouze pøi pohybu myší
        if (isRotating)
        {
            float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
            float mouseY = Input.GetAxis("Mouse Y") * rotationSpeed;

            transform.Rotate(Vector3.up, mouseX, Space.World);
            transform.Rotate(Vector3.left, mouseY, Space.Self);
        }

        // Omezení pohybu kamery na urèený border mapy
        Vector3 currentPosition = transform.position;

        currentPosition.x = Mathf.Clamp(currentPosition.x, -borderSize, borderSize);
        currentPosition.z = Mathf.Clamp(currentPosition.z, -borderSize, borderSize);

        // Aktualizace pozice kamery s omezeným pohybem
        transform.position = currentPosition;
    }
}









