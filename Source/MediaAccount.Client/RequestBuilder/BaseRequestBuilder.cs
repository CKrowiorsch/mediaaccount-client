﻿using System;
using System.Net.Http;

namespace Krowiorsch.MediaAccount.RequestBuilder
{
    public class BaseRequestBuilder
    {
        protected Uri _endpoint;
        string _apiKey;
        string _userAgent;

        protected BaseRequestBuilder(Uri endpoint, string apiKey)
        {
            _endpoint = endpoint;
            _apiKey = apiKey;
            _userAgent = string.Format("MediaAccountClient ({0})", GetType().Assembly.GetName().Version);
        }


        protected HttpRequestMessage CreateGet()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, _endpoint);
            message.Headers.Add("api_key", _apiKey);
            message.Headers.Add("User-Agent", _userAgent);
            message.Headers.Add("Accept", "application/json");

            return message;
        }
    }
}