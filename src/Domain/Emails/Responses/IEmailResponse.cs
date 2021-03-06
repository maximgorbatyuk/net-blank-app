﻿using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Domain.Emails.Responses
{
    public interface IEmailResponse
    {
        HttpStatusCode StatusCode { get; }

        HttpContent Body { get; }

        HttpResponseHeaders Headers { get; }

        Task<Dictionary<string, dynamic>> BodyAsDictionaryAsync();
    }
}