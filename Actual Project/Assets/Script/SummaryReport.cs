using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class summaryReport : MonoBehaviour
{
    public Text summaryText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string summary = $"Subject: {ReportData.Instance.subject}\n" + $"Incident: {ReportData.Instance.incidentDetails}\n" +
        $"People Involved: {ReportData.Instance.peopleInvolved}\n\n" +
        $"Name: {ReportData.Instance.reporterName}\n" + $"Email: {ReportData.Instance.email}\n" +
        $"Phone Number: {ReportData.Instance.number}\n" + $"IC: {ReportData.Instance.ic}";

        summaryText.text = summary;
    }

    public void Submit(string sceneName)
    {
        ReportData.Instance.Clear();
        SceneManager.LoadScene(sceneName);
    }
}
