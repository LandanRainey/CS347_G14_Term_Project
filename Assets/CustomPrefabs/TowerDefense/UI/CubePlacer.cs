using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public GameObject realCubePrefab; // The prefab for the real cube
    public GameObject ghostCube; // The ghost cube
    private bool isPlacing = false; // Whether we're currently placing a cube

void Update()
{
    if (isPlacing)
    {
        MoveGhostCubeToMouse();
        if (Input.GetMouseButtonDown(0))
        {
            PlaceRealCube();
        }
    }
}

    public void StartPlacingCube()
    {
        isPlacing = true;
        ghostCube = Instantiate(ghostCube);
        //ghostCube.GetComponent<Renderer>().material.color = new Color(1, 1, 1, 0.5f); // Make the ghost cube semi-transparent
    }

    private void MoveGhostCubeToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += ghostCube.transform.localScale.y / 2;
            ghostCube.transform.position = newPosition;
        }
    }

    private void PlaceRealCube()
    {
        isPlacing = false;
        Vector3 newPosition = ghostCube.transform.position;
    
        // GameObject tempCube = Instantiate(realCubePrefab, newPosition, Quaternion.identity);
        // float cubeHeight = tempCube.transform.GetChild(0).GetComponent<Collider>().bounds.size.y;
        // Destroy(tempCube);
    
        newPosition.y += 4.58f;
        Instantiate(realCubePrefab, newPosition, Quaternion.identity);
        Destroy(ghostCube);
    }
}