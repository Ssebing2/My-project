# Don't Get Caught

> 폐건물을 탐색하며 전력을 복구하고 Enemy를 피해 탈출하는 1인칭 공포 추격 게임

---

## 🎮 프로젝트 소개

| 항목 | 내용 |
| --- | --- |
| 프로젝트명 | Don't Get Caught |
| 장르 | 1인칭 공포 / 추격 |
| 플랫폼 | PC |
| 개발 엔진 | Unity |
| 개발 언어 | C# |
| 개발 도구 | Unity, Visual Studio 2022 |
| 버전 관리 | GitHub / GitHub Desktop |
| 개발 형태 | 개인 미니프로젝트 |

---

## 🕹️ 게임 플레이

플레이어는 폐건물 내부를 탐색하며 **Key와 Fuse를 획득**하고,  
총 **3개의 배전함을 복구하여 탈출**해야 합니다.

건물 내부의 Enemy는 주변을 순찰하며 플레이어를 탐색합니다.  
플레이어를 발견하면 추격하고, 시야에서 놓치면 마지막으로 확인한 위치를 탐색합니다.

```text
맵 탐색
   ↓
Key / Fuse 획득
   ↓
잠긴 공간 진입
   ↓
Enemy 이벤트 발생
   ↓
배전함 탐색 및 Fuse 장착
   ↓
배전함 3개 복구
   ↓
전력 복구
   ↓
탈출
   ↓
Game Clear
```

---

## ⚙️ 주요 구현 기능

### Player

- CharacterController 기반 1인칭 이동
- 걷기 / 달리기
- 마우스 시점 조작
- 중력 처리
- 손전등 ON / OFF
- Animator 기반 걷기 / 달리기 애니메이션
- Animation Event 기반 발소리
- Enemy 추격 시 심장박동 연출

### Enemy AI

- NavMeshAgent 기반 이동
- `Patrol / Chase / Search` 상태 머신
- NavMesh 내부 랜덤 순찰
- 거리 + 시야각 + Raycast 기반 플레이어 감지
- 플레이어 발견 시 추격
- 플레이어를 놓쳤을 경우 마지막 목격 위치 탐색
- 상태에 따른 이동 속도 및 애니메이션 변경
- 발소리 / 랜덤 음성 / 추격 사운드

### Interaction

- `IInteractable` Interface 기반 공통 상호작용
- SphereCast 기반 상호작용 오브젝트 감지
- 상호작용 가능 오브젝트 Outline 표시
- Door / Key / Fuse / ElectricalPanel 상호작용
- 여러 개의 Key / Fuse 획득 및 소비

### Door

- 문 열기 / 닫기
- 잠긴 문 구현
- Key를 이용한 잠금 해제
- Key 사용 시 Inventory에서 소비
- NavMeshLink 활성화 / 비활성화
- 이벤트를 통한 강제 문 닫힘 및 잠금
- 상황별 Door Sound 적용

### Electrical Panel

- 총 3개의 배전함 구현
- Fuse를 이용한 배전함 복구
- 각 배전함의 Fuse 장착 상태 관리
- 모든 배전함 복구 여부를 GameManager에서 관리
- 3개의 배전함 복구 완료 후 전력 복구
- Fuse 삽입 / 최종 전력 복구 사운드 적용

### Game System

- Key / Fuse Inventory
- Event Trigger
- 전력 복구 조건 관리
- GameOver / GameClear
- GameOver / GameClear 영상 재생
- Restart / Main Menu 이동
- 게임 종료 시 Player / Enemy / Audio 정지

### UI / Settings

- Main Menu
- Pause / Resume
- Settings Menu
- BGM ON / OFF
- Mouse Sensitivity 설정
- Custom Slider UI
- Button Hover / Click 효과
- GameOver / GameClear UI

### Audio

- Main Menu BGM
- InGame Horror BGM
- Pause Menu Audio
- Player 발소리
- Player 심장박동
- Enemy 발소리 / 음성 / 추격 사운드
- Door 열기 / 닫기 / 잠금 사운드
- Key / Fuse 획득 사운드
- Fuse 삽입 / 전력 복구 사운드
- UI Hover / Click 사운드

---

## 🛠️ 사용 기술

`Unity` `C#` `CharacterController` `NavMesh` `NavMeshAgent` `NavMeshLink`

`Raycast` `SphereCast` `LayerMask` `Interface` `Collider` `Trigger`

`Animator` `Animation Event` `AudioSource` `VideoPlayer` `RenderTexture`

`Unity UI` `Slider` `Event Trigger` `Coroutine` `SceneManager`

