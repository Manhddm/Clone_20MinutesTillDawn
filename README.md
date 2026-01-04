﻿# Clone 20 Minutes Till Dawn (Unity)

Mục tiêu của repo này là **clone lại core gameplay** kiểu *20 Minutes Till Dawn* để luyện Unity: di chuyển 2D, ngắm theo chuột, và bắn đạn theo hướng con trỏ.

- Unity: **2022.3.62f2**
- Scene đang dùng để test: `Assets/Scenes/GamePlay.unity`

## Hiện tại mình đang làm gì?

- Làm **player movement + aiming + shooting** (cốt lõi vòng lặp gameplay).
- Tách input/logic bắn ra khỏi vũ khí bằng **event** để code dễ mở rộng.
- Làm hệ thống **Enemy AI** với di chuyển theo player và tránh đám đông.
- **Đã sửa bug**: Đạn bị xuyên qua quái do thiếu LayerMask trong Raycast.

## Bug đã sửa: Đạn xuyên qua quái

### Nguyên nhân:
- Trong `Projectile.cs`, phương thức `MoveAndCheckCollision()` sử dụng `Physics2D.Raycast` để kiểm tra va chạm.
- **Vấn đề**: Không có LayerMask → Raycast va chạm với TẤT CẢ layer (Player, Projectile, Terrain...) → có thể bỏ qua Enemy.
- Khi đạn spawn quá gần enemy hoặc enemy di chuyển nhanh, Raycast có thể miss va chạm.

### Giải pháp:
- Thêm `[SerializeField] private LayerMask targetLayer` vào Projectile.
- Sửa Raycast: `Physics2D.Raycast(transform.position, transform.right, moveDistance, targetLayer)`.
- **Trong Unity Editor**: Phải set `Target Layer` của Projectile prefab = "Enemy" layer.

### Cách test fix:
1. Mở Projectile prefab trong Unity.
2. Set field "Target Layer" = "Enemy" (layer 6).
3. Chạy game và bắn → đạn sẽ chỉ va chạm với Enemy, không bỏ qua nữa.

## Mình đang code những phần nào? (các file chính)

### `PlayerController` — đọc input, move, và quyết định “khi nào bắn”
File: `Assets/Game/Scripts/Controllers/PlayerController.cs`

- Đọc input từ `InputManager.Instance`:
  - `MoveInput` (Vector2)
  - `ShotInput` (bool)
- Tính hướng player → chuột và đẩy qua `WeaponController.HandleTransform(...)` để xoay/đặt vũ khí.
- Khi giữ bắn + đủ cooldown (fire rate) thì phát event bắn.

### `InputManager` — gom input (New Input System)
File: `Assets/Game/Scripts/Managers/InputManager.cs`

- Wrap New Input System thành 2 state đơn giản cho gameplay: `MoveInput`, `ShotInput`.

### `GameEventManager` — event bus đơn giản
File: `Assets/Game/Scripts/Core/GameEventManager.cs`

- Chứa event `OnPlayerShot`.
- `PlayerController` sẽ invoke event này khi đến thời điểm bắn.

### `WeaponController` — nhận event và spawn projectile
File: `Assets/Game/Scripts/Controllers/WeaponController.cs`

- `OnEnable`/`OnDisable`: subscribe/unsubscribe `GameEventManager.OnPlayerShot`.
- `FireProjectile()`:
  - `Instantiate(projectilePrefab, firePoint.position, firePoint.rotation)`
  - set vận tốc: `rb.velocity = firePoint.right * projectileSpeed`
- `HandleTransform(playerPosition, playerToMouseDirection, radius)`:
  - đặt weapon quanh player theo bán kính `radius`
  - xoay theo hướng chuột (`transform.right = direction`)
  - flip sprite theo góc để tránh hiển thị bị ngược

### `Projectile` — viên đạn với raycast collision detection
File: `Assets/Game/Scripts/Core/Projectile.cs`

- Di chuyển theo hướng `transform.right` với tốc độ `speed`.
- **Raycast-based collision**: Mỗi frame raycast trước khi di chuyển để tránh xuyên qua enemy.
- Khi trúng:
  - Gọi `HealthSystem.TakeDamage(damage)` trên enemy.
  - Gọi `Knockback()` để đẩy lùi enemy.
  - Destroy projectile.
- **Quan trọng**: Phải set `targetLayer` = "Enemy" trong Inspector để chỉ va chạm với enemy.

### `EnemyAI` — AI di chuyển theo player
File: `Assets/Game/Scripts/Core/EnemyAI.cs`

