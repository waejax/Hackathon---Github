using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class heatmap : MonoBehaviour
{
    [Header("Map Settings")]
    public RectTransform mapPanel;
    public GameObject heatPointPrefab;

    [Header("Report Data Source")]
    public string reportURL = "http://localhost/hackathon/get_report.php";

    private Dictionary<string, Vector2> locationMap = new Dictionary<string, Vector2>();
    private Dictionary<string, int> heatCounts = new Dictionary<string, int>();

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
        InitLocationMap();
        StartCoroutine(LoadReports());
    }

    void InitLocationMap()
    {
        locationMap["bsb"] = new Vector2(51, 63);
        locationMap["kg tungku"] = new Vector2(45, 75);
        locationMap["kg kiulap"] = new Vector2(65, 70);
        locationMap["kg rimba"] = new Vector2(56, 79);
        locationMap["bangar"] = new Vector2(97, 39);
        locationMap["tutong"] = new Vector2(-11, 44);
        locationMap["seria"] = new Vector2(-82, 10);
        locationMap["kb"] = new Vector2(-112, 7);
    }

    IEnumerator LoadReports()
    {
        UnityWebRequest www = UnityWebRequest.Get(reportURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            json = "{\"reports\":" + json + "}";

            ReportList reportList = JsonUtility.FromJson<ReportList>(json);
            CountReportsByLocation(reportList.reports);
            SpawnHeatPoints();
        }
        else
        {
            Debug.LogError("Error fetching reports: " + www.error);
        }
    }

    void CountReportsByLocation(List<Report> reports)
    {
        foreach (Report r in reports)
        {
            string location = NormalizeLocation(r.incident);

            if (locationMap.ContainsKey(location))
            {
                if (!heatCounts.ContainsKey(location))
                    heatCounts[location] = 0;

                heatCounts[location]++;
            }
            else
            {
                Debug.LogWarning("Unknown location: " + r.incident);
            }
        }
    }

    void SpawnHeatPoints()
    {
        foreach (var entry in heatCounts)
        {
            string location = entry.Key;
            int count = entry.Value;

            Vector2 pos = locationMap[location];

            GameObject point = Instantiate(heatPointPrefab, mapPanel);
            point.GetComponent<RectTransform>().anchoredPosition = pos;

            Image img = point.GetComponent<Image>();
            float intensity = Mathf.Clamp01(count / 5f);
            img.color = Color.Lerp(Color.yellow, Color.red, intensity);

            point.GetComponent<RectTransform>().sizeDelta = Vector2.one * (10 + intensity * 20);
        }
    }

    string NormalizeLocation(string raw)
    {
        string loc = raw.ToLower().Trim();

        if (loc.Contains("kiulap"))
            return "kg kiulap";
        if (loc.Contains("tungku"))
            return "kg tungku";
        if (loc.Contains("rimba"))
            return "kg rimba";
        if (loc.Contains("bangar"))
            return "bangar";
        if (loc.Contains("tutong"))
            return "tutong";
        if (loc.Contains("seria"))
            return "seria";
        if (loc.Contains("kuala belait") || loc.Contains("kb"))
            return "kb";
        if (loc.Contains("bsb") || loc.Contains("bandar") || loc.Contains("bandar seri begawan"))
            return "bsb";

        return loc;
    }
}
