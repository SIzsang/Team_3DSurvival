using _02_Scripts.Narrative.Data;
using _02_Scripts.Narrative.Entities;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _02_Scripts.Narrative
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        [SerializeField] private GameObject dialoguePanel;
        [SerializeField] private TextMeshProUGUI speakerName;
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private GameObject nextIndicator;
        [SerializeField] private float typingSpeed = 0.04f;

        private Queue<Dialogue> _dialogueQueue;
        private Dialogue _currentDialogue;
        private Coroutine _typingCoroutine;
        private string _currentSentence;
        private bool _isTyping;
        private bool _isDialogueActive;

        public bool IsDialogueActive => _isDialogueActive;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _dialogueQueue = new Queue<Dialogue>();
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (!dialoguePanel.activeInHierarchy) return;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                HandleContinue();
            }
        }

        /// <summary>
        /// 지정된 스토리 객체를 기반으로 새로운 대화를 시작합니다.
        /// 이미 대화가 진행 중인 경우, 새로운 대화를 시작하지 않고 즉시 반환합니다.
        /// 이 메소드는 대화 UI를 활성화하고, 내부 상태를 초기화한 후, 스토리에 포함된 모든 대사들을 큐에 추가합니다.
        /// 마지막으로, 큐의 첫 번째 대사를 화면에 표시하여 대화를 개시합니다.
        /// </summary>
        /// <param name="story">화면에 표시할 대화 라인들의 컬렉션을 담고 있는 스토리 객체입니다.</param>
        /// <remarks>
        /// 이 메소드는 대화가 끝나는 것을 기다리지 않습니다. 대화의 시작만 담당하며,
        /// 실제 진행은 DisplayNextLine과 사용자 입력에 의해 처리됩니다.
        /// </remarks>
        public void StartDialogue(Story story)
        {
            if (_isDialogueActive) return;
            dialoguePanel.SetActive(true);
            _dialogueQueue.Clear();
            _currentDialogue = null;
            _isDialogueActive = true;
            foreach (Dialogue dialogue in story.Dialogues)
            {
                _dialogueQueue.Enqueue(dialogue);
            }

            DisplayNextLine();
        }

        /// <summary>
        /// 새로운 대화를 시작하고 UI를 표시합니다.
        /// </summary>
        /// <param name="sentence">화면에 표시할 대화 내용이 담긴 문자열입니다.</param>
        /// <remarks>
        /// 이미 대화가 진행 중인 경우 아무 작업도 수행하지 않고 즉시 반환됩니다.
        /// 이 메소드는 대화의 시작만 담당하며, 실제 대화 흐름은 DisplayNextLine 메소드와 사용자 입력에 의해 제어됩니다.
        /// </remarks>
        public void StartDialogue(string sentence)
        {
            if (_isDialogueActive) return;
            dialoguePanel.SetActive(true);
            _dialogueQueue.Clear();
            _currentDialogue = null;
            _dialogueQueue.Enqueue(new Dialogue(sentence));
            _isDialogueActive = true;
            DisplayNextLine();
        }

        /// <summary>
        /// 새로운 대화를 시작하고 UI를 표시합니다.
        /// </summary>
        /// <param name="dialogue">화면에 표시할 대화 내용이 담긴 Dialogue 객체입니다.</param>
        /// <remarks>
        /// 이미 대화가 진행 중인 경우 아무 작업도 수행하지 않고 즉시 반환됩니다.
        /// 이 메소드는 대화의 시작만 담당하며, 실제 대화 흐름은 DisplayNextLine 메소드와 사용자 입력에 의해 제어됩니다.
        /// </remarks>
        public void StartDialogue(DialogueData dialogue)
        {
            if (_isDialogueActive) return;
            dialoguePanel.SetActive(true);
            _dialogueQueue.Clear();
            _currentDialogue = null;
            _dialogueQueue.Enqueue(new Dialogue(dialogue));
            _isDialogueActive = true;
            DisplayNextLine();
        }

        private void DisplayNextLine()
        {
            if (_currentDialogue != null && _currentDialogue.IsFinished())
            {
                _currentDialogue = null;
            }

            if (_currentDialogue == null)
            {
                if (_dialogueQueue.Count > 0)
                {
                    _currentDialogue = _dialogueQueue.Dequeue();
                }
                else
                {
                    EndDialogue();
                    return;
                }
            }

            DialogueLine line = _currentDialogue.GetNextLine();

            if (line == null)
            {
                DisplayNextLine();
                return;
            }

            speakerName.text = line.speakerName;
            _currentSentence = line.text;

            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
            }
            _typingCoroutine = StartCoroutine(TypeSentence(_currentSentence));
        }

        private IEnumerator TypeSentence(string sentence)
        {
            _isTyping = true;
            nextIndicator.SetActive(false);
            dialogueText.text = "";

            foreach (char letter in sentence)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(typingSpeed);
            }

            _isTyping = false;
            nextIndicator.SetActive(true);
        }

        private void HandleContinue()
        {
            if (_isTyping)
            {
                if (_typingCoroutine != null)
                {
                    StopCoroutine(_typingCoroutine);
                }
                dialogueText.text = _currentSentence;
                _isTyping = false;
                nextIndicator.SetActive(true);
            }
            else
            {
                DisplayNextLine();
            }
        }

        private void EndDialogue()
        {
            if (_typingCoroutine != null)
            {
                StopCoroutine(_typingCoroutine);
                _typingCoroutine = null;
            }
            dialoguePanel.SetActive(false);
            nextIndicator.SetActive(false);
            dialogueText.text = "";
            speakerName.text = "";
            _currentDialogue = null;
            _isDialogueActive = false;
        }
    }
}