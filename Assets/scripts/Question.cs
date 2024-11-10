using UnityEngine;
using System.Collections.Generic; // Add this to access List<>

[System.Serializable]
public class Question
{
    public string question;               // The question text
    public string[] answers;              // Array to store answer options
    public int correctAnswerIndex;        // The index of the correct answer
}

[System.Serializable]
public class QuestionSet
{
    public List<Question> questions;      // List to hold all the questions
}
