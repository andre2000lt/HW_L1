#region

using UnityEngine;

#endregion

namespace _Game.Develop.UI.MainMenu
{
    public class MainMenuUIRoot : MonoBehaviour
    {
        [field: SerializeField]
        public Transform HUDLayer { get; private set; }
        [field: SerializeField]
        public Transform UnderPopupsLayer { get; private set; }
        [field: SerializeField]
        public Transform PopupsLayer { get; private set; }
        [field: SerializeField]
        public Transform OverPopsupLayer { get; private set; }
    }
}