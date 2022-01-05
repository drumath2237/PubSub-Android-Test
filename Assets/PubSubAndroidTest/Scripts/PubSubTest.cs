using System;
using System.Net.WebSockets;
using UnityEngine;
using Azure.Messaging.WebPubSub;
using PubSubAndroidTest.Scripts;
using TMPro;
using System.Threading.Tasks;

public class PubSubTest : MonoBehaviour
{
    private WebPubSubServiceClient client = null;

    private ClientWebSocket ws = null;

    [SerializeField] private WebPubSubConfig config;

    [SerializeField] private TextMeshProUGUI _logText;

    public event Action<string> OnMessage;

    private async void Start()
    {
        client = new WebPubSubServiceClient(config.ConnectionString, config.HubName);
        var url = await client.GetClientAccessUriAsync();

        ws = new ClientWebSocket();

        try
        {
            await ws.ConnectAsync(url, default);

            _logText.text = $"Connected status: {ws.State}";
        }
        catch (Exception e)
        {
            _logText.text = e.ToString();
            throw;
        }

        _ = ReceiveLoop();
    }

    private async Task ReceiveLoop()
    {
        const int maxBuffer = 4096;
        var receiveBuffer = new byte[maxBuffer];
        var arraySegment = new ArraySegment<byte>(receiveBuffer);


        while (ws.State == WebSocketState.Open)
        {
            var websocketRes = await ws.ReceiveAsync(arraySegment, default);

            _logText.text =
                $"MessageType:{websocketRes.MessageType}, EndOfMessage?:{websocketRes.EndOfMessage}, count:{websocketRes.Count}";
        }

        if (ws.State == WebSocketState.Closed)
        {
            if (ws.CloseStatus != null)
            {
                await ws.CloseAsync(ws.CloseStatus.Value, "", default);
            }
        }
    }

    public async void SendTextToAll()
    {
        if (client == null)
        {
            return;
        }

        try
        {
            await client.SendToAllAsync("Hello guys");
            Debug.Log("message sent");
        }
        catch (Exception e)
        {
            _logText.text = e.ToString();
            throw;
        }
    }

    private void OnDisable()
    {
        if (ws == null)
        {
            return;
        }

        ws.Dispose();
        ws = null;
    }
}