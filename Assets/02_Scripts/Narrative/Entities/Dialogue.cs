using _02_Scripts.Narrative.Data;

namespace _02_Scripts.Narrative.Entities
{
    public class Dialogue
    {
        private readonly DialogueData _data;
        private int _currentLineIndex = -1;

        public bool IsFinished => _currentLineIndex >= _data.dialogueLines.Count - 1;

        public Dialogue(DialogueData dialogueData)
        {
            _data = dialogueData;
        }

        public DialogueLine GetNextLine()
        {
            _currentLineIndex++;

            if (_currentLineIndex >= _data.dialogueLines.Count)
            {
                return null;
            }

            return _data.dialogueLines[_currentLineIndex];
        }
    }
}