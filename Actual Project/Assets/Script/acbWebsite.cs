using UnityEngine;

public class acbWebsite : MonoBehaviour
{
    public string url = "https://www.acb.gov.bn/Theme/Home.aspx";
    
    public void OpenURL() {
        Application.OpenURL(url);
    }
}
