# Team_3DSurvival

<img width="295" height="306" alt="image" src="https://github.com/user-attachments/assets/d11a8172-4b43-48b4-b9b0-8fffa4ae3b5f" />


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

    <img width="809" height="701" alt="image" src="https://github.com/user-attachments/assets/cb6cab28-04a8-4f82-a3a8-e3ef25a3e5f9" />
