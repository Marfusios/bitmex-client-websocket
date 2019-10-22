using Bitmex.Client.Websocket.Client;
using Bitmex.Client.Websocket.Json;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;
using Bitmex.Client.Websocket.Responses.Executions;
using Bitmex.Client.Websocket.Responses.Fundings;
using Bitmex.Client.Websocket.Responses.Instruments;
using Bitmex.Client.Websocket.Responses.Liquidation;
using Bitmex.Client.Websocket.Responses.Margins;
using Bitmex.Client.Websocket.Responses.Orders;
using Bitmex.Client.Websocket.Responses.Positions;
using Bitmex.Client.Websocket.Responses.Quotes;
using Bitmex.Client.Websocket.Responses.TradeBins;
using Bitmex.Client.Websocket.Responses.Trades;
using Bitmex.Client.Websocket.Responses.Wallets;
using Newtonsoft.Json.Linq;

namespace Bitmex.Client.Websocket.Utils
{
    static class BitmexResponseHandler
    {
        /// <summary>
        /// Handles raw messages sent by bitmex.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="streams">The streams.</param>
        /// <returns></returns>
        public static bool HandleRawMessage(string msg, BitmexClientStreams streams)
        {
            // ********************
            // ADD RAW HANDLERS BELOW
            // ********************

            return
                PongResponse.TryHandle(msg, streams.PongSubject);
        }

        /// <summary>
        /// Handles object messages sent by bitmex.
        /// </summary>
        /// <param name="msg">The MSG.</param>
        /// <param name="streams">The streams.</param>
        /// <returns></returns>
        public static bool HandleObjectMessage(string msg, BitmexClientStreams streams)
        {
            var response = BitmexJsonSerializer.Deserialize<JObject>(msg);

            // ********************
            // ADD OBJECT HANDLERS BELOW
            // ********************

            return
                TradeResponse.TryHandle(response, streams.TradesSubject) ||
                TradeBinResponse.TryHandle(response, streams.TradeBinSubject) ||
                BookResponse.TryHandle(response, streams.BookSubject) ||
                QuoteResponse.TryHandle(response, streams.QuoteSubject) ||
                LiquidationResponse.TryHandle(response, streams.LiquidationSubject) ||
                PositionResponse.TryHandle(response, streams.PositionSubject) ||
                MarginResponse.TryHandle(response, streams.MarginSubject) ||
                OrderResponse.TryHandle(response, streams.OrderSubject) ||
                WalletResponse.TryHandle(response, streams.WalletSubject) ||
                InstrumentResponse.TryHandle(response, streams.InstrumentSubject) ||
                ExecutionResponse.TryHandle(response, streams.ExecutionSubject) ||
                FundingResponse.TryHandle(response, streams.FundingsSubject) ||


                ErrorResponse.TryHandle(response, streams.ErrorSubject) ||
                SubscribeResponse.TryHandle(response, streams.SubscribeSubject) ||
                InfoResponse.TryHandle(response, streams.InfoSubject) ||
                AuthenticationResponse.TryHandle(response, streams.AuthenticationSubject);
        }
    }
}
