using System;
using System.IO;
using System.Net.WebSockets;
using System.Threading;
using UnityEngine;
using Azure.Messaging.WebPubSub;
using PubSubAndroidTest.Scripts;
using TMPro;
using System.Threading.Tasks;
using Azure.Core;
using Azure.Core.Serialization;

[Serializable]
public class TempData
{
    public string data;
}

class MySerializer : ObjectSerializer
{
    public override void Serialize(Stream stream, object? value, Type inputType, CancellationToken cancellationToken)
    {
        var stringValue = JsonUtility.ToJson(value);
        var byteArray = System.Text.Encoding.UTF8.GetBytes(stringValue);
        new MemoryStream(byteArray).CopyTo(stream);
    }


    public override ValueTask SerializeAsync(Stream stream, object? value, Type inputType,
        CancellationToken cancellationToken)
    {
        Debug.Log("fooooooooooo");
        throw new NotImplementedException();
    }

    public override object? Deserialize(Stream stream, Type returnType, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public override ValueTask<object?> DeserializeAsync(Stream stream, Type returnType,
        CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

public class PubSubTest : MonoBehaviour
{
    private WebPubSubServiceClient client = null;

    private ClientWebSocket ws = null;

    [SerializeField] private WebPubSubConfig config;

    [SerializeField] private TextMeshProUGUI _logText;

    [SerializeField] private TextMeshProUGUI _socketStatusText;


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

    private void Update()
    {
        _socketStatusText.text = ws.State.ToString();
    }

    private async Task ReceiveLoop()
    {
        const int maxBuffer = 1024;
        var receiveBuffer = new byte[maxBuffer];
        var arraySegment = new ArraySegment<byte>(receiveBuffer);


        while (ws.State == WebSocketState.Open)
        {
            try
            {
                var websocketRes = await ws.ReceiveAsync(arraySegment, CancellationToken.None);

                _logText.text =
                    $"MessageType:{websocketRes.MessageType}, count:{websocketRes.Count}, time:{DateTime.Now}";

                if (arraySegment.Array == null)
                {
                    continue;
                }

                Debug.Log(System.Text.Encoding.UTF8.GetString(arraySegment.Array));
            }
            catch (Exception e)
            {
                _logText.text = e.ToString();
                throw;
            }
        }

        // if (ws.State == WebSocketState.Closed)
        // {
        //     if (ws.CloseStatus != null)
        //     {
        //         await ws.CloseAsync(ws.CloseStatus.Value, "", default);
        //     }
        // }
    }

    public async void SendTextToAll()
    {
        if (client == null)
        {
            return;
        }

        try
        {
            var body = JsonUtility.ToJson(new { a = 1 });

            var tempData = new TempData { data = "hellooooo" };

            // await client.SendToAllAsync(RequestContent.Create(new { a = 1 }), ContentType.ApplicationJson);
            await client.SendToAllAsync(RequestContent.Create(tempData, new MySerializer()), ContentType.ApplicationOctetStream);

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