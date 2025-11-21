# 🤖 3D Survival Game: FY-317

## 1. 프로젝트 개요

> FY-317은 3D 월드에서 전투, 자원채집, 제작, 스토리를 즐길수 있는 종합게임입니다.
> 주인공은 우연히 방랑도중 우연히 만나게 된 버려진 로봇을 통해 과거를 떠올리게 됩니다.

<br>

## 2. 팀원 소개

<img width="1254" height="609" alt="image" src="https://github.com/user-attachments/assets/3928405c-ae66-468b-9414-c486eeeb3e3e" />


## 3. 와이어프레임

<img width="819" height="711" alt="image" src="https://github.com/user-attachments/assets/96fa0ee8-6ec7-481f-8b52-a46182716f7a" />

## 4. 프로젝트 구조

### 아키텍처 개요

본 프로젝트는 각 기능의 독립성과 확장성을 높이기 위해 **매니저 기반의 싱글톤 패턴(Manager-based Singleton Pattern)**과 **데이터 중심 설계(Data-Driven Design)**를 핵심 아키텍처로 채택했습니다. 각 시스템은 명확하게 역할을 분리하여 유지보수와 협업 효율성을 극대화했습니다.

<br>

### 주요 시스템별 구조

#### ⚙️ **코어 시스템 (Core System)**
-   **`GameManager`**: 시간의 흐름(낮/밤), 게임 상태(Pause/Resume), 엔딩 처리 등 게임의 전반적인 라이프사이클을 총괄하는 컨트롤 타워입니다.
-   **`InputManager`**: Unity의 새로운 Input System을 기반으로 `InputBinder`를 통해 Player, UI 등 상황에 맞는 입력 체계를 관리하고 전환합니다.
-   **`AudioManager`**: BGM, 효과음 등 게임의 모든 사운드 출력을 관리하며, `GameManager`의 시간대에 맞춰 배경음악을 교체하는 등 동적인 사운드 환경을 제공합니다.
-   **`CameraManager`**: 플레이어를 추적하는 3인칭 카메라의 움직임, 회전, 장애물 충돌 처리 등 모든 카메라 로직을 담당합니다.

#### 🚶 **플레이어 시스템 (Player System)**
-   **역할 분리(SoC)** 원칙에 따라 `Player`(총괄), `PlayerBehaviour`(움직임), `PlayerStatus`(상태값), `PlayerInputHandler`(입력 처리)로 클래스를 분리하여 설계했습니다.
-   `PlayerStatus`는 `Condition` 클래스를 통해 체력, 스태미나, 허기, 갈증을 관리하며, 모든 행동은 상태값과 유기적으로 연동됩니다.
-   C# `event`를 활용하여 플레이어의 행동(점프, 공격 등)이 애니메이션, 사운드 시스템에 독립적으로 신호를 보냅니다.

#### 🤝 **상호작용 시스템 (Interaction System)**
-   **`IInteractable`, `IGatherable`, `ICombatable` 인터페이스**를 기반으로 설계되어 확장성이 매우 높습니다.
-   `Player`에 부착된 `RayDetector`가 전방의 객체를 감지하고, 해당 객체가 어떤 인터페이스를 구현했는지에 따라 상호작용 방식이 결정됩니다.
-   이 구조 덕분에 새로운 상호작용 객체(NPC, 아이템, 자원 등)를 인터페이스 구현만으로 쉽게 추가할 수 있습니다.

#### 📜 **퀘스트 & 네러티브 시스템 (Quest & Narrative System)**
-   **ScriptableObject**를 활용한 **데이터 중심 설계**의 핵심입니다. `QuestData`, `StoryData`를 통해 퀘스트 내용, 대사, 진행 조건을 코드와 완벽히 분리했습니다.
-   **`QuestManager`**: 퀘스트의 수락, 진행 상태 업데이트, 완료 처리를 담당합니다.
-   **`NarrativeManager`**: `DialogueManager`와 협력하여 게임의 프롤로그, 주요 이벤트, 엔딩 등 큰 줄기의 스토리를 진행합니다.

#### 🛠️ **아이템 & 제작 시스템 (Item & Crafting System)**
-   `ItemData`, `RecipeData` 등 ScriptableObject를 통해 아이템과 제작법 정보를 관리합니다.
-   `Inventory` 클래스는 플레이어의 소지 아이템을 관리하며, `Recipe` 클래스는 `Inventory`의 아이템을 기반으로 제작 가능 여부를 판단합니다.


