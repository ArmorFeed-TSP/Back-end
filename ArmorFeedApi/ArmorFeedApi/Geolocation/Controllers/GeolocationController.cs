using ArmorFeedApi.Geolocation.Utils;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.WebSockets;
using System.Net;
using System.Text;

namespace ArmorFeedApi.Geolocation.Controllers
{
    public class GeolocationController: ControllerBase
    {
        private static GeolocationSocketManager _socketManager = new GeolocationSocketManager();
        private readonly ILogger _logger;

        public GeolocationController(ILogger<GeolocationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/ws/{connectionId}")]
        public async Task Get(string connectionId)
        {
            if (HttpContext.WebSockets.IsWebSocketRequest)
            {
                _logger.LogInformation("Socket connection was estabilshed successfully");
                WebSocket webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

                _socketManager.AddSocket(connectionId, webSocket);
                await ReceiveMessages(webSocket);

                _socketManager.RemoveSocket(connectionId);
            }
            else
            {
                _logger.LogError("Socket connection failed because connection request sended is not a valid web socket request");
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }
        private async Task ReceiveMessages(WebSocket webSocket)
        {
            byte[] buffer = new byte[1024];

            while (webSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text)
                {
                    string message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    // Analizar el mensaje JSON recibido
                    var parsedMessage = JObject.Parse(message);
                    string customerId = parsedMessage["customerId"].Value<string>();
                    string enterpriseId = parsedMessage["enterpriseId"].Value<string>();
                    double latitude = parsedMessage["data"]["latitude"].Value<double>();
                    double longitude = parsedMessage["data"]["longitude"].Value<double>();
                    double height = parsedMessage["data"]["height"].Value<double>();

                    LocationData locationData = new(latitude, longitude, height);

                    _logger.LogInformation("Data sent by client is latitude: {}, longitude: {}, height: {}, customerId: {}, enterpriseId: {}", latitude, longitude, height, customerId, enterpriseId);

                    await SendMessageToClient(customerId, locationData);
                    await SendMessageToClient(enterpriseId, locationData);

                }
                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                }
            }
        }
        private async Task SendMessageToClient(string connectionId, LocationData message)
        {
            await _socketManager.SendMessageAsync(connectionId, message);
        }
    }
    internal record LocationData(double Latitude, double Longitude, double Height);
}
