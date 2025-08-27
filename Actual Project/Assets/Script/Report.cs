using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;
using System.IO;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
    using UnityEditor;
#endif

public class Report : MonoBehaviour
{
    public InputField subjectInput;
    public InputField incidentDetailsInput;
    public InputField infoSourceInput;
    public InputField corruptionTypeInput;
    public InputField peopleInvolvedInput;
    public InputField addressInput;
    public InputField positionInput;
    public InputField numberInput;
    public InputField icInput;
    public Text selectedFileText;

    public InputValidation inputValidation;

    public string dbURL = "http://localhost/hackathon/insert_report.php";
    private List<string> selectedFilePaths = new List<string>();

    public static Report Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Start()
    {
        if (subjectInput != null)
            subjectInput.text = ReportData.Instance.subject;

        if (incidentDetailsInput != null)
            incidentDetailsInput.text = ReportData.Instance.incidentDetails;

        if (infoSourceInput != null)
            infoSourceInput.text = ReportData.Instance.InfoSource;

        if (corruptionTypeInput != null)
            corruptionTypeInput.text = ReportData.Instance.corruptionType;

        if (peopleInvolvedInput != null)
            peopleInvolvedInput.text = ReportData.Instance.peopleInvolvedName;

        if (addressInput != null)
            addressInput.text = ReportData.Instance.address;

        if (positionInput != null)
            positionInput.text = ReportData.Instance.position;

        if (numberInput != null)
            numberInput.text = ReportData.Instance.number;

        if (icInput != null)
            icInput.text = ReportData.Instance.ic;

        UpdateSelectedFile();
    }

    public void MoveScene(string sceneName)
    {
        if (subjectInput != null)
            ReportData.Instance.subject = subjectInput.text;

        if (incidentDetailsInput != null)
            ReportData.Instance.incidentDetails = incidentDetailsInput.text;

        if (infoSourceInput != null)
            ReportData.Instance.InfoSource = infoSourceInput.text;

        if (corruptionTypeInput != null)
            ReportData.Instance.corruptionType = corruptionTypeInput.text;

        if (peopleInvolvedInput != null)
            ReportData.Instance.peopleInvolvedName = peopleInvolvedInput.text;

        if (addressInput != null)
            ReportData.Instance.address = addressInput.text;

        if (positionInput != null)
            ReportData.Instance.position = positionInput.text;

        if (numberInput != null)
            ReportData.Instance.number = numberInput.text;

        if (icInput != null)
            ReportData.Instance.ic = icInput.text;

        if (sceneName.Contains("Report"))
        {
            if (inputValidation != null && !inputValidation.Validate())
                return;
        }

        SceneManager.LoadScene(sceneName);
    }

    public IEnumerator SendReport(string dbURL, string sceneName)
    {
        if (!selectedFilePaths.Any())
        {
            selectedFileText.color = Color.red;
            selectedFileText.text = "Please attach at least one file.";
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("subject", ReportData.Instance.subject);
        form.AddField("incident", ReportData.Instance.incidentDetails);
        form.AddField("info", ReportData.Instance.InfoSource);
        form.AddField("type", ReportData.Instance.corruptionType);

        form.AddField("name", ReportData.Instance.peopleInvolvedName);
        form.AddField("address", ReportData.Instance.address);
        form.AddField("position", ReportData.Instance.position);

        // if (!string.IsNullOrWhiteSpace(ReportData.Instance.peopleInvolvedName))
        //     form.AddField("name", ReportData.Instance.peopleInvolvedName);
        // else
        //     form.AddField("name", "");

        // if (!string.IsNullOrWhiteSpace(ReportData.Instance.address))
        //     form.AddField("address", ReportData.Instance.address);
        // else
        //     form.AddField("address", "");

        // if (!string.IsNullOrWhiteSpace(ReportData.Instance.position))
        //     form.AddField("position", ReportData.Instance.position);
        // else
        //     form.AddField("position", "");

        // if (!string.IsNullOrWhiteSpace(ReportData.Instance.address))
        //     form.AddField("email", ReportData.Instance.address);
        // else
        //     form.AddField("email", "");

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.number))
            form.AddField("number", ReportData.Instance.number);
        else
            form.AddField("number", "");

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.ic))
            form.AddField("ic", ReportData.Instance.ic);
        else
            form.AddField("ic", "");

        for (int i = 0; i < selectedFilePaths.Count; i++)
        {
            string path = selectedFilePaths[i];
            if (File.Exists(path))
            {
                byte[] data = File.ReadAllBytes(path);
                string filename = Path.GetFileName(path);
                form.AddBinaryData($"evidence{i}", data, filename);
            }
        }

        UnityWebRequest www = UnityWebRequest.Post(dbURL, form);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Report submitted");
            ReportData.Instance.Clear();
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Error submitting: " + www.error);
        }
    }

    public void PickEvidence()
    {
#if UNITY_EDITOR
        string path = EditorUtility.OpenFilePanel("Select Evidence File", "", "");
        if (!string.IsNullOrEmpty(path) && File.Exists(path))
        {
            if (!selectedFilePaths.Contains(path))
                selectedFilePaths.Add(path);
        }
        UpdateSelectedFile();
#else
            Debug.LogWarning("File picking is only supported in the Unity Editor. For mobile/desktop builds, implement platform-specific file pickers.");
#endif
    }

    public void UpdateSelectedFile()
    {
        if (selectedFilePaths.Count == 0)
        {
            selectedFileText.text = "No files selected";
            selectedFileText.color = Color.red;
        }
        else
        {
            selectedFileText.text = string.Join("\n", selectedFilePaths.Select(p => Path.GetFileName(p)));
            selectedFileText.color = Color.black;
        }
    }
}
