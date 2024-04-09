using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PuzzleBubble.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public List<Sound> sounds = new List<Sound>(); // Use List for dynamic resizing

        private Dictionary<string, AudioClip> loadedClips = new Dictionary<string, AudioClip>();

        private void Awake()
        {
            // Fill the sounds list with sound effect data
            sounds.Add(new Sound { name = "GameOver", clipPath = "Sounds/GameOver" });
            sounds.Add(new Sound { name = "Wrong", clipPath = "Sounds/Negative" });
            sounds.Add(new Sound { name = "ButtonClicked", clipPath = "Sounds/ButtonClicked" });
            sounds.Add(new Sound { name = "Intro", clipPath = "Sounds/Intro" });

            // Load all audio clips asynchronously
            StartCoroutine(LoadAllClips());
        }

        IEnumerator LoadAllClips()
        {
            foreach (Sound sound in sounds)
            {
                ResourceRequest request = Resources.LoadAsync<AudioClip>(sound.clipPath);
                yield return request;

                if (request.asset != null)
                {
                    AudioClip clip = request.asset as AudioClip;
                    loadedClips.Add(sound.name, clip);
                }
                else
                {
                    Debug.LogWarning("Failed to load AudioClip: " + sound.clipPath);
                }
            }
        }

        public void PlaySound(string name)
        {
            if (!loadedClips.ContainsKey(name))
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            AudioSource.PlayClipAtPoint(loadedClips[name], Camera.main.transform.position);
        }

        // Unload unused audio clips (e.g., called during scene transitions)
        public void UnloadUnusedClips()
        {
            List<string> keysToRemove = new List<string>();

            foreach (var entry in loadedClips)
            {
                if (!IsClipUsed(entry.Key))
                {
                    keysToRemove.Add(entry.Key);
                }
            }

            foreach (string key in keysToRemove)
            {
                Resources.UnloadAsset(loadedClips[key]);
                loadedClips.Remove(key);
            }
        }

        // Check if a clip is currently being used
        private bool IsClipUsed(string name)
        {
            foreach (Sound sound in sounds)
            {
                if (sound.name == name)
                {
                    // Implement your logic to determine if the sound is currently playing or needed
                    return true;
                }
            }
            return false;
        }
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public string clipPath; // Path to the AudioClip in Resources folder
        [Range(0f, 1f)]
        public float volume = 1f;
        [Range(0.1f, 3f)]
        public float pitch = 1f;
    }
}
