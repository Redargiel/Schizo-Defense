using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public GameObject gatlingTower;
    public GameObject missileTower;
    public GameObject laserTower;

    private GameObject selectedTower;

    GameObject focusObs;

    void Start()
    {
        // Nastavíme výchozí typ vìže
        selectedTower = gatlingTower;
    }

    void Update()
    {
        PlaceTower();
    }

    void PlaceTower()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!RaycastWithoutTriggers(ray, out hit))
            {
                return;
            }

            // Check if there is already a tower on the platform
            if (hit.collider.gameObject.CompareTag("platform") && hit.collider.gameObject.transform.childCount == 0)
            {
                Vector3 center = hit.collider.bounds.center;
                center.y = selectedTower.transform.position.y;
                focusObs = Instantiate(selectedTower, center, selectedTower.transform.rotation);
                DisableColliders();
            }
        }
        else if (Input.GetMouseButton(0) && focusObs != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (RaycastWithoutTriggers(ray, out hit))
            {
                focusObs.transform.position = hit.collider.bounds.center;
            }
        }
        else if (Input.GetMouseButtonUp(0) && focusObs != null)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (RaycastWithoutTriggers(ray, out hit))
            {
                if (hit.collider.gameObject.CompareTag("platform"))
                {
                    focusObs.transform.position = hit.collider.bounds.center;
                    hit.collider.gameObject.tag = "occupied";
                    EnableColliders();
                }
                else
                {
                    Destroy(focusObs);
                }
            }
            focusObs = null;
        }
    }

    private void DisableColliders()
    {
        SetCollidersEnabled(false);
    }

    private void EnableColliders()
    {
        SetCollidersEnabled(true);
    }

    private void SetCollidersEnabled(bool enabled)
    {
        if (focusObs != null)
        {
            Collider[] childColliders = focusObs.GetComponentsInChildren<Collider>(true);
            Collider[] mainColliders = focusObs.GetComponents<Collider>();

            foreach (Collider collider in childColliders)
            {
                collider.enabled = enabled;
            }

            foreach (Collider collider in mainColliders)
            {
                collider.enabled = enabled;
            }
        }
    }

    private bool RaycastWithoutTriggers(Ray ray, out RaycastHit hit)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray);

        Array.Sort(hits, (x, y) => x.distance.CompareTo(y.distance));

        foreach (RaycastHit raycastHit in hits)
        {
            if (!raycastHit.collider.isTrigger)
            {
                hit = raycastHit;
                return true;
            }
        }

        hit = new RaycastHit();
        return false;
    }

    public void SelectGatlingTower()
    {
        selectedTower = gatlingTower;
    }

    public void SelectMissileTower()
    {
        selectedTower = missileTower;
    }

    public void SelectLaserTower()
    {
        selectedTower = laserTower;
    }
}







