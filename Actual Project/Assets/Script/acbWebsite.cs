using UnityEngine;

public class acbWebsite : MonoBehaviour
{
    public string url;
    
    public void OpenURL() {
        Application.OpenURL(url);
    }
}
