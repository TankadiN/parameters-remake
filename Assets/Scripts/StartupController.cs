using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartupController : MonoBehaviour
{

    public float delay;

    public bool introPlayed;

    public PlayableDirector timeline;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (delay <= 0)
        {
            if (timeline.state == PlayState.Paused)
            {
                if (!introPlayed)
                {
                    timeline.Play();
                    introPlayed = true;
                    delay = 2f;
                }
                else
                {
                    SceneManager.LoadScene(1);
                }
            }
        }

        if(timeline.state == PlayState.Paused && delay >= 0)
        {
            delay -= Time.deltaTime;
        }

    }
}
