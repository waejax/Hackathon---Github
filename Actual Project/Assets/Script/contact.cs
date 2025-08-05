using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class contact : MonoBehaviour
{
    public Text bandar;
    public Text kb;

    public string dbURL = "http://localhost/hackathon/contact.php";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(dbURL);
        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string json = www.downloadHandler.text;
            Debug.Log("Json from php: " + json);

            json = "{\"contacts\":" + json + "}";
            Debug.Log("wrraped json: " + json);

            ContactList contactList = JsonUtility.FromJson<ContactList>(json);

            foreach (ContactData contact in contactList.contacts) {
                if (contact.name.Contains("HQ"))
                    bandar.text = "Address: " + contact.address + "\nEnquiries: " + contact.Qphone1 + "\t/ " + contact.Qphone2 + "\nReporting Phone: " + contact.Rphone1 + "\t/ " + contact.Rphone2 + "\nLocation: ";
                else if (contact.name.Contains("Kuala Belait"))
                    kb.text = "Address: " + contact.address + "\nEnquiries: " + contact.Qphone1 + "\nReporting Phone: " + contact.Rphone1 + "\t/ " + contact.Rphone2 + "\nLocation: ";
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }

    [System.Serializable]
    public class ContactData {
        public string name, address, Qphone1, Qphone2, Rphone1, Rphone2;
    }

    [System.Serializable]
    public class ContactList {
        public ContactData[] contacts;
    }
}
