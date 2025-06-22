using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedForest : MonoBehaviour
{
    public bool PlayerEntered;
    bool listening; // listens for destination reached

    [SerializeField] float damage;

    UnitController controller;
    GridManager gridManager;

    private void Awake()
    {
        controller = FindFirstObjectByType<UnitController>();
        gridManager = FindFirstObjectByType<GridManager>();
    }

    private void OnEnable() { UnitController.DestinationReached += DestinationReached; }
    private void OnDisable() { UnitController.DestinationReached -= DestinationReached; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            PlayerEntered = true;
            Vector2Int coords = transform.parent.GetComponent<Tile>().cords;
            List<Tile> walkable = new List<Tile>();
            Vector2Int[] directions = new Vector2Int[]
            {
                new Vector2Int(1, 0),
                new Vector2Int(-1, 0),
                new Vector2Int(0, 1),
                new Vector2Int(0, -1)
            };

            foreach (var dir in directions)
            {
                Tile tile = gridManager.GetTileFromCords(coords + dir);
                if (tile != null && !tile.blocked)
                {
                    walkable.Add(tile);
                }
            }
            
            Tile t = GetRandomTile(walkable);
            Transform a = controller.activeUnit;
            print(t.cords);
            controller.SetDestination(t.cords, gridManager.GetCoordinatesFromPosition(a.position));
            controller.interaction.TakeDamge(damage);
            listening = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            PlayerEntered = false;
        }
    }

    void DestinationReached(UnitController controller)
    {
        if (listening)
        {
            listening = false;
            Transform a = controller.activeUnit;

            gridManager.BlockNode(gridManager.GetCoordinatesFromPosition(transform.position));
            controller.SetDestination(controller.FindCastle(controller.interaction.treacherous), gridManager.GetCoordinatesFromPosition(a.position));

            StartCoroutine(Wait());
            gridManager.UnblockNode(gridManager.GetCoordinatesFromPosition(transform.position));

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
    }

    Tile GetRandomTile(List<Tile> walkable)
    {
        while (true)
        {
            Tile t = walkable[Random.Range(0, walkable.Count)];
            if (gridManager.GetPositionFromCoordinates(t.cords) != controller.StartPosition)
            {  
                return t;
            }
        }

    }
}
