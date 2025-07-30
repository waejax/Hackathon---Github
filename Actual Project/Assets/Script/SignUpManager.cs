using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

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
            // You can store credentials here (if needed)

            warningText.gameObject.SetActive(false);
            successPopup.SetActive(true); // ✅ Show success pop-up
        }
    }

    void OnClosePopupClicked()
    {
        SceneManager.LoadScene("Login"); // ✅ Redirect to Login
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
