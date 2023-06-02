using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowCanvas : MonoBehaviour
{
    void Update()
    {
        // Get screen width
        int screenWidth = Screen.width;

        Debug.Log("Screen width: " + screenWidth);

        // If the screen width is less than 600, then the game is running on a mobile device
        if (screenWidth < 600)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
