using UnityEngine;
using EZCameraShake;

public class ShakeOnAwake : MonoBehaviour
{
    private void OnEnable()
    {
        CameraShaker.Instance.ShakeOnce(15f, 15f, 0f, 1.28f);
    }
}
