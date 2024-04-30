using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAgent : MonoBehaviour
{
    Vector3 waypoint;
    int index = 0;

    bool IsMoving = false;
    List<Node> path;


    public void SetPath(List<Node> path)
    {
        this.path = path;
        IsMoving = true;
    }

    private void Stop()
    {
        IsMoving = false;
        index = 0;
    }

    public void Update()
    {
        if (IsMoving)
        {
            waypoint = GridManager.instance.grid.GetCellCenterWorld(new Vector3Int(path[index].position.x, path[index].position.y));
            transform.position = Vector3.MoveTowards(transform.position, waypoint, Time.deltaTime * 2f);
            if (waypoint == transform.position)
            {
                index++;

                if (index >= path.Count)
                    Stop();
            }
        }
    }
}
