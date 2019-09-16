using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayList
{
    public string name;
    public List<AudioClip> songs;
}

public class PlayListManager : MonoBehaviour
{

    public List<AudioClip> songs;

    public List<PlayList> listOfPlaylists;

    public void CreatePlaylist(string name)
    {
        PlayList playList = new PlayList();
        playList.name = name;
        listOfPlaylists.Add(playList);
    }


    public void DeletePlaylist(PlayList playList)
    {
        listOfPlaylists.Remove(playList);
    }

    public void AddToPlayList(PlayList playList, AudioClip song)
    {
        playList.songs.Add(song);
    }

    public void RemoveFromPlaylist(PlayList playList, AudioClip song)
    {
        playList.songs.Remove(song);
    }
}
