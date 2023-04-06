﻿using System.Text.Json.Serialization;

namespace JSpotifyClient.Types;

public sealed class Tracks
{
    [JsonPropertyName("href")]
    public string Href { get; set; }

    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    [JsonPropertyName("next")]
    public string Next { get; set; }

    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    [JsonPropertyName("previous")]
    public string Previous { get; set; }

    [JsonPropertyName("total")]
    public int Total { get; set; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; }
}