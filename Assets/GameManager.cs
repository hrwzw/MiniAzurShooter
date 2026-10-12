using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间

public class GameManager : MonoBehaviour
{
    // 单例模式：方便任何脚本（如 Enemy）直接访问加分
    public static GameManager Instance { get; private set; }

    [Header("UI 引用")]
    [SerializeField] private TextMeshProUGUI scoreText;

    private int currentScore = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateScoreUI();
    }

    // 增加分数接口
    public void AddScore(int amount)
    {
        currentScore += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = $"SCORE: {currentScore}";
        }
    }
}