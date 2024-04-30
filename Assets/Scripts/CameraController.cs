using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    
    private Transform target;
    Vector3 offset;

    private void OnEnable()
    {
        EventHandler.RegisterEvent<Transform>("SetCameraTarget", SetTarget); 
    }

    private void OnDisable()
    {
        EventHandler.UnregisterEvent<Transform>("SetCameraTarget", SetTarget);
    }

    private void Update()
    {
        if(target != null)
        {
            var dest = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, dest, speed * Time.deltaTime);
        }
    }

    void SetTarget(Transform target)
    {
        if (target == null)
            return;

        this.target = target;
        offset = transform.position - this.target.position;
    }
}
