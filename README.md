# MiniAzurShooter (2D Bullet-Hell Shooter Demo)

[![Unity Version](https://img.shields.io/badge/Unity-6000.3.19f1%20LTS-black?logo=unity)](https://unity.com/)
[![Render Pipeline](https://img.shields.io/badge/Pipeline-Universal%202D%20(URP)-blue)](https://unity.com/srp/Universal-Render-Pipeline)
[![Language](https://img.shields.io/badge/Language-C%23-blueviolet?logo=csharp)](https://docs.microsoft.com/en-us/dotnet/csharp/)
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

基于 **Unity 6 (URP 2D)** 独立落地的 2D 横版弹幕射击游戏 Demo。项目致敬《碧蓝航线》核心交互逻辑，实现了自机位移控制、射频弹幕机制、波次实体生成、低开销 2D 物理碰撞检测与自适应 HUD 计分系统。

---

## 🎮 核心系统实现 (Core Mechanics)

### 1. 玩家控制与视口约束 (`PlayerController.cs`)
* **平滑运动学位移**：解析双轴向输入（WASD / 方向键），通过向量归一化消除斜向移动加速度失真，结合 `Time.deltaTime` 实现帧率无关的位移模拟。
* **边界约束计算**：基于正交相机视口坐标计算边界阀值，使用 `Mathf.Clamp` 严格锁定自机活动范围，防止实体出界。
* **射频冷却控制**：采用基于时间戳的时间步控制机制，支持长按连发并严格限制最小发射间隔（Fire Rate Cooldown）。

### 2. 实体生成与生命周期管理 (`EnemySpawner.cs` / `Enemy.cs`)
* **波次发生器**：基于计时器定时调度，在屏幕右侧视界外部随机纵向坐标（$Y$ 轴区间）实例化敌方 Prefab。
* **边界回收机制**：为所有动态实体（子弹、敌机）配置生命周期计时器与越界坐标监测（$X < -10$），主动调用回收逻辑，杜绝底层内存与场景对象堆积。

### 3. 物理判定与碰撞响应 (`Bullet.cs`)
* **2D 触发器管线**：自机子弹与敌机均采用 `Collider2D (Is Trigger)` 与 `Rigidbody2D (Kinematic)` 组合，规避物理引擎求解刚体穿透的额外算力开销。
* **分级命中反馈**：在 `OnTriggerEnter2D` 回调中利用 Tag 过滤碰撞事件，调用敌机生命值接口扣减血量，并在生命值归零时通知全局管理器加分。

### 4. UI 响应式布局与单例架构 (`GameManager.cs`)
* **单例通信解耦**：采用严格单例模式（Singleton Pattern）构建 `GameManager`，实现跨模块计分广播，避免脚本强耦合。
* **跨分辨率适配**：集成 `TextMeshPro` 渲染高清字体，并通过 `Canvas Scaler` 配置 `Scale With Screen Size`（参考分辨率 1920x1080，Match 0.5），保证 PC 视窗与不同长宽比手机屏幕下的 UI 自适应排版。

---

## 🛠️ 技术栈与工程规范 (Tech Specs)

| 模块 | 实现方案 |
| :--- | :--- |
| **引擎版本** | Unity 6 LTS (6000.3.19f1) |
| **编程语言** | C# (.NET Standard 2.1) |
| **渲染管线** | Universal Render Pipeline (URP 2D) |
| **物理系统** | Unity 2D Physics Pipeline (Trigger-based) |
| **UI 框架** | Unity UGUI + TextMeshPro |
| **版本管理** | Git + Unity 官方 `.gitignore` 规范配置 |

---

## 🚀 快速上手 (Getting Started)

1. **环境准备**：安装 Unity Hub 及 **Unity 6 (6000.3.19f1)** 或更高版本。
2. **克隆项目**：
   ```bash
   git clone [https://github.com/hrwzw/MiniAzurShooter.git](https://github.com/hrwzw/MiniAzurShooter.git)
