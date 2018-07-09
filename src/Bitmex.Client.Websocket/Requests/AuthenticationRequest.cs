using System.Net.Http;
using System.Net.Http.Headers;
using Bitmex.Client.Websocket.Messages;
using Bitmex.Client.Websocket.Utils;
using Bitmex.Client.Websocket.Validations;

namespace Bitmex.Client.Websocket.Requests
{
	public class AuthenticationRequest : RequestBase
	{
		private readonly string _apiKey;
		private readonly string _authSig;
		private readonly long _authExpires;
		private readonly HttpClient httpClient;

		public AuthenticationRequest(string apiKey, string apiSecret)
		{
			BmxValidations.ValidateInput(apiKey, nameof(apiKey));
			BmxValidations.ValidateInput(apiSecret, nameof(apiSecret));

			_apiKey = apiKey;

			var _url = BitmexAuthentication.BitmexUrl();
			var _authExpires = BitmexTime.GetUnixTime().ToString();

			_authSig = BitmexAuthentication.CreateSignature(apiSecret, $"{_url}{_authExpires}");

			httpClient.DefaultRequestHeaders.Add("api-expires", _authExpires);
			httpClient.DefaultRequestHeaders.Add("api-key", _apiKey);
			httpClient.DefaultRequestHeaders.Add("api-signature", _authSig);
			httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public override MessageType Operation => MessageType.AuthKey;

		public object[] Args => new object[]
		{
			_apiKey,
			_authExpires,
			_authSig,
			httpClient
		};
	}
}
