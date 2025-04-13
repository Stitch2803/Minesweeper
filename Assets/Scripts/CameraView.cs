using UnityEngine;

public class CameraView : MonoBehaviour
{
    void Start()
    {
        Camera.main.orthographicSize = 5f * (16f / 9f) / (Screen.width / (float)Screen.height);
    }
}
