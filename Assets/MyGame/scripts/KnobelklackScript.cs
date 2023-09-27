using System.Collections;
using UnityEngine;
using TMPro;


public class KnobelklackScript : MonoBehaviour
{
    public TextMeshProUGUI timeText;

    bool gameStartable = false;
    bool gameRunning = false;
    bool askingNumber = false;
    bool nextNumber = false;

    [SerializeField]
    int score = 0;
    int randomNumber;
    int buttonNumber = 0;
    
    [SerializeField]
    float countdown = 1.5f;

    void Start()
    {
        GameReset();
    }

    void Update()
    {
        if (nextNumber == true)
        {
            if (countdown > 0 && buttonNumber == randomNumber)
            {
                score++;
                askingNumber = false;
                NumberAsker();
            }
        }

        if (gameRunning == true && askingNumber == true && countdown > 0)
        {
            countdown = countdown - Time.deltaTime;
        }
        else if (countdown <= 0)
        {
            StartCoroutine(GameOver(score));
        }
    }

    private void FixedUpdate()
    {
        
    }

    void NumberAsker()
    {
        nextNumber = false;
        timeText.text = "Score: " + score.ToString();
        buttonNumber = 0;
        StartCoroutine(Waiter(2));
        
    }

    public void StartButtonCombination(int a)
    {
        if(gameStartable == false && a == 1)
        {
            gameStartable = true;
            timeText.text = "";
        }

        if(gameStartable == true && gameRunning == false && a == 6)
        {
            gameRunning = true;
            NumberAsker();
        }

        buttonNumber = a;
    } 

    IEnumerator GameOver(int s)
    {
        timeText.text = "Game Over: " + s.ToString();
        yield return new WaitForSeconds(5);
        GameReset();
    }

    void GameReset()
    {
        askingNumber = false;
        gameRunning = false;
        gameStartable = false;
        countdown = 1.5f;
        score = 0;
        timeText.text = System.DateTime.Now.ToString("hh:mm");
    }

    IEnumerator Waiter(float b)
    {
        yield return new WaitForSecondsRealtime(b);
        nextNumber = true;
        askingNumber = true;
        countdown = 1.5f - (0.025f * score);
        randomNumber = Random.Range(1, 7);
        timeText.text = randomNumber.ToString();
    }
}
