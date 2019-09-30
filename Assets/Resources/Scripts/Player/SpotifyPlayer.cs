using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpotifyPlayer : MonoBehaviour
{
    public Text playlistTitle;
    public Image songImage;
    public Text songName;
    public Text songArtist;
    public Slider durationSlider;
    public Text currentTime;
    public Text durationTime;

    public Image favouriteImage;
    public Sprite favouriteOnSprite, favouriteOffSprite;

    public Image shuffleImage;
    public Sprite shuffleOnSprite, shuffleOffSprite;

    public Image loopImage;
    public Sprite loopOnSprite, loopOffSprite;

    public Image pausePlayImage;
    public Sprite pauseSprite, playSprite;

    public bool pause, shuffle, loop;
    public int index = 0, nextIndex = 0, previousSongIndex = 0;

    public AudioSource audioSource;
    public Playlist currentPlaylist;
    AudioClip clip;

    public Animation imageAnimation;
    public AnimationClip slideLeftFadeOut, slideRightFadeOut, slideLeftFadeIn, slideRightFadeIn;
    AnimationClip clip1, clip2; // Clip1 is for the first(current) image and clip2 for the second(next) image


    void Start()
    {
        shuffle = System.Convert.ToBoolean(PlayerPrefs.GetInt("Shuffle", 0));
        loop = System.Convert.ToBoolean(PlayerPrefs.GetInt("Loop", 0));
        shuffleImage.sprite = (shuffle) ? shuffleOnSprite : shuffleOffSprite;
        loopImage.sprite = (loop) ? loopOnSprite : loopOffSprite;

        durationSlider.onValueChanged.AddListener(delegate { OnSliderMove(); });

        Skybox.ChangeBackgroundColor();

        PlaySong();
    }

    public void DeterminePlaylistName()
    {
        string name = currentPlaylist.playlistName;
        if (name == string.Empty)
            name = "All songs";
        playlistTitle.text = "PLAYING FROM PLAYLIST \n" + name;
    }

    public void SetUIStuff()
    {
        RegularSong song = currentPlaylist.playlistSongs[index];
        songImage.sprite = song.songImage;
        songName.text = song.songName;
        songArtist.text = song.songArtist;

        SetDurationText();
        DeterminePlaylistName();

        Debug.Log(currentPlaylist.playlistName);

        //isAnimatingSongImage = true;
        /*
        songImage.sprite = tempPlaylist[index].songImage;
        //isAnimatingSongImage = true;
        songName.text = clip.name;
        Debug.Log("Kan ik de artist uit de musicfile halen?"); //public Text songArtist;

        SetDurationText();
        */
    }

    void SetDurationText()
    {
        string minutes = Mathf.Floor((int)clip.length / 60).ToString("00");
        string seconds = ((int)clip.length % 60).ToString("00");

        durationTime.text = minutes + ":" + seconds;
    }

    // Called in Editor
    public void PauseButton()
    {
        pause = !pause;

        pausePlayImage.sprite = (pause) ? pauseSprite : playSprite;

        if (pause)
            audioSource.Pause();
        else
            audioSource.UnPause();
    }

    // Called in Editor
    public void PreviousButton()
    {
        CancelInvoke();
        //Previous();
        ImageAnimation(true);
    }

    void Previous()
    {
        if (!loop)
        {
            int playlistLength = currentPlaylist.playlistSongs.Count;

            // Determine which song has to be played
            if (shuffle)
            {
                while (nextIndex == index) // We do not want the same song to play again
                    nextIndex = Random.Range(0, playlistLength);
            }
            else
            {
                nextIndex = index - 1;
                if (nextIndex < 0)
                    nextIndex = playlistLength - 1;
            }
        }

        PlaySong();
    }

    // Called in Editor
    public void NextButton()
    {
        CancelInvoke();
        //Next();
        ImageAnimation(false);
    }

    void Next()
    {
        if (!loop)
        {
            int playlistLength = currentPlaylist.playlistSongs.Count;

            Debug.Log(nextIndex);
            // Determine which song has to be played
            if (shuffle)
            {
                while (nextIndex == index) // We do not want the same song to play again
                    nextIndex = Random.Range(0, playlistLength);
            }
            else
            {
                nextIndex = index + 1;
                if (nextIndex >= playlistLength)
                    nextIndex = 0;
            }
        }

        PlaySong();
    }

    // Dont call this directly, Call Next or Previous instead
    public void PlaySong()
    {
        if (currentPlaylist.playlistSongs.Count > 0)
        {
            clip = currentPlaylist.playlistSongs[nextIndex].song;

            previousSongIndex = index;
            index = nextIndex;

            // UI
            SetUIStuff();

            // Play the song
            durationSlider.value = 0;
            audioSource.Stop();
            audioSource.clip = clip;
            audioSource.Play();

            // Get and set the heart icon
            Favourite();
        }
    }

    // Toggles shuffle icon when manual is true, else it will just get the current value
    public void Shuffle(bool manual = false)
    {
        shuffle = System.Convert.ToBoolean(PlayerPrefs.GetInt("Shuffle", 0));

        if (manual)
        {
            shuffle = !shuffle;
            PlayerPrefs.SetInt("Shuffle", System.Convert.ToInt32(shuffle));
        }

        shuffleImage.sprite = (shuffle) ? shuffleOnSprite : shuffleOffSprite;
    }

    // Toggles loop icon when manual is true, else it will just get the current value
    public void Loop(bool manual = false)
    {
        loop = System.Convert.ToBoolean(PlayerPrefs.GetInt("Loop", 0));

        if (manual)
        {
            loop = !loop;
            PlayerPrefs.SetInt("Loop", System.Convert.ToInt32(loop));
        }

        loopImage.sprite = (loop) ? loopOnSprite : loopOffSprite;
    }

    // Toggles favourite icon when manual is true, else it will just get the current value
    public void Favourite(bool manual = false)
    {
        bool isFavourite = System.Convert.ToBoolean(PlayerPrefs.GetInt(clip.name + "IsFavourite", 0));

        if (manual)
        {
            isFavourite = !isFavourite;
            PlayerPrefs.SetInt(clip.name + "IsFavourite", System.Convert.ToInt32(isFavourite));
        }

        favouriteImage.sprite = (isFavourite) ? favouriteOnSprite : favouriteOffSprite;
    }

    void Update()
    {
        UpdateSlider();

        // Next song timer
        if (audioSource.time >= clip.length)
            Next();
    }

    void UpdateSlider()
    {
        durationSlider.value = audioSource.time / clip.length;
        SetCurrentTimeText();
    }

    // Slider doorspoel feature
    public void OnSliderMove()
    {
        float timeValue = durationSlider.value * clip.length;
        audioSource.time = timeValue;
    }

    void SetCurrentTimeText()
    {
        string minutes = Mathf.Floor((int)audioSource.time / 60).ToString("00");
        string seconds = ((int)audioSource.time % 60).ToString("00");

        currentTime.text = minutes + ":" + seconds;
    }

    void ImageAnimation(bool previousSong)
    {
        if (previousSong)
        {
            clip1 = slideRightFadeOut;
            clip2 = slideRightFadeIn;
            Invoke("Previous", clip1.length);
        }
        else
        {
            clip1 = slideLeftFadeOut;
            clip2 = slideLeftFadeIn;
            Invoke("Next", clip1.length);
        }

        imageAnimation.clip = clip1;

        imageAnimation.Play();
        Invoke("NextImageAnimation", imageAnimation.clip.length);
    }

    void NextImageAnimation()
    {
        imageAnimation.clip = clip2;
        imageAnimation.Play();
    }
}