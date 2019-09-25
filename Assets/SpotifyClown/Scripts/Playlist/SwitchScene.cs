using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public static SwitchScene switchScene;
    public SpotifyPlayer spotifyPlayer;
    public PlayListManager playListManager;

    public Playlist playlist;

    public void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if(switchScene != null && switchScene != this)
        {
            Destroy(this.gameObject);

        }
        else
        {
            switchScene = this;
        }

        DontDestroyOnLoad(this);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        spotifyPlayer = FindObjectOfType<SpotifyPlayer>();
        playListManager = FindObjectOfType<PlayListManager>();
    }



    public void ChangeScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
