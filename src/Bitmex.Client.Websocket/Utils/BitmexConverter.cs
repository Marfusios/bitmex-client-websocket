using System;
using Bitmex.Client.Websocket.Exceptions;

namespace Bitmex.Client.Websocket.Utils;

/// <summary>
/// Utility class to help with converting Satoshis, miliBTC into BTC
/// </summary>
public static class BitmexConverter
{
    /// <summary>
    /// Convert double value into BTC, according to from currency.
    /// For example: 'XBt' will be converted like value/10^8
    /// </summary>
    /// <param name="from">Currency (XBt, XBT, BTC, etc)</param>
    /// <param name="value">Original value</param>
    /// <returns>Converted value</returns>
    public static double ConvertToBtc(string from, double value)
    {
        var safe = (from ?? string.Empty).Trim();


        if (safe == "XBt")
        {
            return ConvertFromSatoshiToBtc(value);
        }

        if (string.IsNullOrWhiteSpace(safe) || safe.ToLower() == "btc" || safe.ToLower() == "xbt")
            return value;

        throw new BitmexException($"Can't convert from '{safe}' to BTC (double)");
    }

    /// <summary>
    /// Convert decimal value into BTC, according to from currency.
    /// For example: 'XBt' will be converted like value/10^8
    /// </summary>
    /// <param name="from">Currency (XBt, XBT, BTC, etc)</param>
    /// <param name="value">Original value</param>
    /// <returns>Converted value</returns>
    public static decimal ConvertToBtc(string from, decimal value)
    {
        var safe = (from ?? string.Empty).Trim();


        if (safe == "XBt")
        {
            return ConvertFromSatoshiToBtcDecimal(value);
        }

        if (string.IsNullOrWhiteSpace(safe) || safe.ToLower() == "btc" || safe.ToLower() == "xbt")
            return value;

        throw new BitmexException($"Can't convert from '{safe}' to BTC (decimal)");
    }

    /// <summary>
    /// Convert long value into BTC, according to from currency.
    /// For example: 'XBt' will be converted like value/10^8
    /// </summary>
    /// <param name="from">Currency (XBt, XBT, BTC, etc)</param>
    /// <param name="value">Original value</param>
    /// <returns>Converted value</returns>
    public static double ConvertToBtc(string from, long value)
    {
        var valueDouble = Convert.ToDouble(value);
        return ConvertToBtc(from, valueDouble);
    }

    /// <summary>
    /// Convert long value into BTC, according to from currency.
    /// For example: 'XBt' will be converted like value/10^8
    /// </summary>
    /// <param name="from">Currency (XBt, XBT, BTC, etc)</param>
    /// <param name="value">Original value</param>
    /// <returns>Converted value</returns>
    public static decimal ConvertToBtcDecimal(string from, long value)
    {
        var valueDouble = Convert.ToDecimal(value);
        return ConvertToBtc(from, valueDouble);
    }

    /// <summary>
    /// Convert Satoshis into BTC. It will divide value by 10^8
    /// </summary>
    public static double ConvertFromSatoshiToBtc(long satoshi)
    {
        return satoshi / 100000000.0;
    }

    /// <summary>
    /// Convert Satoshis into BTC. It will divide value by 10^8
    /// </summary>
    public static double ConvertFromSatoshiToBtc(double satoshi)
    {
        return satoshi / 100000000.0;
    }

    /// <summary>
    /// Convert Satoshis into BTC. It will divide value by 10^8
    /// </summary>
    public static decimal ConvertFromSatoshiToBtcDecimal(long satoshi)
    {
        return satoshi / 100000000.0m;
    }

    /// <summary>
    /// Convert Satoshis into BTC. It will divide value by 10^8
    /// </summary>
    public static decimal ConvertFromSatoshiToBtcDecimal(decimal satoshi)
    {
        return satoshi / 100000000.0m;
    }
}