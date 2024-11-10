using UnityEngine;
using TMPro; // For TextMeshPro elements

public class QuestionDisplay : MonoBehaviour
{
    public TextMeshProUGUI questionText;  // Assign in the Inspector
    public TextMeshProUGUI[] answerTexts; // An array of TextMeshPro components for answers

    public void DisplayQuestion(Question question)
    {
        questionText.text = question.question;
        for (int i = 0; i < question.answers.Length && i < answerTexts.Length; i++)
        {
            answerTexts[i].text = question.answers[i];
        }
    }
}
