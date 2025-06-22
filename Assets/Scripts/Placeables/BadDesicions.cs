using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadDesicions : MonoBehaviour
{
    UnitInteractions interactions;
    UnitController controller;
    GridManager manager;

    bool listening;

    private void OnEnable() { UnitController.DestinationReached += DestinationReached; }
    private void OnDisable() { UnitController.DestinationReached -= DestinationReached; }

    private void Awake()
    {
        interactions = FindFirstObjectByType<UnitInteractions>();
        controller = FindFirstObjectByType<UnitController>();
        manager = FindFirstObjectByType<GridManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Unit"))
        {
            if (interactions.sanity <= interactions.MaxSanity / 2)
            {
                interactions.treacherous = true;
                interactions.TreacherousColours();

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
                    Tile tile = manager.GetTileFromCords(coords + dir);
                    if (tile != null && !tile.blocked)
                    {
                        walkable.Add(tile);
                    }
                }

                Tile t = GetRandomTile(walkable);
                Transform a = controller.activeUnit;
                print(t.cords);
                controller.SetDestination(t.cords, manager.GetCoordinatesFromPosition(a.position));
                listening = true;
            }
        }
    }
    void DestinationReached(UnitController controller)
    {
        if (listening)
        {
            listening = false;
            Transform a = controller.activeUnit;

            manager.BlockNode(manager.GetCoordinatesFromPosition(transform.position));
            controller.SetDestination(controller.FindCastle(true), manager.GetCoordinatesFromPosition(a.position));
            //StartCoroutine(Wait());
            manager.UnblockNode(manager.GetCoordinatesFromPosition(transform.position));

        }
    }

    Tile GetRandomTile(List<Tile> walkable)
    {
        while (true)
        {
            Tile t = walkable[Random.Range(0, walkable.Count)];
            if (manager.GetPositionFromCoordinates(t.cords) != controller.StartPosition)
            {
                return t;
            }
        }

    }
}
