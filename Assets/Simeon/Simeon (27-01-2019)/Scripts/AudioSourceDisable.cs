using UnityEngine;

public class AudioSourceDisable : MonoBehaviour
{
    AudioSource source;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (source)
        {
            if (!source.isPlaying) gameObject.SetActive(false);
        }
    }
}
