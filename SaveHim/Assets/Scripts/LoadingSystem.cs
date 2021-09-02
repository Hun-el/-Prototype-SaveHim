using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSystem : MonoBehaviour
{
    Animator animator;

    private void Start() 
    {
        animator = GameObject.FindWithTag("LevelLoader").GetComponent<Animator>();
    }

    public void LoadLevel(string levelName)
    {
        StartCoroutine(LoadLevelCo(levelName));
    }

    IEnumerator LoadLevelCo(string levelName)
    {
        if(levelName == "Restart" || levelName == "NextLevel" || levelName == "PreviousLevel")
        {
            if(levelName == "Restart")
            {
                levelName = SceneManager.GetActiveScene().name;
                animator.SetTrigger("End");
                yield return new WaitForSeconds(1);
                SceneManager.LoadScene(levelName);
            }
            if(levelName == "NextLevel")
            {
                levelName = SceneManager.GetActiveScene().name;
                string level = levelName.Substring(levelName.Length - 1);

                int levelIndex = int.Parse(level) + 1;

                if(Application.CanStreamedLevelBeLoaded("Level"+levelIndex))
                {
                    animator.SetTrigger("End");
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene("Level"+levelIndex);
                }
            }
            if(levelName == "PreviousLevel")
            {
                levelName = SceneManager.GetActiveScene().name;
                string level = levelName.Substring(levelName.Length - 1);

                int levelIndex = int.Parse(level) - 1;

                if(Application.CanStreamedLevelBeLoaded("Level"+levelIndex))
                {
                    animator.SetTrigger("End");
                    yield return new WaitForSeconds(1);
                    SceneManager.LoadScene("Level"+levelIndex);
                }
            }
        }
        else
        {
            animator.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadScene(levelName);
        }
    }
}
