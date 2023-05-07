using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    private void Awake()
    {
        Cursor.visible = false;
    }

    void Update()
    {
        if(!PauseMenu.gameIsPaused) transform.position = Input.mousePosition;
    }
}
