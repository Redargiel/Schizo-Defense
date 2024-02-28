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
            focusObs = Instantiate(gatlingTower, hit.point, gatlingTower.transform.rotation);
            focusObs.GetComponent<Collider>().enabled = false;
        }
        else if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }
            focusObs.transform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            if (hit.collider.gameObject.CompareTag("platform") && hit.normal.Equals(new Vector3(0,1,0)))
            {
                hit.collider.gameObject.tag = "occupied";
                focusObs.transform.position = new Vector3(hit.collider.gameObject.transform.position.x, focusObs.transform.position.y, hit.collider.gameObject.transform.position.z);
            }
            else
            {
                Destroy(focusObs);
            }
            focusObs = null;
        }
    }
}
