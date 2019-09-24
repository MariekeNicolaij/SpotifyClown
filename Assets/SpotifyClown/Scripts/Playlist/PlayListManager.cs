﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class PlayListManager : MonoBehaviour
{
    public Playlist allSongsPlaylist;

    public List<AudioClip> allSongs;
    public List<Playlist> listOfPlaylists;


    public Playlist currentPlayList;
    public Playlist nextPlaylist;

    public Dropdown playlistDropdown;

    public GameObject playlistPrefab;

    public GameObject playlistRoot;
    public GameObject songRoot;



    public GameObject addToPlaylistSelection;
    public Dropdown addToPlaylistSelectionDropdown;

    public InputField newPlaylistName;

    public void Start()
    {
        //Add all the songs to the all songs playlist
        foreach (AudioClip song in allSongs)
        {
            //allSongsPlaylist.playListData.songs.Add(song);
            allSongsPlaylist.songs.Add(song);
        }

        currentPlayList = allSongsPlaylist;
        addToPlaylistSelection.SetActive(false);
        UpdatePlaylistUI();
    }

    public void CreatePlaylist()
    {
        PlayListData playList = new PlayListData();


        GameObject obj = Instantiate(playlistPrefab, playlistRoot.transform);

        Playlist playlist = obj.GetComponentInChildren<Playlist>();
        playlist.playListData = playList;
        playlist.buttonText.text = newPlaylistName.text;
        playList.name = newPlaylistName.text;

        listOfPlaylists.Add(playlist);
    }

    public void SwitchPlaylist(Playlist selectedPlaylist)
    {
        foreach(GameObject obj in currentPlayList.songGO)
        {
            Destroy(obj);
        }

        currentPlayList = selectedPlaylist;
        UpdatePlaylistUI();
    }


    public void DeletePlaylist(Playlist playList)
    {
        listOfPlaylists.Remove(playList);

        //Move to the all songs playlist
        SwitchPlaylist(allSongsPlaylist);
    }

    public void AddToPlayList(PlayListData playList, AudioClip song)
    {
        playList.songs.Add(song);
    }

    public void RemoveFromPlaylist(PlayListData playList, AudioClip song)
    {
        //playList.songs.Remove(song);
        UpdatePlaylistUI();
    }

    public void UpdatePlaylistUI()
    {
        //Update UI
    }

    public void SelectPlaylistToAddSong()
    {

    }

    //TODO:
    //4. Add songs to playlist from the "general" playlist

    //Done:
    //1. Create playlist
    //6. Delete playlist
    //5. Remove songs from specific playlist
    //3. Swap Between playlist
    //2. the "general" play list with all the songs(cannot be deleted)


}
