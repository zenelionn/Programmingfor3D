using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Dialogue/Dialogue")]
public class Dialogue : ScriptableObject
{
    [TextArea(3, 10)]  // Makes it easier to edit in the Inspector
    public string[] sentences;  // Array of sentences for the dialogue
}
