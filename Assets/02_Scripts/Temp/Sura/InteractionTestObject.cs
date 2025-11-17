using System.Collections;
using System.Collections.Generic;
using _02_Scripts.Narrative;
using UnityEngine;

public class InteractionTestObject : MonoBehaviour, IInteractable
{
    [SerializeField] private string testString;
    public void OnInteract()
    {
        Debug.Log(testString);
    }
}
