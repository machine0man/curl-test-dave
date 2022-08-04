using UnityEngine;

namespace Nature
{
	public class MainManager : MonoBehaviour
	{
		static MainManager s_Instance;

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
	}
}
