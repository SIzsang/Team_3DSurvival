using System.Collections;
using UnityEngine;

namespace Core.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        private AudioSource _activeSource;
        private Coroutine _crossfadeCoroutine;

        [SerializeField] private AudioSource bgmSource1;
        [SerializeField] private AudioSource bgmSource2;
        [SerializeField] private float fadeDuration = 0.5f;
        private AudioSource[] _sfxSources;

        public bool IsBgmPlaying => _activeSource.isPlaying;
        public bool IsPlayForce = false;

        [Header("BGM Clips")]
        public AudioClip daytimeBgm;       // 타이틀 BGM
        public AudioClip nighttimeBgm;
        public AudioClip recallBgm;

        [Header("Player Sfxs")]
        public AudioClip walk;
        public AudioClip jump;
        public AudioClip getWood;
        public AudioClip getWater;
        public AudioClip getStone;
        public AudioClip die;
        public AudioClip clearQuest;


        [Header("Monster Sfxs")]
        public AudioClip monsterIdle;
        public AudioClip monsterDie;
        public AudioClip monsterAttack;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            _sfxSources = new AudioSource[30];
            for (int i = 0; i < _sfxSources.Length; i++)
            {
                _sfxSources[i] = gameObject.AddComponent<AudioSource>();
            }

            _activeSource = bgmSource1;
            DontDestroyOnLoad(gameObject);
        }

        public void PlayBgm(AudioClip newClip)
        {
            if (IsPlayForce)
            {
                return;
            }
            if (_activeSource.clip == newClip && _activeSource.isPlaying)
            {
                return;
            }
            if (_crossfadeCoroutine != null)
            {
                StopCoroutine(_crossfadeCoroutine);
            }
            _crossfadeCoroutine = StartCoroutine(CrossfadeBgmCoroutine(newClip));
        }

        private IEnumerator CrossfadeBgmCoroutine(AudioClip newClip)
        {
            AudioSource fadeInSource = (_activeSource == bgmSource1) ? bgmSource2 : bgmSource1;
            fadeInSource.clip = newClip;
            fadeInSource.volume = 0f;
            fadeInSource.loop = true;
            fadeInSource.Play();
            AudioSource fadeOutSource = _activeSource;
            _activeSource = fadeInSource;

            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float progress = elapsedTime / fadeDuration;
                fadeInSource.volume = Mathf.Lerp(0f, 1f, progress);
                fadeOutSource.volume = Mathf.Lerp(1f, 0f, progress);

                yield return null;
            }
            fadeOutSource.Stop();
            fadeOutSource.clip = null;
            _crossfadeCoroutine = null;
        }

        public void PlaySfx(AudioClip clip)
        {
            foreach (var source in _sfxSources)
            {
                if (!source.isPlaying)
                {
                    source.PlayOneShot(clip);
                    return;
                }
            }
        }
    }
}