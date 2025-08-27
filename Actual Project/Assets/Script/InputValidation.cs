using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InputValidation : MonoBehaviour
{
    public InputField subjectInput;
    public InputField incidentDetailsInput;
    public InputField infoSourceInput;
    public InputField corruptionTypeInput;
    public InputField peopleInvolvedInput;
    public InputField addressInput;
    public InputField positonInput;
    public InputField numberInput;
    public InputField icInput;

    public Text errorText;
    
    public bool Validate()
    {
        if (errorText != null)
            errorText.text = "";

        if (subjectInput != null && string.IsNullOrWhiteSpace(subjectInput.text))
        {
            errorText.text = "Subject is required";
            return false;
        }

        if (incidentDetailsInput != null && string.IsNullOrWhiteSpace(incidentDetailsInput.text))
        {
            errorText.text = "Incident is required";
            return false;
        }

        if (infoSourceInput != null && string.IsNullOrWhiteSpace(infoSourceInput.text))
        {
            errorText.text = "Source of Info is required";
            return false;
        }

        if (corruptionTypeInput != null && string.IsNullOrWhiteSpace(corruptionTypeInput.text))
        {
            errorText.text = "Type of corruption/gratification is required";
            return false;
        }

        if (peopleInvolvedInput != null && string.IsNullOrWhiteSpace(peopleInvolvedInput.text))
        {
            errorText.text = "People Involved is required";
            return false;
        }

        if (addressInput != null && string.IsNullOrWhiteSpace(addressInput.text))
        {
            errorText.text = "Address is required";
            return false;
        }

        if (positonInput != null && string.IsNullOrWhiteSpace(positonInput.text))
        {
            errorText.text = "Position is required";
            return false;
        }

        // if (emailInput != null && !string.IsNullOrWhiteSpace(emailInput.text))
        // {
        //     if (!Regex.IsMatch(emailInput.text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        //     {
        //         errorText.text = "Invalid email format. Expected format: name@company.domain";
        //         return false;
        //     }
        // }

        if (numberInput != null && !string.IsNullOrWhiteSpace(numberInput.text))
        {
            if (!Regex.IsMatch(numberInput.text, @"^\d{7}$"))
            {
                errorText.text = "Phone number must be 7 digits.";
                return false;
            }
        }

        if (icInput != null && !string.IsNullOrWhiteSpace(icInput.text))
        {
            if (!Regex.IsMatch(icInput.text, @"^\d{8}$"))
            {
                errorText.text = "IC number must be 8 digits.";
                return false;
            }
        }

        return true;
    }
}
