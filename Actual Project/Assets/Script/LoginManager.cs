using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.Networking;

public class LoginManager : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button SignupButton;
    public Text warningText;

    string loginURL = "http://localhost/hackathon/login.php";

    void Start()
    {
        LoginButton.onClick.AddListener(OnLoginClicked);
        SignupButton.onClick.AddListener(OnSignupClicked);
        warningText.gameObject.SetActive(false);
    }

    void OnLoginClicked()
    {
        string email = EmailInput.text.Trim();
        string password = PasswordInput.text.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            ShowWarning("Please enter both email and password.");
        }
        else if (!IsValidEmail(email))
        {
            ShowWarning("Please enter a valid email address.");
        }
        else
        {
            StartCoroutine(VerifyLogin(email, password));
        }
    }

    IEnumerator VerifyLogin(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);

        UnityWebRequest www = UnityWebRequest.Post(loginURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            ShowWarning("Server error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text.Trim();
            Debug.Log("Server response: " + response);

            if (response.StartsWith("OK|"))
            {
                // Split into parts
                string[] parts = response.Split('|');
                GameManager.Instance.userID = int.Parse(parts[1]);      // userID
                string lastScene = parts[2];                            // lastScene
                GameManager.Instance.moralityScore = int.Parse(parts[3]); // moralityScore

                // Go to lastScene
                SceneManager.LoadScene(lastScene);
            }
            else if (response == "Invalid")
            {
                ShowWarning("Invalid email or password.");
            }
            else
            {
                ShowWarning("Unexpected server response.");
            }
        }
    }

    bool IsValidEmail(string email)
    {
        string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, pattern);
    }

    void ShowWarning(string message)
    {
        warningText.text = message;
        warningText.color = Color.red;
        warningText.gameObject.SetActive(true);
    }

    void OnSignupClicked()
    {
        SceneManager.LoadScene("SignUp");
    }
}
