# Don't Get Caught

> Enemy의 추격을 피해 아이템을 획득하고 탈출하는 1인칭 공포 게임

## 🎮 프로젝트 소개

**Don't Get Caught**는 폐쇄된 공간을 탐색하며  
플레이어를 감지하고 추격하는 Enemy를 피해 탈출하는 1인칭 공포 게임입니다.

- 장르 : 1인칭 공포 / 추격 / 탈출
- 플랫폼 : PC
- 개발 엔진 : Unity
- 개발 언어 : C#
- 개발 기간 : 15일
- 개발 형태 : 개인 프로젝트

---

## 🔥 주요 기능

### 👻 Enemy AI

- NavMeshAgent 기반 랜덤 순찰
- Patrol / Chase / Search 상태 관리
- 거리 + 시야각 + Raycast를 이용한 Player 감지
- Player 마지막 위치 탐색
- NavMeshLink를 이용한 문 통과

### 🎮 Player

- CharacterController 기반 이동
- 마우스 시점 회전
- Shift 달리기
- 카메라 Raycast 기반 상호작용

### 🖐️ Interaction

- `IInteractable`을 이용한 공통 상호작용 구조
- E키를 통한 오브젝트 상호작용
- 문 열기 / 닫기
- 아이템 획득
- 일반 문 / 잠긴 문 구분
- 열쇠 보유 여부에 따른 문 잠금 해제

### 🎒 Inventory

- PlayerInventory를 통한 아이템 상태 관리
- 열쇠 획득 및 보유 여부 확인

---

## 🧩 주요 시스템 구조

```text
Player
 └─ Raycast
      ↓
 IInteractable
   ├─ Door
   │    └─ Open / Close
   │
   └─ Item
        └─ PlayerInventory
                ↓
              HasKey
                ↓
           Locked Door
```

---

## 🛠️ 사용 기술

`Unity` `C#` `NavMesh` `NavMeshAgent` `NavMeshLink`  
`Raycast` `CharacterController` `Interface` `Enum`

---

## 📌 개발 진행 상황

- [x] Player 이동 / 시점 / 달리기
- [x] Enemy 랜덤 순찰
- [x] Enemy Patrol / Chase / Search
- [x] 거리 / 시야각 / Raycast 감지
- [x] 문 상호작용
- [x] 아이템 상호작용
- [x] 열쇠 / 잠긴 문 시스템
- [ ] 맵 Collider 폴리싱
- [ ] Enemy Animation
- [ ] Sound / Lighting
- [ ] 게임 진행 및 엔딩 연출

---

## 📅 Development Log

### Day 1
- 맵 및 NavMesh 환경 구성
- Enemy 랜덤 순찰 구현

### Day 2
- Enemy 상태 시스템 구현
- Patrol / Chase / Search 구현
- Player 감지 시스템 구현

### Day 3
- Player 달리기 구현
- NavMeshLink를 활용한 Enemy 문 통과 구현

### Day 4
- `IInteractable` 기반 상호작용 시스템 구현
- 문 열기 / 닫기 구현
- 아이템 획득 구현
- PlayerInventory 구현
- 열쇠 / 잠긴 문 시스템 구현

---

## 🚧 개발 중

현재 개발 중인 프로젝트입니다.

핵심 게임 기능을 우선 구현한 뒤  
맵 Collider, Animation, Lighting, Sound 등의 폴리싱을 진행할 예정입니다.
