using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TacticsMove : MonoBehaviour
{
    public bool turn = false;
    List<Tile> selectableTiles = new List<Tile>();
    public GameObject[] tiles;

    Stack<Tile> path = new Stack<Tile>();
    Tile currentTile;

    public int move = 5;
    public float jumpHeight = 2;
    public float moveSpeed = 2;

    public bool moving = false;

    Vector3 velocity = new Vector3();
    Vector3 heading = new Vector3();

    public Tile actualTargetTile;

    float halfHeight = 0;

    protected void Init()
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        halfHeight = GetComponent<Collider>().bounds.extents.y;

        TurnManager.AddUnit(this);
    }

    public void GetCurrentTile()
    {
        currentTile = GetTargetTile(gameObject);
        currentTile.current = true;
    }
    public Tile GetTargetTile(GameObject target)
    {
        RaycastHit hit;
        Tile tile = null;
        if (Physics.Raycast(target.transform.position, -Vector3.up, out hit, 100))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);
            tile = hit.collider.GetComponent<Tile>();
             if(tile == null)
            Debug.Log("Tile component not found on: " + hit.collider.gameObject.name);
        }
        return tile;
    }

    public void ComputeAdjacencyLists(float jumpHeight, Tile target)
    {
        tiles = GameObject.FindGameObjectsWithTag("Tile");

        foreach (GameObject tile in tiles)
        {
            Tile t = tile.GetComponent<Tile>();
            t.FindNeighbors(100, target);
        }
    }

    public void FindSelectableTiles()
    {
        ComputeAdjacencyLists(jumpHeight, null);
        GetCurrentTile();

        Queue<Tile> process = new Queue<Tile>();

        process.Enqueue(currentTile);
        currentTile.visited = true;
        //currentTile.parent = ?? leave as null
        while (process.Count > 0)
        {
            Tile t = process.Dequeue();

            selectableTiles.Add(t);
            t.selectable = true;

            if (t.distance < move)
            {
                foreach (Tile tile in t.adjacencyList)
                {
                    if (!tile.visited)
                    {
                        tile.parent = t;
                        tile.visited = true;
                        tile.distance = 1 + t.distance;
                        process.Enqueue(tile);
                    }
                }
            }
        }
    }

    public void MoveToTile (Tile tile)
    {
        path.Clear();
        tile.target = true;
        moving = true;

        Tile next = tile;
        while (next != null)
        {
            path.Push(next);
            next = next.parent;
        }
    }
    protected void Move()
    {
        if (path.Count > 0)
        {
            Tile t = path.Peek();
            Vector3 target = t.transform.position;

            //Calculate the unit's position on top of the target tile

            target.y += halfHeight + t.GetComponent<Collider>().bounds.extents.y;

            if (Vector3.Distance(transform.position, target) >= 0.05f)

            {
                CalculateHeading(target);
                SetHorizontalVelocity();

                transform.forward = heading;
                transform.position += velocity * Time.deltaTime;
                
            }
            else
            {
                //Tile center reached
                transform.position = target;
                path.Pop();
            }


        }
        else
        {
             RemoveSelectableTiles();
             moving = false;

             TurnManager.EndTurn();
        }
    }

    protected void RemoveSelectableTiles()
    {

        if (currentTile != null)
        {
            currentTile.current = false;
            currentTile = null;
        }
        foreach (Tile tile in selectableTiles)
        {
            tile.Reset();
        }

        selectableTiles.Clear();
    }

    void CalculateHeading(Vector3 target)
    {
        heading = target - transform.position;
        heading.Normalize();
    }
    void SetHorizontalVelocity()
    {
        velocity = heading * moveSpeed;
    }

    protected Tile FindLowestF(List<Tile> list)
    {
        Tile lowest = list [0];

        foreach (Tile t in list)
        {
            if (t.f < lowest.f)
            {
                lowest = t;
            }
        }
        list.Remove(lowest);

        return lowest;
    }

    protected Tile FindEndTile(Tile t)
    {
        Stack<Tile> tempPath = new Stack<Tile>();

        Tile next = t.parent;

        while (next != null)
        {
            tempPath.Push(next);
            next = next.parent;
        }

        if (tempPath.Count <= move)
        {
            return t.parent;
        }

        Tile endTile = null;
        for (int i = 0; i <= move; i++)
        {
            endTile = tempPath.Pop();
        }

        return endTile;
    }

    protected void FindPath(Tile target)
    {
        ComputeAdjacencyLists(jumpHeight, target);
        GetCurrentTile();
        List<Tile> openList = new List<Tile>();
        List<Tile> closedList = new List<Tile>();

        openList.Add(currentTile);
        //currentTile.parent = ??
        currentTile.h = Vector3.Distance(currentTile.transform.position, target.transform.position);

        
        
        while (openList.Count > 0)
        {
            Tile t = FindLowestF(openList);

            Debug.Log("t is " + t);

            closedList.Add(t);

            if (t == target)
            {
                Debug.Log("target is" + target);
                actualTargetTile = FindEndTile(t);
                MoveToTile(actualTargetTile);
                return;
            }

            foreach (Tile tile in t.adjacencyList)
            {
                if (closedList.Contains(tile))
                {
                    //Do nothing 
                }
                else if (openList.Contains(tile))
                {
                    float tempG = t.g + Vector3.Distance(tile.transform.position, t.transform.position);

                    if (tempG < tile.g)
                    {
                        tile.parent = t;

                        tile.g = tempG;
                        tile.f = tile.g + tile.h;
                    }
                }
                else
                {
                    tile.parent = t;

                    tile.g = t.g + Vector3.Distance(tile.transform.position, t.transform.position);
                    tile.h = Vector3.Distance(tile.transform.position, target.transform.position);
                    tile.f = tile.g + tile.h;

                    openList.Add(tile);
                }
            }


        }

        
    }

    public void BeginTurn()
    {
        ActiveCharacterSwitcher.Instance.UpdateCameraFollow(transform);
        turn = true;
    }

    public void EndTurn()
    {
        turn = false;
    }
}
