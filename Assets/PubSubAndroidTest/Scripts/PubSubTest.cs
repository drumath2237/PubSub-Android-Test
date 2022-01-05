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
    catch (System.Exception e)
    {
      _logText.text = e.ToString();
      throw;
    }


  }

  private async Task ReceiveLoop()
  {
    const int maxBuffer = 4096;
    byte[] receiveBUffer = new byte[maxBuffer];

    while (ws.State = WebSocketState.Open)
    {
      WebSocketReceiveResult websocketRes = await ws.ReceiveAsync(receiveBUffer, default);

      _logText.text = $"MessageType:{websocketRes.MessageType}, EndOfMessage?:{websocketRes.EndOfMessage}, count:{websocketRes.Count}";
    }
  }

  /// <summary>
  /// This function is called when the behaviour becomes disabled or inactive.
  /// </summary>
  private async void OnDisable()
  {
    if (ws == null)
    {
      return;
    }

    await ws.CloseAsync();
    ws.Dispose();
    ws = null;
  }
}