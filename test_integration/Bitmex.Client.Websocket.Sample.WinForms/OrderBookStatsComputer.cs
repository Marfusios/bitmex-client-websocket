using System;
using System.Collections.Generic;
using System.Linq;
using Bitmex.Client.Websocket.Responses;
using Bitmex.Client.Websocket.Responses.Books;

namespace Bitmex.Client.Websocket.Sample.WinForms
{
    class OrderBookStatsComputer
    {
        private readonly Dictionary<long, BookLevel> _bids = new Dictionary<long, BookLevel>();
        private readonly Dictionary<long, BookLevel> _asks = new Dictionary<long, BookLevel>();


        public void HandleOrderBook(BookResponse response)
        {
            if (response.Action == BitmexAction.Delete)
            {
                foreach (var bookLevel in response.Data)
                {
                    RemoveBook(bookLevel);
                }
            }

            if (response.Action == BitmexAction.Insert ||
                response.Action == BitmexAction.Partial)
            {
                foreach (var bookLevel in response.Data)
                {
                    InsertNewBook(bookLevel);
                }
            }

            if (response.Action == BitmexAction.Update)
            {
                foreach (var bookLevel in response.Data)
                {
                    UpdateBook(bookLevel);
                }
            }
        }

        public OrderBookStats GetStats()
        {
            var bids = _bids.OrderByDescending(x => x.Value.Price).ToArray();
            var asks = _asks.OrderBy(x => x.Value.Price).ToArray();

            if(!bids.Any() || !asks.Any())
                return OrderBookStats.NULL;

            var bidAmounts = bids.Take(20).Sum(x => x.Value.Size) ?? 1;
            var askAmounts = asks.Take(20).Sum(x => x.Value.Size) ?? 1;

            var total = bidAmounts + askAmounts + 0.0;

            var bidsPerc = bidAmounts / total * 100;
            var asksPerc = askAmounts / total * 100;

            return new OrderBookStats(
                bids[0].Value.Price ?? 0,
                asks[0].Value.Price ?? 0,
                bidsPerc,
                asksPerc
                );
        }

        private void InsertNewBook(BookLevel book)
        {
            var id = book.Id;

            if (book.Side == BitmexSide.Buy)
            {
                _bids[id] = book;
                return;
            }

            _asks[id] = book;
        }

        private void RemoveBook(BookLevel book)
        {
            var id = book.Id;
            if (_bids.ContainsKey(id))
                _bids.Remove(id);
            if (_asks.ContainsKey(id))
                _asks.Remove(id);
        }

        private void UpdateBook(BookLevel book)
        {
            var id = book.Id;
            BookLevel found = null;
            if (_bids.ContainsKey(id))
                found = _bids[id];
            if (_asks.ContainsKey(id))
                found = _asks[id];

            if (found == null)
                return;

            found.Size = book.Size;
        }
    }

    class OrderBookStats
    {
        public static readonly OrderBookStats NULL = new OrderBookStats(0, 0, 0, 0);

        public OrderBookStats(double bid, double ask, double bidAmountPerc, double askAmountPerc)
        {
            Bid = bid;
            Ask = ask;
            BidAmountPerc = bidAmountPerc;
            AskAmountPerc = askAmountPerc;
        }

        public double Bid { get; }
        public double Ask { get; }

        public double BidAmountPerc { get; }
        public double AskAmountPerc { get; }
    }
}
