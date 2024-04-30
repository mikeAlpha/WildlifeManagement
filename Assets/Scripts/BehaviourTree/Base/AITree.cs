using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using mikealpha;

public class AITree : BaseBT
{
    public Transform[] Waypoints;
    public float ViewAngle = 120f;
    public float ViewRadius = 20f;

    public LayerMask EnemyLayer, ObstacleLayer;

    public List<Transform> targets = new List<Transform>();

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
}
