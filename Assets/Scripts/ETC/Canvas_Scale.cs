using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Canvas_Scale : MonoBehaviour
{
    private CanvasScaler cs_ = null;

    private void Awake()
    {
        SetCanvasScale();
    }

    void SetCanvasScale()
    {
        //높이고정
        float rw = Screen.width / 1920.0f;
        float rh = Screen.height / 1080.0f;
        float rate = rh;
        
        if (cs_ == null)
            cs_ = GetComponent<CanvasScaler>();
        cs_.scaleFactor = rate;
    }
}
