using UnityEngine;

namespace Nature
{
    public class AudioManager : MonoBehaviour
    {
		static AudioManager s_Instance;

		[SerializeField] AudioSource m_audioSource;

		#region Unity Methods
		void Start()
		{
			s_Instance = this;
		}
		private void OnDestroy()
		{
			s_Instance = null;
		}
		#endregion

		public static void PlayAudio(AudioClip a_audioClip)
		{
			s_Instance.m_audioSource.clip = a_audioClip;
			s_Instance.m_audioSource.Play();
		}
		public static void StopCurrentAudio()
		{
			s_Instance.m_audioSource.Stop();
		}
	}
}   
