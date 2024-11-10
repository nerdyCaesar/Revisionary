using UnityEngine;
using System.Collections;
using TMPro; // Import TextMeshPro namespace

public class answerplatform : MonoBehaviour
{
    public string answerText;  // The answer text to display
    public bool isCorrect;     // Whether this platform is the correct answer
    private TextMeshProUGUI textMeshPro; // Reference to the TextMeshProUGUI component
    public GameObject End;
    public TextMeshProUGUI win;

    void Start()
    {
        // Ensure we get the TextMeshProUGUI component that is inside the child Canvas
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>(); 

        if (textMeshPro == null)
        {
            Debug.LogError("TextMeshProUGUI component not found on " + gameObject.name);
        }
        else
        {
            textMeshPro.text = answerText; // Set the text on the platform
        }
    }

    // Method to set the answer text and the isCorrect flag
    public void SetAnswer(string answer, bool correct)
    {
        answerText = answer;      // Set the answer text
        isCorrect = correct;      // Set if the answer is correct

        // Set the text only if textMeshPro is properly assigned
        if (textMeshPro != null)
        {
            textMeshPro.text = answerText;  // Update the text with the assigned answer
        }
        else
        {
            Debug.LogError("TextMeshProUGUI component is missing on " + gameObject.name);
        }
    }

    // On collision with the player, check if the answer is correct or not
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // Ensure collision is only detected with the player
        {
            Debug.Log("Player collided with: " + gameObject.name + ", isCorrect: " + isCorrect);

            // Check if the platform has the correct tag or specific condition to be considered an answer platform
            if (gameObject.CompareTag("AnswerPlatform"))
            {
                if (isCorrect)
                {
                    Debug.Log("Correct Answer on platform: " + gameObject.name);
                    win.text = "You Win!";
                    End.SetActive(true);
                }
                else
                {
                    Debug.Log("Wrong Answer on platform: " + gameObject.name);
                    win.text = "You Lose!";
                    End.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Player collided with a non-answer platform: " + gameObject.name);
            }
        }
    }

    private IEnumerator SlidePlayerAndLoadNextQuestion(GameObject player)
    {
        // Slide the player to a new position smoothly (adjust values as needed)
        Vector3 targetPosition = player.transform.position + new Vector3(5f, 0, 0); // Slide distance (e.g., 5 units to the right)
        float slideDuration = 1f; // Slide duration in seconds
        float elapsedTime = 0f;

        while (elapsedTime < slideDuration)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, targetPosition, (elapsedTime / slideDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        player.transform.position = targetPosition; // Ensure the player ends at the exact target position

        // Generate the next question after the slide is complete
        FindObjectOfType<QuestionLoader>().DisplayRandomQuestion();

        // Reset UI or gameplay logic as needed to prepare for the next question
        win.text = ""; // Clear the win/lose message
        End.SetActive(false); // Hide the end game UI

    }



}
