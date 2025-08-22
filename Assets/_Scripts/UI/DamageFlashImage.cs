using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Image))]
public class DamageFlashImage : MonoBehaviour
{
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float offDelay = 0.1f;
    [SerializeField] private Color transparent = new Color(1, 1, 1, 0);
    [SerializeField] private Color damageColor = new Color(1, 0, 0, 0.4f);
    [SerializeField] private Color enemyKillColor = new Color(1, 0, 0, 0.4f);
    private Image img;

    private void Awake()
    {
        img = GetComponent<Image>();
    }

    public void ShowDamageFlash()
    {
        img.DOColor(damageColor, duration).SetUpdate(true).OnComplete(() =>
        {
            img.DOColor(transparent, duration / 2).SetDelay(offDelay);
        });
    }
    public void ShowEnemyKilledFlash()
    {
        img.DOColor(enemyKillColor, duration / 2).SetUpdate(true).OnComplete(() =>
        {
            img.DOColor(transparent, duration / 4).SetDelay(offDelay);
        });
    }
}