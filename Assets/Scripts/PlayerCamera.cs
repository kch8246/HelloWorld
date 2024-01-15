using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Texture2D texAimCursor = null;


    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnGUI()
    {
        Vector3 mousePos = Input.mousePosition;
        GUI.DrawTexture(new Rect(mousePos.x - 32, Screen.height - mousePos.y - 32, 64, 64), texAimCursor);
    }
}
