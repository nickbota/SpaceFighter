using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private Vector3 strength = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private int vibration = 10;
    [SerializeField] private float randomness = 90f;
    private Tween currentShake;

    public void ShakeCamera()
    {
        // Stop previous shake if it's still running
        if (currentShake != null && currentShake.IsActive())
            currentShake.Kill();

        currentShake = transform.DOShakePosition(duration, strength, vibration, randomness);
    }
}