using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class UnitController : MonoBehaviour
{
    [SerializeField] float movementSpeed = 1f;
    public float Speed {  get { return movementSpeed; } set { movementSpeed = value; } }

    [HideInInspector] public Transform activeUnit; // Holds the active unit
    [SerializeField] GameObject Unit; // holds the unit gameObject

    List<Node> path = new List<Node>();

    GridManager gridManager;
    Pathfinding pathFinder;

    Vector3 startPosition;
    Vector3 currentPosition;
    public Vector3 StartPosition { get { return startPosition; } }

    public static event Action<UnitController> DestinationReached;

    [HideInInspector] public UnitInteractions interaction;

    void Start()
    {
        Debug.Log("Started");
        activeUnit = GameObject.FindGameObjectWithTag("Unit").transform;
        interaction = GetComponent<UnitInteractions>();

        gridManager = FindObjectOfType<GridManager>();
        pathFinder = FindObjectOfType<Pathfinding>();
        StartCoroutine(Wait());   
        //Debug.Log(FindCastle(false) + " " + FindCastle(true));
    }

    private void Update()
    {
        RotateTroops();
    }

    IEnumerator Wait()
    {
        yield return new WaitForEndOfFrame();
        SetDestination(FindCastle(false), FindCastle(true));
    }

    public void SetDestination(Vector2Int targetCords, Vector2Int startCords)
    {
         //new Vector2Int((int)activeUnit.transform.position.x, (int)activeUnit.transform.position.z) / gridManager.UnityGridSize;
        pathFinder.SetNewDestination(startCords, targetCords);
        RecalculatePath(true);
    }

    void RecalculatePath(bool resetPath)
    {
        Vector2Int coordinates = new Vector2Int();
        if (resetPath)
        {
            coordinates = pathFinder.StartCords;
        }
        else
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);
        }
        StopAllCoroutines();
        path.Clear();
        path = pathFinder.GetNewPath(coordinates);
        StartCoroutine(FollowPath());
    }

    IEnumerator FollowPath()
    {
        for (int i = 1; i < path.Count; i++)
        {
            startPosition = activeUnit.position;
            Vector3 endPosition = gridManager.GetPositionFromCoordinates(path[i].cords);
            float travelPercent = 0f;           

            while (travelPercent < 1f)
            {
                travelPercent += Time.deltaTime * movementSpeed;
                activeUnit.position = Vector3.Lerp(startPosition, endPosition, travelPercent);
                yield return new WaitForEndOfFrame();
            }

            if (i == path.Count - 1) { DestinationReached?.Invoke(this); }
        }
    }

    void RotateTroops()
    {
        float moveDirection = activeUnit.transform.position.x - currentPosition.x;

        if (moveDirection > 0)
        {
            activeUnit.GetChild(0).transform.localScale = new Vector3(1, 1, 1); // Facing right
        }
        else if (moveDirection < 0)
        {
            activeUnit.GetChild(0).transform.localScale = new Vector3(-1, 1, 1); // Facing left
        }

        currentPosition = activeUnit.transform.position;
    }

    public Vector2Int FindCastle(bool PlayerCastle)
    {
        foreach (Tile tile in FindObjectsOfType<Tile>())
        {
            Castle castle = tile.GetComponent<Castle>();
            if (castle != null && castle.PlayerCastle == PlayerCastle)
            {
                //Debug.Log("Castle Found" + tile.cords);
                return tile.cords;  
            }
        }

        //Debug.Log("Castle not found");
        return Vector2Int.zero;      
    }

    public void DestroyActiveUnit()
    {
        Destroy(activeUnit.gameObject);
        GameObject toInstantiate = Instantiate(Unit, new Vector3(FindCastle(true).x, 0f, FindCastle(true).y),Quaternion.identity);
        activeUnit = toInstantiate.transform;
        movementSpeed = 1.5f;
        interaction.UpdateUnit();
        SetDestination(FindCastle(false), FindCastle(true));
    }
    public void PlaySelect()
    {
        FindFirstObjectByType<AudioManager>().Play("Select");
    }
}
