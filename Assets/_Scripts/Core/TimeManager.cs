using UnityEngine;
using DG.Tweening;

public class TimeManager : MonoBehaviour
{
    private float defaultTimeScale = 1;
    private Tween currentTween;

    public void StopTime()
    {
        float freezeDuration = 0.05f;
        float resumeDuration = 0.05f;

        currentTween?.Kill();
        Time.timeScale = 0f;

        currentTween = DOVirtual.DelayedCall(freezeDuration, () =>
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, defaultTimeScale, resumeDuration)
                .SetUpdate(true); 
        }).SetUpdate(true); // ensures tween runs during time freeze
    }
    public void ResetTime()
    {
        Time.timeScale = defaultTimeScale;
        currentTween?.Kill();
    }
}