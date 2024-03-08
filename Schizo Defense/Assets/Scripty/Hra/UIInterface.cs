using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIInterface : MonoBehaviour
{
    public GameObject gatlingTower;

    GameObject focusObs;

    // Funkce pro pokládání vìže na urèené místo na herní ploše
    public void PlaceTower(GameObject towerPrefab)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!RaycastWithoutTriggers(ray, out hit))
        {
            return;
        }

        // Zkontrolujte, zda je na platformì již vìž
        if (hit.collider.gameObject.CompareTag("platform") && hit.collider.gameObject.transform.childCount == 0)
        {
            Vector3 center = hit.collider.bounds.center;
            center.y = towerPrefab.transform.position.y; // Zarovnání se Y pozicí vìže
            focusObs = Instantiate(towerPrefab, center, towerPrefab.transform.rotation);
            DisableColliders();
        }
    }

    // Update is called once per frame
    void Update()
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
                center.y = gatlingTower.transform.position.y; // Align with tower's Y position
                focusObs = Instantiate(gatlingTower, center, gatlingTower.transform.rotation);
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
                    hit.collider.gameObject.tag = "occupied"; // Assign "occupied" tag to the block
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
}





