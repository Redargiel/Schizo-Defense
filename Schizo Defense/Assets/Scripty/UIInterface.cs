using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public GameObject gatlingTower;

    GameObject focusObs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            // Check if there is already a tower on the platform
            if (hit.collider.gameObject.CompareTag("platform") && hit.collider.gameObject.transform.childCount == 0)
            {
                Vector3 center = hit.collider.bounds.center;
                center.y = gatlingTower.transform.position.y; // Align with tower's Y position
                focusObs = Instantiate(gatlingTower, center, gatlingTower.transform.rotation);
                focusObs.GetComponent<Collider>().enabled = false;
            }
        }
        else if (Input.GetMouseButton(0) && focusObs != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                focusObs.transform.position = hit.collider.bounds.center;
            }
        }
        else if (Input.GetMouseButtonUp(0) && focusObs != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("platform"))
                {
                    focusObs.transform.position = hit.collider.bounds.center;
                    hit.collider.gameObject.tag = "occupied"; // Assign "occupied" tag to the block
                }
                else
                {
                    Destroy(focusObs);
                }
            }
            focusObs = null;
        }
    }
}




