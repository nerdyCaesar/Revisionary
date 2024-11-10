using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class QuestionLoader : MonoBehaviour
{
    public TextMeshProUGUI questionText; // UI Text component to display question
    public List<answerplatform> answerPlatforms; // List of answer platforms
    public string jsonFileName = "questions"; // The name of the JSON file (without extension)
    private QuestionSet questionSet; // The set of questions

    void Start()
    {
        LoadQuestions();
        DisplayRandomQuestion();
    }

    // Load questions from the JSON file
    public void LoadQuestions()
    {
        TextAsset jsonText = Resources.Load<TextAsset>(jsonFileName); // Load the JSON file from Resources folder
        if (jsonText != null)
        {
            questionSet = JsonUtility.FromJson<QuestionSet>(jsonText.ToString());
            Debug.Log("Questions loaded: " + questionSet.questions.Count); // Log the number of questions loaded
        }
        else
        {
            Debug.LogError("Failed to load questions from JSON file!");
        }
    }

    // Display a random question and its answers
    public void DisplayRandomQuestion()
    {
        if (questionSet != null && questionSet.questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questionSet.questions.Count);
            Question selectedQuestion = questionSet.questions[randomIndex];
            questionText.text = selectedQuestion.question; // Display the question

            List<string> shuffledAnswers = new List<string>(selectedQuestion.answers);
            Shuffle(shuffledAnswers); // Shuffle the answers

            // Find the correct answer from the original answers
            string correctAnswer = selectedQuestion.answers[selectedQuestion.correctAnswerIndex];

            // Assign shuffled answers to the platforms and check if they match the correct answer
            // Assuming the correct answer is selected from the question's answers:
            for (int i = 0; i < answerPlatforms.Count && i < shuffledAnswers.Count; i++)
            {
                // Check if this platform's answer matches the actual correct answer
                bool isCorrect = shuffledAnswers[i] == selectedQuestion.answers[selectedQuestion.correctAnswerIndex];
                answerPlatforms[i].SetAnswer(shuffledAnswers[i], isCorrect);
                Debug.Log("Assigned answer to platform " + i + ": " + shuffledAnswers[i] + " (Correct: " + isCorrect + ")");
            }

        }
        else
        {
            Debug.LogError("No questions available to display!");
        }
    }


    // Shuffle the answers to randomize their order
    public void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
