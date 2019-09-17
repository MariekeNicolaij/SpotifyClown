using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayListData
{
    public string name;
    public List<AudioClip> songs;
}

public class Playlist : MonoBehaviour
{
    public PlayListManager playListManager;

    public PlayListData playListData;
    public Button button;
    public Text buttonText;
    public GameObject songPrefab;

    public Button deletePlaylistButton;


    // Start is called before the first frame update
    void Start()
    {
        playListManager = FindObjectOfType<PlayListManager>();

        buttonText.text = playListData.name;        
    }

    public void switchPlaylist()
    {
        foreach (AudioClip song in playListData.songs)
        {
            GameObject obj = Instantiate(songPrefab);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Delete()
    {
        playListManager.DeletePlaylist(this);
        Destroy(this.gameObject);
    }
}
