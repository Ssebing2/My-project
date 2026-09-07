# Don't Get Caught

> 폐건물을 탐색하며 전력을 복구하고 Enemy를 피해 탈출하는 1인칭 공포 추격 게임

## 🎮 프로젝트 소개

- 장르 : 1인칭 공포 / 추격
- 플랫폼 : PC
- 개발 엔진 : Unity
- 개발 언어 : C#
- 개발 형태 : 개인 미니프로젝트

---

## 🕹️ 게임 플레이

플레이어는 폐건물을 탐색하며 Key와 Fuse를 획득하고,
총 3개의 배전함을 복구하여 탈출해야 합니다.

Enemy는 건물 내부를 순찰하며,
플레이어를 발견하면 추격하고 시야에서 놓치면 마지막 위치를 탐색합니다.

```text
탐색
 ↓
Key / Fuse 획득
 ↓
Enemy 이벤트
 ↓
배전함 3개 복구
 ↓
탈출
 ↓
Game Clear
```
⚙️ 주요 구현 기능
Player
CharacterController 기반 1인칭 이동
걷기 / 달리기
마우스 시점 조작
손전등
Animation Event 기반 발소리
Enemy AI
NavMeshAgent 기반 이동
Patrol / Chase / Search 상태 머신
거리 + 시야각 + Raycast 기반 플레이어 감지
마지막 목격 위치 탐색
Interaction
IInteractable 기반 공통 상호작용
SphereCast 기반 오브젝트 감지
상호작용 가능 오브젝트 Outline
Door / Key / Fuse / ElectricalPanel 상호작용
Game System
Key / Fuse Inventory
잠금 Door 시스템
3개의 배전함 복구 시스템
GameOver / GameClear
이벤트 Trigger
UI / Settings
Main Menu
Pause / Resume
BGM ON / OFF
Mouse Sensitivity
Custom Slider
GameOver / GameClear UI
Audio
Player / Enemy 발소리
Enemy 추격 및 심장박동
Door / Item / Fuse 사운드
Main Menu / InGame BGM
UI Hover / Click 사운드
🛠️ 사용 기술

Unity C# NavMesh NavMeshAgent
CharacterController Raycast SphereCast
Animator Animation Event VideoPlayer
AudioSource RenderTexture Unity UI

🔧 주요 트러블슈팅
Enemy Raycast가 Floor를 감지하던 문제 해결
Map MeshCollider로 인해 문을 통과하지 못하던 문제 해결
GameOver / GameClear 이후 Animation Event 사운드가 계속 재생되던 문제 해결
VideoPlayer와 Time.timeScale 충돌 문제 해결
Custom Slider Fill 위치 문제 해결
UI Event 중복으로 사운드가 중복 재생되던 문제 해결
📌 개발 현황
 Player Controller
 Enemy AI
 Interaction System
 Inventory
 Electrical Panel Puzzle
 GameOver / GameClear
 Main Menu
 Pause / Settings
 Audio System
 Lighting Polish
 Horror Event Polish
 Final Play Test
📚 프로젝트를 통해 배운 점

NavMesh 기반 AI 상태 머신을 직접 구현하고,
Player, Enemy, Interaction, UI, Audio 등 여러 시스템을 연결하면서
각 시스템의 상태와 역할을 분리하여 관리하는 방법을 학습했습니다.
