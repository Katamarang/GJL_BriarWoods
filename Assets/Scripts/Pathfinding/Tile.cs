using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool blocked;
    public bool taken;

    public Vector2Int cords;

    GridManager gridManager;

    // Start is called before the first frame update
    void Start()
    {
        SetCords();
        //print(cords);
        gridManager.ConnectTile(cords, this);

        if (blocked)
        {
            gridManager.BlockNode(cords);
        }
    }

    private void SetCords()
    {
        gridManager = FindObjectOfType<GridManager>();
        int x = (int)transform.position.x;
        int z = (int)transform.position.z;

        cords = new Vector2Int(x / gridManager.UnityGridSize, z / gridManager.UnityGridSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Placeable"))
        {
            taken = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Placeable"))
        {
            taken = false;
        }
    }
}
