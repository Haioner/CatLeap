using UnityEngine;

public class MusicRandomizer : MonoBehaviour
{
    [Header("Musics")]
    [SerializeField] private AudioClip[] musicsClips;
    [SerializeField] private AudioSource _source;
    private int clipIndex = 0;

    public void Start()
    {
        StartMusics();
    }

    void StartMusics()
    {
        _source.clip = musicsClips[clipIndex];
        _source.Play();

        //Invoke timer to next music
        Invoke("PlayNextTrack", _source.clip.length);
    }

    void PlayNextTrack()
    {
        //Change Index
        if (clipIndex < musicsClips.Length - 1)
            clipIndex++;
        else
            clipIndex = 0;

        //Set and Play Music Index
        _source.Stop();
        _source.clip = musicsClips[clipIndex];
        _source.Play();

        //Invoke timer to next music
        Invoke("PlayNextTrack", _source.clip.length);
    }
}
