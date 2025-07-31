using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using System.Collections;

public class Report : MonoBehaviour
{
    public InputField subjectInput;
    public InputField incidentDetailsInput;
    public InputField peopleInvolvedInput;
    public InputField reporterNameInput;
    public InputField emailInput;
    public InputField numberInput;
    public InputField icInput;

    public InputValidation inputValidation;

    public string dbURL = "http://localhost/hackathon/insert_report.php";

    public void Start()
    {
        if (subjectInput != null)
            subjectInput.text = ReportData.Instance.subject;

        if (incidentDetailsInput != null)
            incidentDetailsInput.text = ReportData.Instance.incidentDetails;

        if (peopleInvolvedInput != null)
            peopleInvolvedInput.text = ReportData.Instance.peopleInvolved;

        if (reporterNameInput != null)
            reporterNameInput.text = ReportData.Instance.reporterName;

        if (emailInput != null)
            emailInput.text = ReportData.Instance.email;

        if (numberInput != null)
            numberInput.text = ReportData.Instance.number;

        if (icInput != null)
            icInput.text = ReportData.Instance.ic;
    }

    public void MoveScene(string sceneName)
    {
        if (subjectInput != null)
            ReportData.Instance.subject = subjectInput.text;

        if (incidentDetailsInput != null)
            ReportData.Instance.incidentDetails = incidentDetailsInput.text;

        if (peopleInvolvedInput != null)
            ReportData.Instance.peopleInvolved = peopleInvolvedInput.text;

        if (reporterNameInput != null)
            ReportData.Instance.reporterName = reporterNameInput.text;

        if (emailInput != null)
            ReportData.Instance.email = emailInput.text;

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

    public static IEnumerator SendReport(string dbURL, string sceneName)
    {
        WWWForm form = new WWWForm();
        form.AddField("subject", ReportData.Instance.subject);
        form.AddField("incident", ReportData.Instance.incidentDetails);
        form.AddField("people", ReportData.Instance.peopleInvolved);

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.reporterName))
            form.AddField("name", ReportData.Instance.reporterName);
        else
            form.AddField("name", "");

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.email))
            form.AddField("email", ReportData.Instance.email);
        else
            form.AddField("email", "");

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.number))
            form.AddField("number", ReportData.Instance.number);
        else
            form.AddField("number", "");

        if (!string.IsNullOrWhiteSpace(ReportData.Instance.ic))
            form.AddField("ic", ReportData.Instance.ic);
        else
            form.AddField("ic", "");

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
}
