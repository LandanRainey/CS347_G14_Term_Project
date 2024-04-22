using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    public GameObject realCubePrefab; // The prefab for the real cube
    public GameObject ghostCubePrefab; // The prefab for the ghost cube
    public Material placeMaterial; // The cube is green
    public Material cantPlaceMaterial; // The cube is red
    private GameObject ghostCube; // The ghost cube
    private bool isPlacing = false; // Whether we're currently placing a cube

    public TowerDefense towerDefense;

    void Update()
    {       
        if(ghostCube != null)
        {
            MoveGhostCubeToMouse();
        }

        if (Input.GetMouseButtonDown(0))
        {
        if (isPlacing)
            {
                PlaceRealCube();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPlacing = false;
            Destroy(ghostCube);
            ghostCube = null;
        }    
    }

    public void StartPlacingCube()
    {
        //Debug.Log($"Current Money: {towerDefense.currentMoney}");
        if(towerDefense.currentMoney >= 100)
        {
            isPlacing = true;
            ghostCube = Instantiate(ghostCubePrefab);
        }
    }

    private void MoveGhostCubeToMouse()
    {
        // We need to make the ray be at the same angle as the camera, so that we can't place on other towers
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the ray hits something
            Vector3 newPosition = hit.point;
            newPosition.y += ghostCube.transform.localScale.y / 2;
            ghostCube.transform.position = newPosition;

            // If the ray doesn't intersect with ground, you can't place
            if (!hit.collider.gameObject.CompareTag("Ground"))
            {
                isPlacing = false;
                ghostCube.GetComponent<Renderer>().material = cantPlaceMaterial;
            }
            else // else, you can place
            {
                isPlacing = true;
                ghostCube.GetComponent<Renderer>().material = placeMaterial;
            }
        }
    }


    private void PlaceRealCube()
    {
        towerDefense.DecreaseMoney(100);
        isPlacing = false;
        Vector3 newPosition = ghostCube.transform.position;

        //Debug.Log("Placing cube at " + newPosition);
    
        // GameObject tempCube = Instantiate(realCubePrefab, newPosition, Quaternion.identity);
        // float cubeHeight = tempCube.transform.GetChild(0).GetComponent<Collider>().bounds.size.y;
        // Destroy(tempCube);
    
        //newPosition.x += 2.55f;
        //newPosition.y += 4.5f;
        //newPosition.z += 1.025f;
        Instantiate(realCubePrefab, newPosition, Quaternion.identity);
        Destroy(ghostCube);
        ghostCube = null;
    }
}