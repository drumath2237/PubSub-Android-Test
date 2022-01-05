using UnityEngine;

namespace PubSubAndroidTest.Scripts
{
    [CreateAssetMenu(fileName = "PubSubConfig", menuName = "PubSubTest/Config", order = 0)]
    public class WebPubSubConfig : ScriptableObject
    {
        public string ConnectionString;
        public string HubName;
    }
}