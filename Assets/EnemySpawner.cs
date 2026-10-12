using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("生成配置")]
    [SerializeField] private GameObject enemyPrefab;   // 敌机预制体
    [SerializeField] private float spawnInterval = 1.2f; // 生成间隔（秒）

    [Header("生成位置范围")]
    [SerializeField] private float spawnX = 10f;        // 屏幕右侧外边缘 X 坐标
    [SerializeField] private float minY = -4f;          // 随机生成 Y 轴下界
    [SerializeField] private float maxY = 4f;           // 随机生成 Y 轴上界

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        // 达到时间间隔则生成一只敌机
        if (timer >= spawnInterval)
        {
            SpawnEnemy();
            timer = 0f;
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // 在指定的 Y 轴区间内随机取一个纵向高度
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(spawnX, randomY, 0f);

        // 实例化敌机
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}