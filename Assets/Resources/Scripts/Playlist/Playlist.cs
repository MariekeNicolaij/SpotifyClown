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

    public string playlistName = "playlist";
    public PlayListManager playListManager;

    public PlayListData playListData = new PlayListData();
    public List<AudioClip> songs;
    public List<RegularSong> playlistSongs;
    public List<GameObject> songGO;

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
        playListManager.SwitchPlaylist(this);

        foreach (RegularSong song in playlistSongs)
        {
            GameObject obj = Instantiate(songPrefab, playListManager.songRoot.transform);
            Song s = obj.GetComponent<Song>();
            s.regularSong = song;
            s.songImage.sprite = song.songImage;
            s.songName.text = song.songName;
            s.songArtist.text = song.songArtist;

            songGO.Add(obj);

            if (isGeneralPlaylist)
            {
                obj.GetComponent<Song>().removeFromPlaylistButton.gameObject.SetActive(false);
            }
            else
                deletePlaylistButton.gameObject.SetActive(true);
        }

        /*
        foreach (AudioClip song in songs)
        {
            GameObject obj = Instantiate(songPrefab, playListManager.songRoot.transform);
            obj.GetComponent<Song>().song = song;
            obj.GetComponent<Song>().songName.text = song.name.ToString();
            songGO.Add(obj);
            if(isGeneralPlaylist)
            {
                obj.GetComponent<Song>().removeFromPlaylistButton.gameObject.SetActive(false);
            }
        }*/


        if (!isGeneralPlaylist)
        {
            //deletePlaylistButton.gameObject.SetActive(true);
        }
    }

    public void Delete()
    {
        playListManager.DeletePlaylist();//this);
        Destroy(this.gameObject);
    }
}
