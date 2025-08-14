using UnityEngine;
using Zenject;
using DG.Tweening;

public class WaveWarning : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private EnemyFormation enemyFormation;
    [SerializeField] private GameObject warningObject;
    [SerializeField] private AudioClip warningSound;

    [Header("Tween Parameters")]
    [SerializeField] private Vector3 punch = new Vector3(0.2f, 0.2f, 0.2f);
    [SerializeField] private float appearDuration = 0.25f;
    [SerializeField] private float punchDuration = 0.5f;
    [SerializeField] private float delay = 0.25f;
    [SerializeField] private int loops = 3;

    private SoundManager soundManager;
    [Inject]
    private void Init(SoundManager soundManager)
    {
        this.soundManager = soundManager;
    }
    private void OnEnable()
    {
        warningObject.SetActive(false);
        warningObject.transform.localScale = Vector3.zero;

        enemyFormation.OnNewWaveComing += OnNewWaveComing;
    }
    private void OnDisable()
    {
        enemyFormation.OnNewWaveComing -= OnNewWaveComing;
    }

    private void OnNewWaveComing()
    {
        warningObject.SetActive(true);
        warningObject.transform.DOScale(Vector3.one, appearDuration).SetEase(Ease.InElastic).OnComplete(() =>
        {
            int currentLoop = 0;

            warningObject.transform
                .DOPunchScale(punch, punchDuration)
                .SetLoops(loops, LoopType.Restart)
                .SetDelay(delay)
                .OnStart(() => soundManager.PlaySound(warningSound, false))
                .OnStepComplete(() =>
                {
                    currentLoop++;

                    // Play sound only if it's not the final loop
                    if (currentLoop < loops)
                        soundManager.PlaySound(warningSound, false);
                })
                .OnComplete(() =>
                {
                    warningObject.SetActive(false);
                });
        });
    }
}