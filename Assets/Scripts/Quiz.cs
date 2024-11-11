using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] TextMeshProUGUI scoreTMP;
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    [Header("Questions")]
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO questionSO;

    [Header("Buttons")]
    [SerializeField] Sprite defaultAnsSprite;
    [SerializeField] Sprite correctAnsSprite;
    [Header("Timer")]
    [SerializeField] Image imageTimer;
    Timer timer;
    bool hasAnsweredEarly;
    ScoreKeeper scoreKeeper;
    [SerializeField] Slider slider;

    void Start()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        slider.maxValue=questions.Count;
        slider.value=0;
    }

    public void Update()
    {
        // Update the timer UI fill amount
        imageTimer.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion)
        {
            // Prepare for the next question when timer triggers loadNextQuestion
            hasAnsweredEarly = false;
            timer.loadNextQuestion = false;
            GetNextQuestion();
        }else if(!hasAnsweredEarly && !timer.isAnsweringQuesion){
            DisplayAnswer(-1);
            SetButtonState(false);
        }
        scoreTMP.text="Score is: "+scoreKeeper.CalculateScore();
    }

    public void GetNextQuestion()
    {
        if (questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultSprites();

            // Ensure we get a new random question and remove it from the list
            GetRandomQuestion();
            DisplayQuestion();
            slider.value++;
            scoreKeeper.IncrementQuestionSeen();
        }
        else
        {
            questionText.text = "Quiz Complete!";
            SetButtonState(false);
        }
    }

    void GetRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        questionSO = questions[index];
        questions.RemoveAt(index); // Remove question directly by index for simplicity
    }

    public void DisplayQuestion()
    {
        questionText.text = questionSO.GetQuestion();
        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI textButton = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            textButton.text = questionSO.GetAnswer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        hasAnsweredEarly = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
    }

    public void DisplayAnswer(int index)
    {
        if (index == questionSO.GetCorrectAnswerIndex())
        {
            questionText.text = "Correct Answer!";
            Image image = answerButtons[index].GetComponent<Image>();
            image.sprite = correctAnsSprite;
            scoreKeeper.IncrementCorrectAnswer();
        }
        else
        {
            questionText.text = "Sorry, the correct answer was: \n" + questionSO.GetAnswer(questionSO.GetCorrectAnswerIndex());
            Image image = answerButtons[questionSO.GetCorrectAnswerIndex()].GetComponent<Image>();
            image.sprite = correctAnsSprite;
        }
    }

    public void SetButtonState(bool state)
    {
        foreach (var buttonObj in answerButtons)
        {
            Button button = buttonObj.GetComponent<Button>();
            button.interactable = state;
        }
    }

    public void SetDefaultSprites()
    {
        foreach (var buttonObj in answerButtons)
        {
            Image image = buttonObj.GetComponent<Image>();
            image.sprite = defaultAnsSprite;
        }
    }
}
