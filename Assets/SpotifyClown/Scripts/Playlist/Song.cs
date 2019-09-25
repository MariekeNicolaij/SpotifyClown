using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Song : MonoBehaviour
{
    public PlayListManager playListManager;
    public AudioClip song;
    public RegularSong regularSong;
    public Button addToPlaylistButton;
    public Button removeFromPlaylistButton;
    public Text songName;

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

    public void AddToPlaylist()
    {
        //Do stuff to get the actual playlist
        playListManager.songToAddToPlaylist = song;
        playListManager.regularSongToAddToPlaylist = regularSong;
        playListManager.SelectPlaylistToAddSong();
    }

    public void RemoveFromPlaylist()
    {

        //playListManager.RemoveFromPlaylist(playListManager.currentPlayList, song, this.gameObject);
        playListManager.RemoveFromPlaylist(playListManager.currentPlayList, regularSong, this.gameObject);

        Destroy(this.gameObject);
    }
}
