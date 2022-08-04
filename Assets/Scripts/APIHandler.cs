using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Nature
{
	public class APIHandler : MonoBehaviour
	{
		static APIHandler s_Instance;

		#region Serialized Fields
		[SerializeField] string m_Url;
		[SerializeField] string m_enterpriseId;
		[SerializeField] string m_userID;
		[SerializeField] string m_ApiKey;
		[SerializeField] string m_formatRawData;
		#endregion

		#region Constants
		const string LABEL__CONTENT_TYPE = "Content-Type";
		const string LABEL__ENTERPRISE_ID = "X-I2CE-ENTERPRISE-ID";
		const string LABEL__USER_ID = "X-I2CE-USER-ID";
		const string LABEL__API_KEY = "X-I2CE-API-KEY";

		const string VALUE__CONTENT_TYPE_JSON = "application/json";

		const string VALUE__CS__MEN_PRODUCTS = "cs_men_products";
		const string VALUE__CS__MEN_EXPLORE = "cs_men_explore";
		const string VALUE__CS__ABOUT_COLLECTION = "cs_about_collection";

		#endregion

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

		#region Static Methods
		public static void GetData_MenExplore(Action<Root_Explore> a_OnSuccess, Action a_OnFail) => s_Instance.Internal_GetData_MenExplore(a_OnSuccess, a_OnFail);
		public static void GetData_MenProducts(Action<Root_Product> a_OnSuccess, Action a_OnFail) => s_Instance.Internal_GetData_MenProducts(a_OnSuccess, a_OnFail);
		public static void GetData_AboutCollection(Action<Root_Collections> a_OnSuccess, Action a_OnFail) => s_Instance.Internal_GetData_AboutCollection(a_OnSuccess, a_OnFail);
		public static void DownloadAudio(string a_audioUrl, Action<AudioClip> a_OnSuccess, Action a_OnFail) => s_Instance.Internal_DownloadAudio(a_audioUrl, a_OnSuccess, a_OnFail);

		#endregion

		void Internal_GetData_MenExplore(Action<Root_Explore> a_OnSuccess, Action a_OnFail)
		{
			StartCoroutine(Coroutine_GetData_MenExplore(GetRawDataString(VALUE__CS__MEN_EXPLORE), a_OnSuccess, a_OnFail));
		}
		void Internal_GetData_MenProducts(Action<Root_Product> a_OnSuccess, Action a_OnFail)
		{
			StartCoroutine(Coroutine_GetData_MenProducts(GetRawDataString(VALUE__CS__MEN_PRODUCTS), a_OnSuccess, a_OnFail));
		}
		void Internal_GetData_AboutCollection(Action<Root_Collections> a_OnSuccess, Action a_OnFail)
		{
			StartCoroutine(Coroutine_GetData_AboutCollection(GetRawDataString(VALUE__CS__ABOUT_COLLECTION), a_OnSuccess, a_OnFail));
		}
		void Internal_DownloadAudio(string a_audioUrl, Action<AudioClip> a_OnSuccess, Action a_OnFail)
		{
			StartCoroutine(Coroutine_DownloadAudio(a_audioUrl, a_OnSuccess, a_OnFail));
		}

		string GetRawDataString(string a_customerState)
		{
			return "{ \"system_response\": \"sr_init\",\"engagement_id\":\"NzQ3MTBjNTItNDJhNS0zZTY1LWIxZjAtMmRjMzllYmU0MmMyZXhoaWJpdF9hbGRv\",\"customer_state\" : \"" + a_customerState + "\"}";
		}
		UnityWebRequest CreateWebRequestBase(string a_rawData)
		{
			WWWForm l_form = new WWWForm();

			UnityWebRequest l_uwr = UnityWebRequest.Post(m_Url, l_form);

			//set header
			l_uwr.SetRequestHeader(LABEL__CONTENT_TYPE, VALUE__CONTENT_TYPE_JSON);
			l_uwr.SetRequestHeader(LABEL__ENTERPRISE_ID, m_enterpriseId);
			l_uwr.SetRequestHeader(LABEL__USER_ID, m_userID);
			l_uwr.SetRequestHeader(LABEL__API_KEY, m_ApiKey);

			//set body : raw
			byte[] l_rawData = Encoding.UTF8.GetBytes(a_rawData);
			l_uwr.uploadHandler = new UploadHandlerRaw(l_rawData);
			l_uwr.downloadHandler = new DownloadHandlerBuffer();

			return l_uwr;
		}

		IEnumerator Coroutine_GetData_MenProducts(string a_rawData, Action<Root_Product> a_OnSuccess, Action a_OnFail)
		{
			UnityWebRequest l_uwr = CreateWebRequestBase(a_rawData);

			yield return l_uwr.SendWebRequest();

			switch (l_uwr.result)
			{
				case UnityWebRequest.Result.Success:
					Root_Product myDeserializedClass = JsonConvert.DeserializeObject<Root_Product>(l_uwr.downloadHandler.text);
					a_OnSuccess?.Invoke(myDeserializedClass);
					break;
				case UnityWebRequest.Result.InProgress:
					break;
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.ProtocolError:
				case UnityWebRequest.Result.DataProcessingError:
					a_OnFail?.Invoke();
					break;
				default:
					Debug.Log("Unknown Error!");
					break;
			}
		}
		IEnumerator Coroutine_GetData_MenExplore(string a_rawData, Action<Root_Explore> a_OnSuccess, Action a_OnFail)
		{
			UnityWebRequest l_uwr = CreateWebRequestBase(a_rawData);

			yield return l_uwr.SendWebRequest();

			switch (l_uwr.result)
			{
				case UnityWebRequest.Result.Success:
					Root_Explore myDeserializedClass = JsonConvert.DeserializeObject<Root_Explore>(l_uwr.downloadHandler.text);
					a_OnSuccess?.Invoke(myDeserializedClass);
					break;
				case UnityWebRequest.Result.InProgress:
					break;
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.ProtocolError:
				case UnityWebRequest.Result.DataProcessingError:
					a_OnFail?.Invoke();
					break;
				default:
					Debug.Log("Unknown Error!");
					break;
			}
		}
		IEnumerator Coroutine_GetData_AboutCollection(string a_rawData, Action<Root_Collections> a_OnSuccess, Action a_OnFail)
		{
			UnityWebRequest l_uwr = CreateWebRequestBase(a_rawData);

			yield return l_uwr.SendWebRequest();

			switch (l_uwr.result)
			{
				case UnityWebRequest.Result.Success:
					Root_Collections myDeserializedClass = JsonConvert.DeserializeObject<Root_Collections>(l_uwr.downloadHandler.text);
					a_OnSuccess?.Invoke(myDeserializedClass);
					break;
				case UnityWebRequest.Result.InProgress:
					break;
				case UnityWebRequest.Result.ConnectionError:
				case UnityWebRequest.Result.ProtocolError:
				case UnityWebRequest.Result.DataProcessingError:
					a_OnFail?.Invoke();
					break;
				default:
					Debug.Log("Unknown Error!");
					break;
			}
		}



		
		IEnumerator Coroutine_DownloadAudio(string a_audioUrl, Action<AudioClip> a_OnSuccess, Action a_OnFail)
		{
			using (UnityWebRequest l_uwr = UnityWebRequestMultimedia.GetAudioClip(a_audioUrl, AudioType.WAV))
			{
				yield return l_uwr.SendWebRequest();
				if (l_uwr.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError(l_uwr.error);
					a_OnFail?.Invoke();
					yield break;
				}

				AudioClip l_audio = DownloadHandlerAudioClip.GetContent(l_uwr);
				a_OnSuccess?.Invoke(l_audio);
			}
		}
	}
}