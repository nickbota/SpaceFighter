using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Terresquall;

public class VirtualJoystickFade : MonoBehaviour
{
    public VirtualJoystick joystick;
    [Range(0.1f, 100)] public float activeTime = 2f;
    public float fadeTime = 0.5f;
    public float fadeAlpha = 0.2f;

    private Image joystickImage;
    private Image controlStickImage;
    private Coroutine fadeCoroutine;
    private bool joystickActive = false;

    void Start()
    {
        joystick = GetComponent<VirtualJoystick>();
        joystickImage = GetComponent<Image>();
        controlStickImage = joystick.controlStick.GetComponent<Image>();
    }

    void Update()
    {
        // Joystick pressed (pointer down)
        if (Input.GetMouseButtonDown(0) && joystick.currentPointerId == -2)
        {
            joystickActive = true;

            if (fadeCoroutine != null)
            {
                StopCoroutine(fadeCoroutine);
                fadeCoroutine = null;
            }

            SetAlpha(1f);
        }

        // Joystick released (pointer up or touch ended)
        if (Input.GetMouseButtonUp(0))
        {
            if (joystickActive)
            {
                joystickActive = false;

                if (fadeCoroutine != null)
                    StopCoroutine(fadeCoroutine);

                fadeCoroutine = StartCoroutine(StartDissapearance());
            }
        }
    }

    IEnumerator StartDissapearance()
    {
        // Wait while active
        float time = 0f;
        while (time < activeTime)
        {
            time += Time.deltaTime;
            yield return null;
        }

        // Get current alpha from joystick image (or fallback to 1f)
        float startAlpha = joystickImage ? joystickImage.color.a : 1f;

        // Fade out
        time = 0f;
        while (time < fadeTime)
        {
            float t = time / fadeTime;
            float alpha = Mathf.Lerp(startAlpha, fadeAlpha, t);
            SetAlpha(alpha);

            time += Time.deltaTime;
            yield return null;
        }

        SetAlpha(fadeAlpha);
        fadeCoroutine = null;
    }

    void SetAlpha(float alpha)
    {
        if (joystickImage)
        {
            Color c = joystickImage.color;
            c.a = alpha;
            joystickImage.color = c;
        }

        if (controlStickImage)
        {
            Color c = controlStickImage.color;
            c.a = alpha;
            controlStickImage.color = c;
        }
    }
}
