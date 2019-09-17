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

    public bool isGeneralPlaylist = false;

    // Start is called before the first frame update
    void Start()
    {
        playListManager = FindObjectOfType<PlayListManager>();

        deletePlaylistButton.gameObject.SetActive(false);
        if (isGeneralPlaylist)
        {
            buttonText.text = "All songs";
        }

        else
        {
            buttonText.text = playListData.name;
        }
    }

    public void switchPlaylist()
    {
        playListManager.currentPlayList.deletePlaylistButton.gameObject.SetActive(false);

        playListManager.SwitchPlaylist(this);

        /*
        foreach (AudioClip song in playListData.songs)
        {
            GameObject obj = Instantiate(songPrefab);

        }
        */

        if (!isGeneralPlaylist)
        {
            deletePlaylistButton.gameObject.SetActive(true);
        }
    }

    public void Delete()
    {
        playListManager.DeletePlaylist(this);
        Destroy(this.gameObject);
    }
}
