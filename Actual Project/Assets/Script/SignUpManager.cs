using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using System.Collections;
using UnityEngine.Networking;

public class SignUpManager : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button SignUpButton;
    public Button LoginButton;
    public Text warningText; // Assign in Inspector

    public GameObject successPopup; // Assign in Inspector
    public Button closePopupButton; // Assign in Inspector

    // URL to your PHP signup script
    string signupURL = "http://localhost/hackathon/signup.php";

    void Start()
    {
        SignUpButton.onClick.AddListener(OnSignUpClicked);
        LoginButton.onClick.AddListener(OnLoginClicked);
        closePopupButton.onClick.AddListener(OnClosePopupClicked);

        warningText.gameObject.SetActive(false);
        successPopup.SetActive(false); // Hide pop-up at start
    }

    void OnSignUpClicked()
    {
        string email = EmailInput.text.Trim();
        string password = PasswordInput.text;
        string confirmPassword = ConfirmPasswordInput.text;

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmPassword))
        {
            ShowWarning("Please fill in all fields.");
        }
        else if (!IsValidEmail(email))
        {
            ShowWarning("Invalid email format.");
        }
        else if (password != confirmPassword)
        {
            ShowWarning("Passwords do not match.");
        }
        else
        {
            // Start coroutine to create user
            StartCoroutine(CreateUser(email, password));
        }
    }

    IEnumerator CreateUser(string email, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("emailPost", email);
        form.AddField("passwordPost", password);

        UnityWebRequest www = UnityWebRequest.Post(signupURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            ShowWarning("Server error: " + www.error);
        }
        else
        {
            if (www.downloadHandler.text.Contains("Everything ok"))
            {
                warningText.gameObject.SetActive(false);
                successPopup.SetActive(true);
            }
            else
            {
                ShowWarning(www.downloadHandler.text);
            }
        }
    }

    void OnClosePopupClicked()
    {
        SceneManager.LoadScene("Login"); //Redirect to Login
    }

    void OnLoginClicked()
    {
        SceneManager.LoadScene("Login");
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
}
