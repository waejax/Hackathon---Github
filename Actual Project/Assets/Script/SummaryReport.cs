using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class summaryReport : MonoBehaviour
{
    public Text summaryText;
    // private string emailAddress = "info.bmr@acb.gov.bn";
    public string dbURL = "http://localhost/hackathon/insert_report.php";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string summary = $"Subject: {ReportData.Instance.subject}\n" + $"Incident: {ReportData.Instance.incidentDetails}\n" +
        $"Source of Info: {ReportData.Instance.InfoSource}\n" +
        $"Type of Corruption: {ReportData.Instance.corruptionType}\n\n" +
        $"Name: {ReportData.Instance.peopleInvolvedName}\n" + $"Address: {ReportData.Instance.address}\n" +
        $"Position: {ReportData.Instance.position}\n" +
        $"Phone Number: {ReportData.Instance.number}\n" + $"IC: {ReportData.Instance.ic}";

        summaryText.text = summary;
    }

    public void Submit(string sceneName)
    {
        Report report = FindObjectOfType<Report>();

        report.StartCoroutine(report.SendReport(dbURL, sceneName));

        // string subject = EscapeURL("Corruption Report");

        // string body = EscapeURL(
        //     $"Incident: {ReportData.Instance.incidentDetails}\n" +
        //     $"Source of Info: {ReportData.Instance.InfoSource}\n" +
        //     $"Type of Corruption: {ReportData.Instance.corruptionType}\n\n" +
        //     $"Name: {ReportData.Instance.peopleInvolvedName}\n" + $"Address: {ReportData.Instance.address}\n" +
        //     $"Position: {ReportData.Instance.position}\n" +
        //     $"Phone Number: {ReportData.Instance.number}\n" + $"IC: {ReportData.Instance.ic}\n" +
        //     "Evidence: "
        // );

        // string mailto = $"mailto:{emailAddress}?subject={subject}&body={body}";
        // Application.OpenURL(mailto);
    }

    // string EscapeURL(string text)
    // {
    //     return WWW.EscapeURL(text).Replace("+", "%20");
    // }
}
