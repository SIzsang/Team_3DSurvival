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

        private void DisplayNextLine()
        {
            if (_currentDialogue != null && _currentDialogue.IsFinished)
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

            speakerName.text = line.SpeakerName;
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