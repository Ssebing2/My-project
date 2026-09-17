# Don't Get Caught

> 어둠 속에서 전력을 복구하고, 좀비를 피해 탈출하라.

`Don't Get Caught`는 폐쇄된 공간을 탐색하며 Fuse를 찾아 전력을 복구하고,  
좀비의 추격을 피해 탈출하는 1인칭 공포 추격 게임입니다.

전력을 복구할수록 좀비의 이동 속도와 감지 능력이 강화되며,  
총 3개의 Electrical Panel을 복구하고 탈출하는 것이 목표입니다.

---

## Project Info

| 항목 | 내용 |
| --- | --- |
| 장르 | 1인칭 공포 / 추격 |
| 개발 형태 | 개인 프로젝트 |
| 개발 기간 | 15일 |
| 개발 인원 | 1명 |
| 플랫폼 | Windows PC |
| 개발 엔진 | Unity 2022.3.62f3 |
| 개발 언어 | C# |

---

## Game Flow

```text
맵 탐색
    ↓
Key / Fuse 획득
    ↓
이벤트 발생 및 좀비 등장
    ↓
Electrical Panel 탐색
    ↓
Fuse 설치 및 전력 복구
    ↓
좀비 강화
    ↓
다음 지역 탐색
    ↓
3개의 전력 복구
    ↓
탈출
```

---

## Controls

| Key | Action |
| --- | --- |
| `W A S D` | 이동 |
| `Mouse` | 시점 조작 |
| `Shift` | 달리기 |
| `E` | 상호작용 |
| `F` | 손전등 ON / OFF |
| `TAB` | 인벤토리 |
| `ESC` | 메뉴 |

---

## Main Features

### Player

- CharacterController 기반 1인칭 이동
- Mouse Look 및 달리기
- 손전등 ON / OFF
- Key / Fuse Inventory
- SphereCast 기반 상호작용

### Enemy AI

- NavMesh 기반 랜덤 순찰
- 거리, 시야각, Raycast를 이용한 플레이어 감지
- `Patrol → Chase → Search` 상태 전환
- 플레이어를 놓쳤을 경우 마지막 발견 위치 탐색
- 전력 복구 단계에 따른 Enemy Phase 강화

### Interaction

- Door Open / Close
- Key를 이용한 잠금 해제
- Key / Fuse 획득
- Electrical Panel 상호작용
- Outline 및 상호작용 안내 UI

### Power System

- 총 3개의 Electrical Panel
- Fuse 설치를 통한 전력 복구
- 전력 복구 진행도 UI
- 전력 복구 단계에 따른 Enemy 능력치 상승

### Sound

- Player / Enemy Footstep
- Zombie Voice
- Heartbeat
- Door / Item / Flashlight Sound
- In-Game / Main Menu BGM
- UI Sound

### UI

- Main Menu
- Pause Menu
- Settings
- HOW TO PLAY
- Mouse Sensitivity
- BGM ON / OFF
- Custom Cursor
- Game Over / Game Clear

---

## Enemy AI

Enemy는 세 가지 상태를 기반으로 동작합니다.

```text
Patrol
  ↓
플레이어 발견
  ↓
Chase
  ↓
플레이어 시야 상실
  ↓
Search
  ├─ 플레이어 재발견 → Chase
  └─ 탐색 실패 → Patrol
```

플레이어 감지는 단순 거리만 확인하지 않고 다음 조건을 함께 사용했습니다.

```text
Distance
+
Field of View
+
Raycast
```

랜덤 순찰에서는 `NavMesh.SamplePosition()`으로 찾은 위치가 실제로 도달 가능한지  
`NavMesh.CalculatePath()`와 `PathComplete`를 통해 추가로 확인하도록 구현했습니다.

---

## Enemy Phase

Electrical Panel을 복구할수록 Enemy의 난이도가 상승합니다.

| Phase | Patrol Speed | Chase Speed | Detect Distance |
| --- | ---: | ---: | ---: |
| Phase 1 | 2.5 | 4.5 | 9 |
| Phase 2 | 3.0 | 5.0 | 10 |
| Phase 3 | 3.5 | 5.5 | 11 |

각 Phase마다 서로 다른 Zombie Sound를 적용하여 난이도 변화를 전달하도록 구성했습니다.

---

## Tech Stack

### Development

- Unity 2022.3.62f3
- C#
- Visual Studio 2022
- Git / GitHub
- GitHub Desktop

### Unity

- CharacterController
- NavMesh / NavMeshAgent / NavMeshLink
- Raycast / SphereCast
- Animator / Animation Event
- AudioSource
- VideoPlayer / Render Texture
- TextMeshPro
- CanvasGroup

---

## Troubleshooting

### NavMesh 랜덤 순찰

`NavMesh.SamplePosition()`으로 선택한 위치가 NavMesh 위에 존재하더라도  
현재 Enemy 위치에서 실제로 도달할 수 없는 경우가 있었습니다.

`NavMesh.CalculatePath()`로 경로를 추가 검증하고  
`PathComplete`인 목적지만 선택하도록 수정하여 해결했습니다.

### Enemy Footstep

AudioSource와 AudioClip에는 문제가 없었지만 발소리가 재생되지 않았습니다.

호출 과정을 확인한 결과 Walk / Run Animation의 Animation Event가 사라진 것이 원인이었고,  
발이 지면에 닿는 Frame에 Event를 다시 설정하여 해결했습니다.

### Event Room Soft Lock

이벤트 발생 시 Door가 잠기는 기존 구조에서 Fuse와 Electrical Panel의 위치를 변경하면서  
플레이어가 방에서 나갈 수 없는 상황이 발생할 가능성이 있었습니다.

Door가 강제로 닫히는 연출은 유지하고 잠금 처리는 제거하여  
게임 진행이 막히지 않도록 수정했습니다.

### GameOverTrigger

Hierarchy 정리 과정에서 Zombie의 자식이었던 GameOverTrigger를 분리하면서  
Trigger가 시작 위치에 남아 게임 시작 직후 Game Over가 발생했습니다.

GameOverTrigger를 다시 Zombie의 자식으로 배치하여 해결했습니다.

---

## Development Status

**Version 1.0**

Windows Build 완료

- Core Gameplay
- Player System
- Enemy AI
- Interaction System
- Inventory
- Power System
- Sound / Lighting
- UI / Menu
- Pause / Settings
- Game Over / Game Clear
- Windows Build

---

## Developer

**서요셉**

Unity와 C#을 학습하며 제작한 개인 미니프로젝트입니다.
