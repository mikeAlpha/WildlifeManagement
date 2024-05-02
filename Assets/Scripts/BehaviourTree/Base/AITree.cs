using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mikealpha;
using System;

public class AITree : BaseBT
{
    public Transform[] Waypoints;
    public float ViewAngle = 120f;
    public float ViewRadius = 20f;

    public LayerMask EnemyLayer, ObstacleLayer;

    public List<Transform> targets = new List<Transform>();

    int index = 0;

    bool IsMoving = false;
    List<Node> path;
    Vector3 nodepoint;
    GridManager gridManager;

    protected override NodeBT CreateNode()
    {
        NodeBT root = new Fallback(new List<NodeBT>() {
            new Sequence(new List<NodeBT>
            {
                new EnemyWithinRange(transform, this)
            }),
            new Patrol(transform,Waypoints)
            });

        return root;
    }

    public void SetGrid(GridManager gridManager)
    {
        this.gridManager = gridManager;
    }


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

    public override void Update()
    {
        base.Update();

        if (IsMoving)
        {
            nodepoint = gridManager.grid.GetCellCenterWorld(new Vector3Int(path[index].position.x, path[index].position.y));
            transform.position = Vector3.MoveTowards(transform.position, nodepoint, Time.deltaTime * 2f);
            if (nodepoint == transform.position)
            {
                index++;

                if (index >= path.Count)
                    Stop();
            }
        }

    }
}
