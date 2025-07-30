using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class reporting : MonoBehaviour
{
    public InputField subjectInput;
    public InputField incidentDetailsInput;
    public InputField peopleInvolvedInput;
    public Text errorText;
    public Button next;
    public InputValidation inputValidation;

    public void Start()
    {
        if (errorText != null)
        {
            errorText.text = "";
        }

        // Pre-fill inputs from saved data
        if (subjectInput != null)
            subjectInput.text = ReportData.Instance.subject;

        if (incidentDetailsInput != null)
            incidentDetailsInput.text = ReportData.Instance.incidentDetails;

        if (peopleInvolvedInput != null)
            peopleInvolvedInput.text = ReportData.Instance.peopleInvolved;
    }

    public void MoveScene(string sceneName)
    {
        bool isValid = ValidateInputs();

        if (subjectInput != null)
            ReportData.Instance.subject = subjectInput.text;

        if (incidentDetailsInput != null)
            ReportData.Instance.incidentDetails = incidentDetailsInput.text;

        if (peopleInvolvedInput != null)
            ReportData.Instance.peopleInvolved = peopleInvolvedInput.text;

        if (sceneName.Contains("Report") && !isValid)
            return;

        SceneManager.LoadScene(sceneName);
    }

    private bool ValidateInputs()
    {
        if (errorText != null)
            errorText.text = "";

        bool isValid = true;

        if (string.IsNullOrWhiteSpace(subjectInput.text))
        {
            errorText.text = "Subject is required";
            isValid = false;
        }
        else if (string.IsNullOrWhiteSpace(incidentDetailsInput.text))
        {
            errorText.text = "Incident is required";
            isValid = false;
        }
        else if (string.IsNullOrWhiteSpace(peopleInvolvedInput.text))
        {
            errorText.text = "People Involved is required";
            isValid = false;
        }

        return isValid;
    }
}
