using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayList
{
    public string name;
    public List<AudioClip> songs;
}

public class PlayListManager : MonoBehaviour
{
    public PlayList allSongsPlaylist;

    public List<AudioClip> allSongs;
    public List<PlayList> listOfPlaylists;


    public PlayList currentPlayList;

    public Dropdown playlistDropdown;

    public void Start()
    {
        //Add all the songs to the all songs playlist
        foreach (AudioClip song in allSongs)
        {
            allSongsPlaylist.songs.Add(song);
        }

        currentPlayList = allSongsPlaylist;
        UpdatePlaylistUI();
    }

    public void CreatePlaylist(string name)
    {
        PlayList playList = new PlayList();
        playList.name = name;
        listOfPlaylists.Add(playList);
    }

    public void SwitchPlaylist(PlayList selectedPlaylist)
    {
        currentPlayList = selectedPlaylist;
        UpdatePlaylistUI();
    }


    public void DeletePlaylist(PlayList playList)
    {
        listOfPlaylists.Remove(playList);

        //Move to the all songs playlist
        SwitchPlaylist(allSongsPlaylist);
    }

    public void AddToPlayList(PlayList playList, AudioClip song)
    {
        playList.songs.Add(song);
    }

    public void RemoveFromPlaylist(PlayList playList, AudioClip song)
    {
        playList.songs.Remove(song);
        UpdatePlaylistUI();
    }

    public void UpdatePlaylistUI()
    {
        //Update UI
    }

    //TODO:

    //3. Swap Between playlist
    //4. Add songs to playlist from the "general" playlist
    //5. Remove songs from specific playlist
    //6. Delete playlist

    //Done:
    //1. the "general" play list with all the songs(cannot be deleted)
    //2. Create playlist
}
