using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizing : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Screen.height < Screen.width)
            Camera.main.orthographicSize = 15f;
        else if (Screen.height >= Screen.width && (Screen.height / (Screen.width / 9f)) / 16f == 1f)
            Camera.main.orthographicSize = 30f;
        else if (Screen.height >= Screen.width && (Screen.height / (Screen.width / 9f)) / 16f != 1f) {
            Camera.main.orthographicSize = 35f;
        }
    }
}
