using UnityEngine;
using UnityEngine.UI;

public class playerDetail : MonoBehaviour
{
    public Text emailText;
    public Text detailText;

    void Start()
    {
        emailText.text = PlayerPrefs.GetString("SelectedPlayerEmail", "Unknown");
        detailText.text = PlayerPrefs.GetString("SelectedPlayerScore", "0");
    }
}
