using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using UnityEngine.EventSystems;
using System.Linq;

public class infoReceived : MonoBehaviour, IPointerClickHandler
{
    public string reportURL = "http://localhost/hackathon/get_report.php";
    public Text incidentText;
    public Text peopleText;
    public Text corruptionText;

    public GameObject detailPopup;
    public Text detailText;
    public InputField searchInput;

    private List<Report> currentReports;
    private List<Report> allReports;
    private List<string> evidenceLink = new List<string>();

    [System.Serializable]
    public class Report
    {
        public string id;
        public string subject;
        public string incident;
        public string info;
        public string corruption;
        public string people;
        public string peopleAddress;
        public string position;
        public string peopleNo;
        public string peopleIc;
        public string evidence;
    }

    [System.Serializable]
    public class ReportList
    {
        public List<Report> reports;
    }

    void Start()
    {
        StartCoroutine(LoadReports());
    }

    public void OnSearch()
    {
        string query = searchInput.text.Trim().ToLower();

        if (string.IsNullOrEmpty(query))
        {
            DisplayReport(allReports);
            return;
        }

        List<Report> filtered = allReports
        .Where(report => report.incident.ToLower().Contains(query) || report.people.ToLower().Contains(query) || report.corruption.ToLower().Contains(query))
        .ToList();

        DisplayReport(filtered);
    }

    IEnumerator LoadReports()
    {
        UnityWebRequest www = UnityWebRequest.Get(reportURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"reports\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            ReportList reportList = JsonUtility.FromJson<ReportList>(json);

            allReports = reportList.reports;
            DisplayReport(allReports);
        }
        else
        {
            Debug.LogError("Error: " + www.error);
            incidentText.text = "error loading report";
        }
    }

    void DisplayReport(List<Report> reports)
    {
        currentReports = reports;
        incidentText.text = "";
        peopleText.text = "";
        corruptionText.text = "";

        foreach (Report report in reports)
        {
            incidentText.text += report.incident + "\n";
            peopleText.text += report.people + "\n";
            corruptionText.text += report.corruption + "\n";
        }

        if (reports.Count == 0)
        {
            incidentText.text = "No report Received";
            peopleText.text = "";
            corruptionText.text = "";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (detailPopup.activeSelf)
        {
            Vector2 localMousePos;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                detailText.rectTransform,
                eventData.position,
                eventData.pressEventCamera,
                out localMousePos
            );

            float pivotOffsetY = detailText.rectTransform.rect.height * detailText.rectTransform.pivot.y;
            float yFromTop = pivotOffsetY - localMousePos.y;

            float lineHeight = detailText.fontSize * detailText.lineSpacing;
            int clickedLine = Mathf.FloorToInt(yFromTop / lineHeight);

            Debug.Log($"Clicked detail line: {clickedLine}");

            string[] lines = detailText.text.Split('\n');
            int evidenceStartLine = System.Array.IndexOf(lines, "Evidence File(s):") + 1;

            int evidenceIndex = clickedLine - evidenceStartLine;

            if (evidenceIndex >= 0 && evidenceIndex < evidenceLink.Count)
            {
                Debug.Log($"Opening link: {evidenceLink[evidenceIndex]}");
                Application.OpenURL(evidenceLink[evidenceIndex]);
                return;
            }
        }

        Vector2 localMousePosIncident;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            incidentText.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out localMousePosIncident
        );

        float pivotOffsetYIncident = incidentText.rectTransform.rect.height * incidentText.rectTransform.pivot.y;

        float yFromTopIncident = pivotOffsetYIncident - localMousePosIncident.y;

        float lineHeightIncident = incidentText.fontSize * incidentText.lineSpacing;

        int clickedIndex = Mathf.FloorToInt(yFromTopIncident / lineHeightIncident);

        Debug.Log($"Clicked line: {clickedIndex}");

        if (clickedIndex >= 0 && clickedIndex < currentReports.Count)
        {
            Report selectedReport = currentReports[clickedIndex];
            ShowDetail(selectedReport);
        }
    }

    void ShowDetail(Report report)
    {
        detailPopup.SetActive(true);

        string baseURL = "http://localhost/hackathon/";

        string detailInfo =
        $"Subject: {report.subject}\n" +
        $"Incident: {report.incident}\n" +
        $"Source of Info: {report.info}\n" +
        $"Type of Corruption: {report.corruption}\n\n" +
        $"People Involved: {report.people}\n" +
        $"Address: {report.peopleAddress}\n" +
        $"Position: {report.position}\n" +
        $"Number: {report.peopleNo}\n" +
        $"IC: {report.peopleIc}\n\n" +

        $"Evidence File(s):\n";

        if (!string.IsNullOrEmpty(report.evidence))
        {
            string[] evidenceFiles = report.evidence.Split(',');

            for (int i = 0; i < evidenceFiles.Length; i++)
            {
                string filename = evidenceFiles[i].Trim();
                if (!string.IsNullOrEmpty(filename))
                {
                    string fullUrl = baseURL + filename;
                    evidenceLink.Add(fullUrl);
                    detailInfo += $"{i + 1}. {filename}\n";
                }
            }
        }
        else
        {
            detailInfo += "No evidence files submitted.";
        }

        detailText.text = detailInfo;
    }

    public void ClosePopup()
    {
        detailPopup.SetActive(false);
    }
}
