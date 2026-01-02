# Clone 20 Minutes Till Dawn (Unity)

Mục tiêu của repo này là **clone lại core gameplay** kiểu *20 Minutes Till Dawn* để luyện Unity: di chuyển 2D, ngắm theo chuột, và bắn đạn theo hướng con trỏ.

- Unity: **2022.3.62f2**
- Scene đang dùng để test: `Assets/Scenes/GamePlay.unity`

## Hiện tại mình đang làm gì?

- Làm **player movement + aiming + shooting** (cốt lõi vòng lặp gameplay).
- Tách input/logic bắn ra khỏi vũ khí bằng **event** để code dễ mở rộng.

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

## Flow hiện tại (tóm tắt)

`InputManager` (WASD/chuột) → `PlayerController.Update()`
→ cập nhật aim (`WeaponController.HandleTransform`)
→ nếu đủ điều kiện bắn: `GameEventManager.OnPlayerShot`
→ `WeaponController.FireProjectile()` spawn đạn.
