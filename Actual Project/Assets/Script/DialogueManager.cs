using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] Text continueText;
    [SerializeField] int lettersPerSecond;
    [SerializeField] private Transform playerTransform;
    private Vector2 originalSize;
    private Vector2 infoDialogSize = new Vector2(500, 200);
    private RectTransform dialogRectTransform;
    private Vector3 originalPos;

    private RectTransform continueTextRect;
    private Vector3 originalContinuePos;
    // private Vector3 infoContinuePos = new Vector3(500, -20, 0);
    // private Vector2 demoDialogSize;
    // private Vector3 demoDialogPos;

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

        dialogRectTransform = dialogBox.GetComponent<RectTransform>();

        continueTextRect = continueText.GetComponent<RectTransform>();

        // demoDialogSize = originalSize;
        // demoDialogPos = playerTransform.position + new Vector3(-12f, 0, 0);
    }

    private void Start()
    {
        originalSize = dialogRectTransform.sizeDelta;
        originalPos = dialogRectTransform.localPosition;

        originalContinuePos = continueTextRect.localPosition;
    }

    public IEnumerator ShowDialog(List<string> lines, int startIndex, int count, bool isInfoDialogue = false, Action onDialogComplete = null)
    {
        yield return new WaitForEndOfFrame();

        OnShowDialog?.Invoke();

        fullDialog = lines;
        currentLine = startIndex;
        endLine = Mathf.Min(startIndex + count, fullDialog.Count);

        string currentScene = SceneManager.GetActiveScene().name;

        if (isInfoDialogue)
        {
            dialogRectTransform.sizeDelta = infoDialogSize;
            continueTextRect.localPosition = originalContinuePos;

            // if (currentScene == "GameDemo" || currentScene == "PrimaryLevelEvidence")
            // {
            //     dialogRectTransform.anchorMin = new Vector2(0.5f, 0.5f);
            //     dialogRectTransform.anchorMax = new Vector2(0.5f, 0.5f);
            //     dialogRectTransform.pivot = new Vector2(0.5f, 0.5f);
            //     dialogRectTransform.anchoredPosition = Vector2.zero;

            //     continueTextRect.anchorMin = new Vector2(1f, 0f);
            //     continueTextRect.anchorMax = new Vector2(1f, 0f);
            //     continueTextRect.pivot = new Vector2(1f, 0f);
            //     continueTextRect.anchoredPosition = new Vector2(-20, 10);
            // }
            // else
            // {
            //     Vector3 worldPos = playerTransform.position + new Vector3(-15.5f, -2f, 0);
            //     //Vector3 worldPos = playerTransform.position + new Vector3(0f, 1f, 0);
            //     Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPos);

            //     Vector2 localPoint;
            //     RectTransformUtility.ScreenPointToLocalPointInRectangle(dialogRectTransform.parent as RectTransform, screenPos, null, out localPoint);

            //     dialogRectTransform.localPosition = localPoint;

            //     continueTextRect.localPosition = new Vector3(infoDialogSize.x / 2 - 10, 10, 0);
            // }
        }
        else
        {
            dialogRectTransform.sizeDelta = originalSize;

            // if (currentScene == "GameDemo" || currentScene == "PrimaryLevelEvidence")
            // {
            //     dialogRectTransform.anchorMin = new Vector2(0.5f, 0);
            //     dialogRectTransform.anchorMax = new Vector2(0.5f, 0);
            //     dialogRectTransform.pivot = new Vector2(0, 0);
            //     dialogRectTransform.anchoredPosition = new Vector2(-83.9084f, 193.89f);
            // }
            // else
            // {
            //     dialogBox.transform.localPosition = originalPos;
            // }

            continueTextRect.localPosition = originalContinuePos;
        }

        dialogBox.SetActive(true);
        StartCoroutine(TypeDialog(fullDialog[currentLine]));

        while (dialogBox.activeSelf)
            yield return null;

        dialogRectTransform.sizeDelta = originalSize;
        dialogBox.transform.localPosition = originalPos;

        onDialogComplete?.Invoke();
    }

    void Update()
    {
        HandleUpdate();
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && !isTyping)
        {
            currentLine++;

            continueText.text = "";

            if (currentLine < endLine)
            {
                StartCoroutine(TypeDialog(fullDialog[currentLine]));
            }
            else
            {
                currentLine = 0;
                dialogBox.SetActive(false);
                continueText.text = "";
                OnCloseDialog?.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.E) && isTyping)
        {
            StopAllCoroutines();
            dialogText.text = fullDialog[currentLine];
            isTyping = false;
            continueText.text = "Press Z to continue";
        }
        
    }

    public IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        continueText.text = "";

        foreach (char letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }

        isTyping = false;
        yield return new WaitForSeconds(1f);
        continueText.text = "Press Z to continue";
    }
}
