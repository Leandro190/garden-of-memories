using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    private Queue<string> sentences;
    [SerializeField] private Text nameText;
    [SerializeField] private Text diagText;
    [SerializeField] private GameObject diagBox;
    [SerializeField] private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        diagBox.SetActive(true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        else
        {
            string sentence = sentences.Dequeue();
            diagText.text = sentence;
        }

    }

    void EndDialogue()
    {
        diagBox.SetActive(false);
        player.FinishDialogue();
    }
}
