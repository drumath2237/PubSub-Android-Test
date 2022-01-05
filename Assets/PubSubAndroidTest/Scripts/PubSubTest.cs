using System;
using System.Net.WebSockets;
using UnityEngine;
using Azure.Messaging.WebPubSub;
using PubSubAndroidTest.Scripts;
using TMPro;

public class PubSubTest : MonoBehaviour
{
    private WebPubSubServiceClient client = null;

    [SerializeField] private WebPubSubConfig config;

    [SerializeField] private TextMeshProUGUI _logText;
    
    private async void Start()
    {
        client = new WebPubSubServiceClient(config.ConnectionString, config.HubName);
        var url = await client.GetClientAccessUriAsync();

        using var ws = new ClientWebSocket();
        
        await ws.ConnectAsync(url, default);

        Debug.Log("Connected!");
        _logText.text = "Connected";

    }
}