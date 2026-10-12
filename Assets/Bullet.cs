using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("子弹属性")]
    [SerializeField] private float speed = 18f;
    [SerializeField] private float lifeTime = 2.5f;
    [SerializeField] private int damage = 1; // 单发伤害

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime, Space.World);
    }

    // Unity 2D 物理触发回调
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 判断碰到的物体是不是敌方
        if (collision.CompareTag("Enemy"))
        {
            // 获取敌机上的 Enemy 脚本组件并扣血
            Enemy enemy = collision.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }

            // 子弹击中目标后销毁自身
            Destroy(gameObject);
        }
    }
}