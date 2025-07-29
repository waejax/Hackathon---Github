using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class LoginManager : MonoBehaviour
{
    public InputField EmailInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button SignupButton;
    public Text warningText; // Assign in Inspector

    void Start()
    {
        LoginButton.onClick.AddListener(OnLoginClicked);
        SignupButton.onClick.AddListener(OnSignupClicked);
        warningText.gameObject.SetActive(false); // Hide warning at start
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
            warningText.gameObject.SetActive(false);
            SceneManager.LoadScene("LevelSelectScene");
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
