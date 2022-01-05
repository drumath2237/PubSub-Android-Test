using System;
using System.Net.WebSockets;
using UnityEngine;
using Azure.Messaging.WebPubSub;
using PubSubAndroidTest.Scripts;
using TMPro;

public class PubSubTest : MonoBehaviour
{
  private WebPubSubServiceClient client = null;

  private ClientWebSocket ws = null;

  [SerializeField] private WebPubSubConfig config;

  [SerializeField] private TextMeshProUGUI _logText;

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