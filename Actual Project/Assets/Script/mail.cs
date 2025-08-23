using UnityEngine;

public class mail : MonoBehaviour
{
    public GameObject popup;

    private string emailAddress = "info.bmr@acb.gov.bn";

    public void ShowPopup()
    {
        popup.SetActive(true);
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

    public void Inquiry()
    {
        string mailto = $"mailto:{emailAddress}";
        Application.OpenURL(mailto);
        HidePopup();
    }
    public void Report()
    {
        string subject = EscapeURL("Corruption Report");
        string body = EscapeURL(
            "Location, Date and Time of Incident: \n" +
            "Evidence (Document, Picture, Video/Voice Recording): \n\n" +
            "How do you know the information? As a witness? victim? middle person?: \n" +
            "Type of corruption/gratification (Money, Goods, Service, Discount etc): \n\n" +

            "Details of person involved\n" +
            "Name: \n" +
            "Address: \n" +
            "Position: \n" +
            "Phone Number: \n" +
            "IC: "
        );

        string mailto = $"mailto:{emailAddress}?subject={subject}&body={body}";
        Application.OpenURL(mailto);
        HidePopup();
    }

    string EscapeURL(string text)
    {
        return WWW.EscapeURL(text).Replace("+", "%20");
    }
}
