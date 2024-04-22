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
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ghostCube.transform.position, Vector3.down, out hit))
            {
                if (hit.collider.gameObject.CompareTag("Path") || hit.collider.gameObject.CompareTag("Ally"))
                {
                    isPlacing = false;
                    ghostCube.GetComponent<Renderer>().material = cantPlaceMaterial;
                }
                else
                {
                    isPlacing = true;
                    ghostCube.GetComponent<Renderer>().material = placeMaterial;
                }
            }
        if (Physics.Raycast(ray, out hit))
        {
            Vector3 newPosition = hit.point;
            newPosition.y += ghostCube.transform.localScale.y / 2;
            ghostCube.transform.position = newPosition;
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