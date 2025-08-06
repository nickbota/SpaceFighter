using Zenject;
using UnityEngine;

public class GameInstaller : MonoInstaller
{
    [Header("MonoBehaviour References")]
    [SerializeField] private GameManager gameManager;
    [SerializeField] private SoundManager soundManager;

    public override void InstallBindings()
    {
        if (soundManager != null) Container.Bind<SoundManager>().FromInstance(soundManager).AsSingle().NonLazy();
        if (gameManager != null) Container.Bind<GameManager>().FromInstance(gameManager).AsSingle().NonLazy();
    }
}