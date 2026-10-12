using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("敌机属性")]
    [SerializeField] private float speed = 3f;     // 向左移速
    [SerializeField] private int maxHealth = 3;   // 生命值
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        // 沿 X 轴负方向（向左）飞行
        transform.Translate(Vector3.left * speed * Time.deltaTime, Space.World);

        // 飞出屏幕左侧边缘后销毁，释放内存
        if (transform.position.x < -10f)
        {
            Destroy(gameObject);
        }
    }

    // 受到子弹伤害
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // 敌机被击毁时，调用全局单例增加 100 分
        if (GameManager.Instance != null)
        {
            GameManager.Instance.AddScore(100);
        }

        Destroy(gameObject);
    }
}
