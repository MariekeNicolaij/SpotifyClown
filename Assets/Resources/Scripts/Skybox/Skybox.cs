using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skybox : MonoBehaviour
{
    // Change background to a random dark color
    public static void ChangeBackgroundColor()
    {
        RenderSettings.skybox.SetColor("_Tint", new Color(Random.Range(0, 100 / 255f), Random.Range(0, 100 / 255f), Random.Range(0, 100 / 255f)));
    }
}
