using UnityEngine;
using UnityEngine.UI;

namespace Nature
{
	public class UIManager : MonoBehaviour
	{
		static UIManager s_Instance;

		[SerializeField] Text m_txtPlaceHolder;
		[SerializeField] GameObject m_uiBlocker;

		bool m_uiblockerState =  false;
		bool UIBlockerEnableState
		{
			get => m_uiblockerState;
			set
			{
				m_uiBlocker.SetActive(value);
				m_uiblockerState = value;
			}
		}

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

		public void OnBtnClicked_Product()
		{
			UIBlockerEnableState= true;
			AudioManager.StopCurrentAudio();
			APIHandler.GetData_MenProducts(OnSuccess_MenProduct, OnFail);
		}
		public void OnBtnClicked_Explore()
		{
			UIBlockerEnableState = true;
			AudioManager.StopCurrentAudio();
			APIHandler.GetData_MenExplore(OnSuccess_Explore, OnFail);
		}
		public void OnBtnClicked_Collection()
		{
			UIBlockerEnableState = true;
			AudioManager.StopCurrentAudio();
			APIHandler.GetData_AboutCollection(OnSuccess_Collection, OnFail);
		}

		void OnSuccess_MenProduct(Root_Product a_successData)
		{
			//Debug.Log(a_successData.placeholder);
			m_txtPlaceHolder.text = a_successData.placeholder;
			APIHandler.DownloadAudio(a_successData.response_channels.voice, OnSuccess_AudioDownload, OnFail_AudioDownload);
		}
		void OnSuccess_Explore(Root_Explore a_successData)
		{
			//Debug.Log(a_successData.placeholder);
			m_txtPlaceHolder.text = a_successData.placeholder;
			APIHandler.DownloadAudio(a_successData.response_channels.voice, OnSuccess_AudioDownload, OnFail_AudioDownload);
		}
		void OnSuccess_Collection(Root_Collections a_successData)
		{
			//Debug.Log(a_successData.placeholder);
			m_txtPlaceHolder.text = a_successData.placeholder;
			APIHandler.DownloadAudio(a_successData.response_channels.voice, OnSuccess_AudioDownload, OnFail_AudioDownload);
		}
		void OnFail()
		{
			Debug.Log("Error Getting API Hit!");
		}
		void OnSuccess_AudioDownload(AudioClip a_audioClip)
		{
			UIBlockerEnableState = false;
			AudioManager.PlayAudio(a_audioClip);
		}
		void OnFail_AudioDownload()
		{
			UIBlockerEnableState = false;
		}
	}
}
