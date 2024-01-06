using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class CursorReplacment : MonoBehaviour
{
    // Start is called before the first frame update

    public Texture2D _cursorArrow;
    [SerializeField] VirtualMouseInput _virtualMouseInput;
    bool useMouse;
    void Start()
    {
        //Cursor.visible = false;

        Cursor.SetCursor(_cursorArrow, Vector2.zero, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
   
}
