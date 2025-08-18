using UnityEngine;
using DG.Tweening;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private Vector3 strength = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField] private int vibration = 10;
    [SerializeField] private float randomness = 90f;
    private Tween currentShake;

    private Vector3 initialPosition;

    private void Awake()
    {
        // Store the camera’s starting position
        initialPosition = transform.localPosition;
    }

    public void ShakeCamera(float multiplier)
    {
        // Stop previous shake if it's still running
        if (currentShake != null && currentShake.IsActive())
        {
            currentShake.Kill();
            transform.localPosition = initialPosition;
        }

        currentShake = transform.DOShakePosition(duration, strength * multiplier, vibration, randomness).SetUpdate(true).OnKill(() =>
            {
                transform.localPosition = initialPosition;
            });

        Handheld.Vibrate();
    }
}