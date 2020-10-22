using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc : MonoBehaviour
{
    private Dialogue dialogueCS;

    string name;
    string dialogue;
    public int npcNum;

    private void Awake()
    {
       
    }

    public void chat(GameObject dialogue)
    {
        dialogueCS = dialogue.GetComponent<Dialogue>();
        dialogueCS.Dialoguing(npcNum);
    }
}
