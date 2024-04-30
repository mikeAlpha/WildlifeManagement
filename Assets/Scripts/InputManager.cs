using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private bool IsManualEnabled = false;
    private bool IsInventoryEnabled = false;
    private bool IsPlacingObject = false;

    public static InputManager instance;

    private Vector3 current_mousePos;
    public Vector3 getMousePositon { get { return current_mousePos; } }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Update()
    {
        var hX = Input.GetAxisRaw("Horizontal");
        var vY = Input.GetAxisRaw("Vertical");

        //if (testPathFinding)
        //{

        //    if (Input.GetMouseButtonDown(0))
        //    {
        //        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //        var clickPos = GridManager.instance.grid.WorldToCell(mousePos);

        //        Vector2Int endPos = new Vector2Int(clickPos.x, clickPos.y);
        //        var ai_pos = GridManager.instance.grid.WorldToCell(agentTest.transform.position);

        //        GridManager.instance.InitPathFinding(new Vector2Int(ai_pos.x, ai_pos.y), endPos, agentTest);

        //    }

        //    return;
        //}

        if(!IsPlacingObject)
            EventHandler.ExecuteEvent<float, float>("PlayerMovement", hX, vY);

        if (Input.GetKeyDown(KeyCode.B) && !IsPlacingObject && !IsInventoryEnabled)
        {
            IsManualEnabled = !IsManualEnabled;
            UIManager.instance.ToggleManualBookUI(IsManualEnabled);
        }

        if(Input.GetKeyDown(KeyCode.I) && !IsPlacingObject && !IsManualEnabled)
        {
            IsInventoryEnabled = !IsInventoryEnabled;
            UIManager.instance.ToggleInventoryUI(IsInventoryEnabled);
        }

        if (IsPlacingObject)
        {
            Vector3 mousePos = Input.mousePosition;
            Vector3 posToPlace = Camera.main.ScreenToWorldPoint(mousePos);
            posToPlace.z = 0;

            EventHandler.ExecuteEvent<Vector3>("SetCurrentPositionofObj", posToPlace);
            EventHandler.ExecuteEvent<Transform>("SetCameraTarget", null);

            if (Input.GetMouseButtonDown(1))
            {
                EventHandler.ExecuteEvent("DestroyCurrentObject");
                IsPlacingObject = false;
            }
            else if (Input.GetMouseButtonDown(0))
                IsPlacingObject = false;
        }

        if(Input.GetMouseButtonDown(0) && !IsPlacingObject && !IsManualEnabled)
        {
            current_mousePos = Input.mousePosition;
            EventHandler.ExecuteEvent("ExecuteItemAction");
        }
    }

    public void SetobjectPlacing()
    {
        IsPlacingObject = true;
    }
}