📂 프로젝트 구조 (Scripts)
Assets
└── 📂 02_Scripts
    ├── 📂 Camera                 # 카메라 로직 관련
    │   ├── CameraBehaviour.cs
    │   ├── CameraInputHandler.cs
    │   └── CameraManager.cs
    │
    ├── 📂 Core                   # 게임의 핵심 로직 및 매니저
    │   ├── GameTimestamp.cs
    │   └── 📂 Managers
    │       ├── AudioManager.cs
    │       ├── GameManager.cs
    │       └── 📂 InputManager
    │           ├── EInputActionAssetName.cs
    │           ├── InputActionMapExtension.cs
    │           ├── InputBinder.cs
    │           ├── InputManager.cs
    │           └── UIInputHandler.cs
    │
    ├── 📂 Enemy                  # 적(Enemy) 관련 로직
    │   ├── Enemy.cs
    │   ├── EnemyHealthBar.cs
    │   ├── EnemyPlayerDeath.cs
    │   ├── EnemySpawn.cs
    │   └── NarrativeEnemySpawner.cs
    │
    ├── 📂 Interface              # 상호작용을 위한 인터페이스
    │   ├── IGatherable.cs
    │   └── IInteractable.cs
    │
    ├── 📂 Item                   # 아이템, 인벤토리, 제작 관련
    │   ├── HouseObject.cs
    │   ├── Inventory.cs
    │   ├── InventoryUI.cs
    │   ├── Item.cs
    │   ├── ItemObject.cs
    │   ├── Recipe.cs
    │   ├── RecipeSlots.cs
    │   ├── RecipeUI.cs
    │   ├── ResourceObject.cs
    │   ├── WaterObject.cs
    │   └── 📂 Data
    │       ├── ItemData.cs
    │       └── RecipeData.cs
    │
    ├── 📂 Narrative              # 내러티브, 대화, 스토리 진행
    │   ├── DialogueManager.cs
    │   ├── NarrativeManager.cs
    │   ├── 📂 Data
    │   │   ├── DialogueData.cs
    │   │   ├── DialogueLine.cs
    │   │   └── StoryData.cs
    │   └── 📂 Entities
    │       ├── Dialogue.cs
    │       └── Story.cs
    │
    ├── 📂 NarrativeCharacter     # 내러티브에 등장하는 특정 캐릭터
    │   └── FY-317.cs
    │
    ├── 📂 Player                 # 플레이어 캐릭터 관련
    │   ├── Character.cs
    │   ├── Condition.cs
    │   ├── Player.cs
    │   ├── PlayerAnimationController.cs
    │   ├── PlayerBehaviour.cs
    │   ├── PlayerInputHandler.cs
    │   ├── PlayerStatus.cs
    │   └── PlayerStatusData.cs
    │
    ├── 📂 Quest                  # 퀘스트 시스템 관련
    │   ├── QuestManager.cs
    │   ├── 📂 Context
    │   │   └── QuestProcessContext.cs
    │   ├── 📂 Data
    │   │   ├── QuestData.cs
    │   │   ├── 📂 Consequence          # 퀘스트 결과
    │   │   │   ├── ConsequenceSubmitItem.cs
    │   │   │   ├── ConsequenceUnlockRecipe.cs
    │   │   │   └── QuestConsequence.cs
    │   │   └── 📂 UnlockCondition     # 퀘스트 잠금 해제 조건
    │   │       ├── PrerequisitesQuest.cs
    │   │       └── QuestUnlockCondition.cs
    │   └── 📂 Entities
    │       └── QuestEntity.cs
    │
    ├── 📂 RayDetector            # Raycast를 이용한 오브젝트 감지
    │   ├── CombatableDetector.cs
    │   ├── GatherableDetector.cs
    │   ├── InteractableDetector.cs
    │   └── RayDetector.cs
    │
    ├── 📂 Temp                   # 임시 테스트용 스크립트
    │   ├── 📂 Gw
    │   │   ├── DayWidget.cs
    │   │   ├── ... (Test Scripts)
    │   └── 📂 Sura
    │       ├── AttackTestObject.cs
    │       └── ... (Test Scripts)
    │
    ├── 📂 UI                     # UI 로직 및 관리
    │   ├── ConditionUI.cs
    │   ├── PlayerStatusUI.cs
    │   └── UIManager.cs
    │
    └── 📂 Utils                  # 프로젝트 전반에서 사용되는 유틸리티
        ├── Constants.cs
        ├── Enum.cs
        ├── RuntimeInitializer.cs
        └── SceneLoader.cs

## 5. 조작법

<img width="378" height="374" alt="image" src="https://github.com/user-attachments/assets/b791bc43-4e51-458a-ab34-dfb672a57acf" />


<br>

## 6. 기능 소개

<img width="2197" height="1270" alt="image" src="https://github.com/user-attachments/assets/c8e4f526-5ee8-4ad8-8064-a80e48bd0bc0" />
<img width="2534" height="1270" alt="image" src="https://github.com/user-attachments/assets/62e55e1c-0305-4bd2-a6ab-19f633d6afc0" />
<img width="2525" height="1285" alt="image" src="https://github.com/user-attachments/assets/413c105c-e3c0-4769-b655-6a0fa44ff29f" />
<img width="2548" height="1265" alt="image" src="https://github.com/user-attachments/assets/c249e632-ff38-4fbe-93a3-dd22be7eb38c" />
<img width="2547" height="1283" alt="image" src="https://github.com/user-attachments/assets/e0b6874c-43f4-421e-a5f6-7bb244f388d3" />
<img width="2537" height="1279" alt="image" src="https://github.com/user-attachments/assets/73ac4d95-00bc-4e1b-9c9e-1db351d14b07" />
<img width="2543" height="1284" alt="image" src="https://github.com/user-attachments/assets/0cf63ce6-9f6c-4b7f-baf5-d2306351c44c" />


## 7. 플레이영상

https://youtu.be/XqZdjBzFv14

## 8. 사용 스택
C#
Rider, VisualStudio, Unity 2022
NavMesh
