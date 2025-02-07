using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuessTheNumber: MonoBehaviour
{
    public TextMeshProUGUI headerText;            // Reference to the Header Text
    public TMP_InputField guessInputField; // Reference to the Input Field
    public Button submitButton;        // Reference to the Submit Button
    public Button resetButton;         // Reference to the Reset Button

    private int targetNumber;          // The random number the player needs to guess
    private int remainingAttempts = 3; // Number of attempts the player has

    void Start()
    {
        // Attach the buttons to their respective methods
        submitButton.onClick.AddListener(SubmitGuess);
        resetButton.onClick.AddListener(GameSetup);

        // Start the game
        GameSetup();
    }

    public void GameSetup()
    {
        // Reset attempts
        remainingAttempts = 3;

        // Choose a random number between 1 and 10
        targetNumber = Random.Range(1, 11);

        // Update the header text
        headerText.text = $"Guess the Number! You have {remainingAttempts} attempts remaining.";

        // Clear the input field
        guessInputField.text = "";

        // Enable the Submit button and Input Field
        guessInputField.interactable = true;
        submitButton.interactable = true;
    }

    public void SubmitGuess()
    {
        // Validate the input
        if (string.IsNullOrEmpty(guessInputField.text))
        {
            headerText.text = "Please enter a number!";
            return;
        }

        if (!int.TryParse(guessInputField.text, out int playerGuess))
        {
            headerText.text = "Invalid input! Please enter a number.";
            return;
        }

        // Check if the guess is correct
        if (playerGuess == targetNumber)
        {
            headerText.text = "Congratulations! You guessed the correct number!";
            EndGame();
        }
        else
        {
            remainingAttempts--;

            if (remainingAttempts > 0)
            {
                headerText.text = playerGuess < targetNumber
                    ? $"Too low! {remainingAttempts} attempts remaining."
                    : $"Too high! {remainingAttempts} attempts remaining.";
            }
            else
            {
                headerText.text = $"Game Over! The correct number was {targetNumber}.";
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        // Disable input and submission
        guessInputField.interactable = false;
        submitButton.interactable = false;
    }
}