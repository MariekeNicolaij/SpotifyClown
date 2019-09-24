using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Song : MonoBehaviour
{
    public PlayListManager playListManager;

    public AudioClip song;
    public Button addToPlaylistButton;
    public Button removeFromPlaylistButton;

    public bool isGeneralPlaylist = false;

    // Start is called before the first frame update
    void Start()
    {
        playListManager = FindObjectOfType<PlayListManager>();

        if(isGeneralPlaylist)
        {
            removeFromPlaylistButton.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToPlaylist()
    {
        //Do stuff to get the actual playlist
        playListManager.songToAddToPlaylist = song;
        playListManager.SelectPlaylistToAddSong();
    }

    public void RemoveFromPlaylist()
    {
        
        playListManager.RemoveFromPlaylist(playListManager.currentPlayList, song, this.gameObject);

        Destroy(this.gameObject);
    }
}
