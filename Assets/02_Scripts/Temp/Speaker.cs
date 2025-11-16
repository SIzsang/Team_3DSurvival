using UnityEngine;

namespace _02_Scripts.Temp
{
    [CreateAssetMenu(fileName = "Temp New Character", menuName = "Scriptable Objects/Temp Character")]
    public class Character : ScriptableObject
    {
        public string characterName;
    }
}