---

## 🔧 주요 트러블슈팅

### Enemy Raycast 감지 문제

Enemy의 Raycast가 Player 대신 Floor를 감지하는 문제가 발생했습니다.

Raycast의 시작점과 Player Target의 높이를 조절하여  
Enemy의 시야 높이에서 Player를 감지하도록 수정했습니다.

### Map Collider 문제

문이 열려 있어도 Player가 통과하지 못하는 문제가 발생했습니다.

Physics Debugger를 이용하여 Collision Geometry를 확인한 결과  
맵 Asset의 MeshCollider가 실제 모델과 맞지 않는 영역까지 차지하고 있음을 확인하고 수정했습니다.

### GameOver / GameClear 이후 발소리 재생 문제

PlayerController의 입력을 정지해도 Animator의 Animation Event가 계속 실행되면서  
게임 종료 이후에도 발소리가 출력되는 문제가 발생했습니다.

게임 종료 시 Animator와 AudioSource를 함께 정지하도록 수정했습니다.

### Pause 상태에서 VideoPlayer가 정지하는 문제

Pause 시 `Time.timeScale = 0`이 적용되면서 Pause Menu 영상도 함께 정지했습니다.

VideoPlayer의 Update Mode를 `Unscaled Game Time`으로 변경하여  
Pause 상태에서도 영상이 정상적으로 재생되도록 수정했습니다.

### Custom Slider Fill 문제

Mouse Sensitivity Slider의 값이 변경될 때  
Fill의 왼쪽 시작점까지 함께 움직이는 문제가 발생했습니다.

Fill Image를 다음과 같이 설정하여 시작점을 고정했습니다.

```text
Image Type  = Filled
Fill Method = Horizontal
Fill Origin = Left
```

### BGM 설정 유지 문제

Settings에서 BGM을 OFF한 뒤 Resume하면  
InGame BGM이 다시 재생되는 문제가 발생했습니다.

사용자의 BGM 설정 상태를 `_isBgmOn`으로 별도 관리하고,  
Resume 시 해당 값을 확인한 뒤 BGM을 재생하도록 수정했습니다.

### UI Sound 중복 문제

Main Menu 버튼의 Pointer Enter에 Hover Sound와 Click Sound가 동시에 등록되어  
마우스를 올리는 것만으로 두 사운드가 함께 재생되는 문제가 발생했습니다.

Pointer Enter에서는 Hover Sound만 실행하고,  
Click Sound는 Button OnClick에서만 실행하도록 역할을 분리했습니다.

---

## 📌 개발 현황

- [x] Player 이동 / 카메라
- [x] Player Animator
- [x] 손전등
- [x] Enemy Patrol / Chase / Search
- [x] Player Detection
- [x] SphereCast Interaction
- [x] Interaction Outline
- [x] Key / Fuse Inventory
- [x] Door System
- [x] Event Trigger
- [x] Electrical Panel
- [x] 3개 배전함 복구 시스템
- [x] GameOver / GameClear
- [x] Main Menu
- [x] Pause Menu
- [x] Settings Menu
- [x] BGM ON / OFF
- [x] Mouse Sensitivity
- [x] Custom Slider
- [x] Player / Enemy Audio
- [x] Interaction Audio
- [x] UI Audio
- [x] Map Collider 수정
- [ ] Lighting Polish
- [ ] Horror Event Polish
- [ ] Enemy 세부 밸런싱
- [ ] 전체 플레이 테스트 및 버그 수정

---

## 📚 프로젝트를 통해 배운 점

이번 프로젝트를 통해 단순히 각각의 기능을 구현하는 것뿐만 아니라  
여러 시스템이 서로 연결될 때 **상태와 역할을 분리하여 관리하는 방법**을 경험했습니다.

특히 Player, Enemy, Inventory, Interaction, UI, Audio 등의 시스템을 연결하면서  
하나의 상태 변화가 다른 시스템에 어떤 영향을 주는지 고려하며 구현하는 경험을 할 수 있었습니다.

또한 문제 발생 시 Script만 확인하는 것이 아니라  
Hierarchy, Inspector, Collider, Animator, Event 연결 상태 등을 함께 확인하며  
Unity 프로젝트를 디버깅하는 방법을 익혔습니다.

---

## 🚧 향후 계획

- Lighting 및 환경 연출 개선
- 공포 이벤트 연출 강화
- Enemy 추격 밸런스 조정
- Audio Volume 세부 조정
- UI 세부 Polish
- 전체 플레이 동선 점검
- 최종 버그 수정
- Build 및 플레이 영상 제작
