using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    [SerializeField] Animator transition;

    [SerializeField] float waitSeconds;

    // Update is called once per frame
    void Update()
    {
    }

    public void LoadJustAnimation()
    {
        StartCoroutine("LoadAnimation");
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadMenu()
    {
        StartCoroutine(LoadLevel(0));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(waitSeconds);

        SceneManager.LoadScene(levelIndex);
    }
    IEnumerator LoadAnimation()
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(waitSeconds);
    }
}
