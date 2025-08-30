using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class playerDetail : MonoBehaviour
{
    public Text emailText;
    public Text detailText;
    public Text rankText;

    void Start()
    {
        string email = PlayerPrefs.GetString("SelectedPlayerEmail", "Unknown");
        string userID = PlayerPrefs.GetString("SelectedPlayerUserID", "N/A");
        string points = PlayerPrefs.GetString("SelectedPlayerPoints", "0");
        string score = PlayerPrefs.GetString("SelectedPlayerScore", "0");
        int rank = PlayerPrefs.GetInt("SelectedPlayerRank", 0);

        emailText.text = email;

        detailText.text =
        $"\nUser ID: {userID}\n" +
        $"Points: {points}\n" +
        $"Score: {score}\n";

        rankText.text = $"Rank: {rank}";
    }
}
