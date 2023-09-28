using UnityEngine;

abstract public class Event : ScriptableObject
{
    [SerializeField, TextArea(1, 5)]
    string description;
}
