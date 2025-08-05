using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int lettersPerSecond;

    public event Action OnShowDialog;
    public event Action OnCloseDialog;

    public static DialogueManager Instance { get; private set; }

    List<string> fullDialog;
    int currentLine;
    int endLine;
    bool isTyping;

    private void Awake()
    {
        Instance = this;
    }

    public IEnumerator ShowDialog(List<string> lines, int startIndex, int count)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        fullDialog = lines;
        currentLine = startIndex;
        endLine = Mathf.Min(startIndex + count, fullDialog.Count);

        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(fullDialog[currentLine]));
    }

    void Update()
    {
        HandleUpdate();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            currentLine++;
            if (currentLine < endLine)
            {
                StartCoroutine(TypeDialog(fullDialog[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                OnCloseDialog?.Invoke();
            }
        }
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
        isTyping = false;
    }
}
