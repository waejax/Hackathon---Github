using UnityEngine;

public class ReportData : MonoBehaviour
{
    public static ReportData Instance;
    public string subject;
    public string incidentDetails;
    public string peopleInvolved;
    public string reporterName;
    public string email;
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
        peopleInvolved = "";
        reporterName = "";
        email = "";
        number = "";
        ic = "";
    }
}
