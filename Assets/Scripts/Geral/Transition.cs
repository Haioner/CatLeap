using UnityEngine.SceneManagement;
using UnityEngine;

public class Transition : MonoBehaviour
{
    [SerializeField] private Animator anim;
    private string sceneName;

    private void Start()
    {
        if (!Game_Manager.isPlaying)
            anim.Play("inTransition");
    }

    public void PlayOutTransition(string _sceneName)
    {
        //Play Off transition and set the scene name
        anim.Play("outTransition");
        sceneName = _sceneName;
    }

    public void ChangeSceneTo_EVENT()
    {
        //Change scene (in event annimation)
        SceneManager.LoadScene(sceneName);
    }
}
