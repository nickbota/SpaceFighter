using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class DamageFlashImage : MonoBehaviour
{
    [SerializeField] private float finalAlpha = 0.4f;
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float offDelay = 0.1f;
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void ShowDamageFlash()
    {
        img.DOFade(finalAlpha, duration).OnComplete(() =>
        {
            img.DOFade(0, duration / 2).SetDelay(offDelay);
        });
    }
}