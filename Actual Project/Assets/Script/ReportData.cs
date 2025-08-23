using UnityEngine;

public class ReportData : MonoBehaviour
{
    public static ReportData Instance;
    public string subject;
    public string incidentDetails;
    public string InfoSource;
    public string corruptionType;
    public string peopleInvolvedName;
    public string address;
    public string position;
    public string number;
    public string ic;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Clear()
    {
        subject = "";
        incidentDetails = "";
        InfoSource = "";
        corruptionType = "";
        peopleInvolvedName = "";
        address = "";
        position = "";
        number = "";
        ic = "";
    }
}