- Di chuyển về phía player với tốc độ `moveSpeed`.
- **Separation behavior**: Tránh đám đông bằng `Physics2D.OverlapCircleAll` với `enemyLayer`.
- Tính lực tách rời để enemy không chồng lên nhau.
- Cần gọi `SetTarget(player)` để enemy biết đuổi theo ai.

### `HealthSystem` — hệ thống máu và knockback
File: `Assets/Game/Scripts/Core/HealthSystem.cs`

- Quản lý HP: `maxHealth`, `currentHealth`.
- `TakeDamage(int damage)`: trừ máu, nếu <= 0 thì `Die()`.
- `Knockback(direction, force)`: đẩy lùi object với coroutine trong 0.2s.
- Dùng cho cả Player và Enemy.

### `GameplayManager` — quản lý spawn enemy
File: `Assets/Game/Scripts/Managers/GameplayManager.cs`

- Spawn enemy khi game bắt đầu (`Start()`).
- `SpawnEnemy()`: tạo số lượng enemy theo `enemyCount`.
- `GetRandomSpawnPosition()`: spawn enemy ở vị trí ngẫu nhiên quanh player với bán kính `spawnRadius = 10f`.
- Set target cho từng enemy để chúng biết đuổi theo player.

## Flow hiện tại (tóm tắt)

### Gameplay Loop:
`InputManager` (WASD/chuột) → `PlayerController.Update()`
→ cập nhật aim (`WeaponController.HandleTransform`)
→ nếu đủ điều kiện bắn: `GameEventManager.OnPlayerShot`
→ `WeaponController.FireProjectile()` spawn đạn
→ `Projectile` di chuyển và raycast → trúng Enemy
→ `HealthSystem.TakeDamage()` + `Knockback()` trên Enemy
→ Enemy chết nếu HP <= 0.

### Enemy AI Loop:
`GameplayManager.Start()` spawn enemies
→ `EnemyAI.Update()` tính hướng về player + separation force
→ di chuyển về phía player nhưng tránh đám đông.

## Hướng dẫn setup và chạy

### Unity Setup:
1. Mở project bằng Unity **2022.3.62f2**.
2. Mở scene: `Assets/Scenes/GamePlay.unity`.

### Layer Setup (quan trọng!):
- Project đã có layers: "Enemy" (layer 6) và "Player" (layer 7).
- **Phải set đúng layer cho objects**:
  - Player GameObject → set Layer = "Player"
  - Enemy prefab → set Layer = "Enemy"
  - Projectile prefab → set Layer theo ý muốn (hoặc Default)
  - **Projectile Inspector**: Set field "Target Layer" = "Enemy" (để đạn chỉ va chạm với quái)

### Prefab Setup:
1. **Projectile Prefab**:
   - Cần có component: `Projectile` script.
   - Set `Damage`, `Speed`, và **Target Layer** = "Enemy".
   - Cần Collider2D (để raycast detect được).

2. **Enemy Prefab**:
   - Cần components: `EnemyAI`, `HealthSystem`, `Collider2D`, `SpriteRenderer`.
   - Set Layer = "Enemy".
   - EnemyAI: set `Enemy Layer` = "Enemy" để separation hoạt động.

3. **Player**:
   - Cần components: `PlayerController`, `PlayerMovement`, `HealthSystem`.
   - Set Layer = "Player".

### Chạy game:
1. Nhấn Play trong Unity.
2. WASD để di chuyển.
3. Chuột trái (giữ) để bắn.
4. Enemy sẽ tự động spawn và đuổi theo player.

## TODO / Cần làm tiếp

- [ ] Thêm **health bar** cho player và enemy.
- [ ] Thêm **damage number** hiển thị khi trúng đòn.
- [ ] Làm **weapon system** với nhiều loại súng khác nhau.
- [ ] Thêm **experience/leveling** system.
- [ ] Làm **wave system** - spawn enemy theo sóng.
- [ ] Thêm **sound effects** và **visual effects** (muzzle flash, hit effect).
- [ ] Camera shake khi bắn hoặc bị đánh.
- [ ] Thêm **power-ups** và **upgrades**.
- [ ] Làm **main menu** và **game over** screen.

## Lưu ý khi dev

- Code đang dùng **event-driven architecture** với `GameEventManager` → dễ mở rộng features.
- Raycast collision trong Projectile → cần set LayerMask đúng để tránh bug xuyên qua quái.
- Enemy AI dùng separation behavior → cần set `enemyLayer` đúng trong Inspector.
- Fire rate đang hardcode `0.3f` trong PlayerController → nên refactor thành scriptable object hoặc weapon stats.
