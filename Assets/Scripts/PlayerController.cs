using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{

    public enum direction
    {
        right,
        left,
        front,
        back
    }


    private Animator anim;
    private direction dir;

    public float speed = 2f;

    public static PlayerController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        EventHandler.RegisterEvent<float, float>("PlayerMovement", Move);
        EventHandler.RegisterEvent<EquippableItem>("SetEquippableItem", SetEquippableItem);
        EventHandler.RegisterEvent("ExecuteItemAction", ExecuteItemAction);
    }

    private void OnDisable()
    {
        EventHandler.UnregisterEvent<float, float>("PlayerMovement", Move);
        EventHandler.UnregisterEvent<EquippableItem>("SetEquippableItem", SetEquippableItem);
        EventHandler.UnregisterEvent("ExecuteItemAction", ExecuteItemAction);
    }

    private void Start()
    {
        EventHandler.ExecuteEvent<Transform>("SetCameraTarget",transform);
    }


    private void Move(float hX , float vY) 
    {
        var move_dir = new Vector3(hX, vY, 0);

        if (hX < 0 && vY == 0)
            dir = direction.left;
        else if (hX > 0 && vY == 0)
            dir = direction.right;

        if (vY < 0 && hX == 0)
            dir = direction.front;
        else if (vY > 0 && hX == 0)
            dir = direction.back;

        anim.SetFloat("hX", hX);
        anim.SetFloat("vY", vY);


        if (hX == 0f && vY == 0f)
        {
            switch (dir)
            {
                case direction.right:
                    anim.SetFloat("dirX", 1f);
                    break;

                case direction.left:
                    anim.SetFloat("dirX", -1f);
                    break;
                case direction.back:
                    anim.SetFloat("dirY", 1f);
                    break;
                case direction.front:
                    anim.SetFloat("dirY", -1f);
                    break;
            }
        }
        else
        {
            anim.SetFloat("dirX", 0f);
            anim.SetFloat("dirY", 0f);
        }

        var target_pos = transform.position + move_dir;
        transform.position = Vector3.MoveTowards(transform.position, target_pos, speed * Time.deltaTime);

        if (GridManager.instance != null)
            GridManager.instance.SetPlayerPosition(transform.position);
    }
}
