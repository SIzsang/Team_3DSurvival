using _02_Scripts.Narrative.Data;

namespace _02_Scripts.Narrative.Entities
{
    public class Dialogue
    {
        private readonly DialogueData _data;
        private int _currentLineIndex = -1;
        private DialogueLine _singleSentence;

        // public bool IsFinished => _currentLineIndex >= _data.dialogueLines.Count - 1;

        public Dialogue(DialogueData dialogueData)
        {
            _data = dialogueData;
        }

        public Dialogue(string singleSentence)
        {
            _singleSentence = new DialogueLine();
            _singleSentence.text = singleSentence;
        }

        public bool IsFinished()
        {
            if (_currentLineIndex != -1)
            {
                return _currentLineIndex >= _data.dialogueLines.Count - 1;
            }
            else
            {
                return true;
            }
        }


        public DialogueLine GetNextLine()
        {
            if (_data == null && _singleSentence != null)
            {
                return _singleSentence;
            }
            _currentLineIndex++;

            if (_currentLineIndex >= _data.dialogueLines.Count)
            {
                return null;
            }

            return _data.dialogueLines[_currentLineIndex];
        }
    }
}