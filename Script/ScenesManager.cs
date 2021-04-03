using UnityEngine.SceneManagement;
using UnityEngine;

public class ScenesManager : MonoBehaviour
{
    Scenes scenes;
    public enum Scenes
    {
        bootUp,
        title,
        shop,
        level1,
        level2,
        level3,
        gameOver
    }

    float gameTimer = 0;
    float[] endLevelTimer = { 30, 30, 45 };
    int currentSceneNumber = 0;
    bool gameEnding = false;

    void Update()
    {
        if (currentSceneNumber != SceneManager.GetActiveScene().buildIndex)
        {
            currentSceneNumber = SceneManager.GetActiveScene().buildIndex;
            GetScene();
        }
        GameTimer();
    }

    void GetScene()
    {
        scenes = (Scenes)currentSceneNumber;
    }

    public void GameOver()
    {
        Debug.Log("ENDSCORE:" + GameManager.Instance.GetComponent<ScoreManager>().PlayersScore);
        SceneManager.LoadScene("gameOver");
    }

    void GameTimer()
    {
        switch (scenes)
        {
            case Scenes.level1:
            case Scenes.level2:
            case Scenes.level3:
                {
                    if (gameTimer < endLevelTimer[currentSceneNumber - 3])
                    {
                        //if level has not completed
                        gameTimer += Time.deltaTime;
                    }
                    else
                    {
                        //if level is completed
                        if (!gameEnding)
                        {
                            gameEnding = true;
                            if (SceneManager.GetActiveScene().name != "level3")
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().LevelEnds = true;
                            }
                            else
                            {
                                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerTransition>().GameCompleted = true;
                            }
                            Invoke("NextLevel", 4);
                        }
                    }
                    break;
                }
        }
    }

    void NextLevel()
    {
        gameEnding = false;
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene + 1);
    }

    public void ResetScene()
    {
        gameTimer = 0;
        SceneManager.LoadScene(GameManager.currentScene);
    }

    public void BeginGame(int gameLevel)
    {
        SceneManager.LoadScene(gameLevel);
    }
}