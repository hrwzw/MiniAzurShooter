using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("移动设置")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float minX = -8f, maxX = 8f;
    [SerializeField] private float minY = -4.5f, maxY = 4.5f;

    [Header("战斗设置")]
    [SerializeField] private GameObject bulletPrefab; // 引用的子弹预制体
    [SerializeField] private Transform firePoint;     // 炮口发射点
    [SerializeField] private float fireRate = 0.15f;   // 射击间隔（秒）
    private float nextFireTime = 0f;

    void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector2 inputDirection = new Vector2(h, v).normalized;
        Vector3 displacement = inputDirection * moveSpeed * Time.deltaTime;

        Vector3 targetPos = transform.position + displacement;
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.y = Mathf.Clamp(targetPos.y, minY, maxY);

        transform.position = targetPos;
    }

    private void HandleShooting()
    {
        // 按住空格键或鼠标左键持续开火（带射频 CD）
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null) return;

        // 若未指定炮口位置，则默认以自身中心点发射
        Vector3 spawnPosition = firePoint != null ? firePoint.position : transform.position;
        Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
    }
}