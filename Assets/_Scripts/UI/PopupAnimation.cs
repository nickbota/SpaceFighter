using UnityEngine;
using DG.Tweening;

public class PopupAnimation : MonoBehaviour
{
    [SerializeField] private float appearDuration = 0.25f;
    [SerializeField] private float delay = 0.25f;

    private void OnEnable()
    {
        PlayPopupAnimation();
    }
    private void PlayPopupAnimation()
    { 
        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, appearDuration).SetEase(Ease.InOutElastic).SetUpdate(true).OnComplete(() =>
        {
            transform.DOScale(Vector3.zero, appearDuration / 2).SetDelay(delay).SetUpdate(true).OnComplete(() => gameObject.SetActive(false));
        });
    }
}