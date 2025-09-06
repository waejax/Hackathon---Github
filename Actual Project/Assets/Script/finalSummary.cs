using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalSummary : MonoBehaviour
{
    public Text describe;
    public Text rank;
    public Text displayScore;
    public Button homeButton;

    private int score;

    void Start()
    {
        score = GameManager.Instance.moralityScore;
        displayScore.text = "Total Morality Score: " + score;

        if (score > 30)
        {
            rank.text = "An Integrity Champion!";
            // describe.text = "You made excellent choices and showed strong fairness in every situation.\nYour decisions reflect honesty, responsibility, and the courage to resist corruption, even in subtle forms.\nIt builds trust and respect wherever you go so keep up this habit!";
        }
        else if (score > 20)
        {
            rank.text = "A Strong Moral Character!";
            // describe.text = "You made mostly fair and honest choices, showing that you understand the importance of acting with integrity.\nA few decisions could have been stronger, but overall you are building a good foundation.\nRemember that even small shortcuts can harm fairness in the long run.";
        }
        else if (score > 10)
        {
            rank.text = "A striver for what is right!";
            // describe.text = "You are beginning to recognize corruption, but sometimes you leaned toward the easier or unfair option.\nSince you show that you are still learning to evaluate evidence and think critically, with more awareness, you will be able to spot hidden corruption more clearly.";
        }
        else if (score > 0)
        {
            rank.text = "A Considerate Actor!";
            // describe.text = "You made several choices that favoured unfairness, which shows how corruption can often be disguised as normal behavior.\nTo learn and improve, pay close attention to the evidence and think about how your actions can affect others.\nWith practice, you will become stronger at spotting unfair situations.";
        }
        else if (score == 0)
        {
            rank.text = "A Balanced Decision Maker!";
            // describe.text = "You often chose paths that encourage corruption, but that is part of the learning process.\nCorruption is not always obvious, and sometimes it takes practice to see the harm it causes.\nUse this as an opportunity to reflect and try again with a new perspective.\nEvery attempt helps you get closer to making fair and honest choices.";
        }
        else if (score < 0)
        {
            rank.text = "A Pragmatic Challenger!";
            // describe.text = "You made excellent choices and showed strong fairness in every situation.\nYour decisions reflect honesty, responsibility, and the courage to resist corruption, even in subtle forms.\nIt builds trust and respect wherever you go so keep up this habit!";
        }
        else if (score < -10)
        {
            rank.text = "A Survivor of Difficult Choices!";
            // describe.text = "You made excellent choices and showed strong fairness in every situation.\nYour decisions reflect honesty, responsibility, and the courage to resist corruption, even in subtle forms.\nIt builds trust and respect wherever you go so keep up this habit!";
        }
        else if (score < -20)
        {
            rank.text = "Focused on results over rules!";
            // describe.text = "You made excellent choices and showed strong fairness in every situation.\nYour decisions reflect honesty, responsibility, and the courage to resist corruption, even in subtle forms.\nIt builds trust and respect wherever you go so keep up this habit!";
        }
        else if (score < -30)
        {
            rank.text = "A seeker of Personal gain!";
        }

        homeButton.onClick.AddListener(() =>
        {
            GameManager.Instance.chatbotReturnScene = "PrimaryLevelEvidence";
            SceneManager.LoadScene("Start");
        });
    }
}
