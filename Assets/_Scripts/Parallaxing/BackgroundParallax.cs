using UnityEngine;
using Zenject;

public class BackgroundParallax : MonoBehaviour
{
    [SerializeField] private Transform background1;
    [SerializeField] private Transform background2;
    [SerializeField] private float scrollSpeed = 2f;
    private float spriteHeight;
    private bool moving;

    private GameManager gameManager;
    [Inject]
    private void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
    private void OnEnable()
    {
        gameManager.OnGameStateChanged += OnGameStateChanged;
    }
    private void OnDisable()
    {
        gameManager.OnGameStateChanged -= OnGameStateChanged;
    }
    private void Start()
    {
        spriteHeight = background1.GetComponent<SpriteRenderer>().bounds.size.y;
    }
    private void Update()
    {
        if (!moving) return;

        Vector3 move = Vector3.down * scrollSpeed * Time.deltaTime;

        background1.position += move;
        background2.position += move;

        // Reposition if a background has moved completely out of view
        if (background1.position.y <= -spriteHeight)
            background1.position += Vector3.up * spriteHeight * 2f;

        if (background2.position.y <= -spriteHeight)
            background2.position += Vector3.up * spriteHeight * 2f;
    }

    public void OnGameStateChanged(GameManager.GameState gameState)
    {
        moving = (gameState == GameManager.GameState.Game);
    }
}
