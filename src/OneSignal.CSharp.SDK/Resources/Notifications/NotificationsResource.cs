﻿using System;
using System.Net;
using OneSignal.CSharp.SDK.Serializers;
using RestSharp;

namespace OneSignal.CSharp.SDK.Resources.Notifications
{
    /// <summary>
    /// Class used to define resources needed for client to manage notifications.
    /// </summary>
    public class NotificationsResource : BaseResource, INotificationsResource
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="apiKey">Your OneSignal API key</param>
        /// <param name="apiUri">API uri (https://onesignal.com/api/v1/notifications)</param>
        public NotificationsResource(string apiKey, string apiUri) : base(apiKey, apiUri)
        {
        }

        /// <summary>
        /// Creates new notification to be sent by OneSignal system.
        /// </summary>
        /// <param name="options">Options used for notification create operation.</param>
        /// <returns></returns>
        public NotificationCreateResult Create(NotificationCreateOptions options)
        {
            RestRequest restRequest = new RestRequest("notifications", Method.POST);

            restRequest.AddHeader("Authorization", string.Format("Basic {0}", base.ApiKey));

            restRequest.RequestFormat = DataFormat.Json;
            restRequest.JsonSerializer = new NewtonsoftJsonSerializer();
            restRequest.AddBody(options);

            IRestResponse<NotificationCreateResult> restResponse = base.RestClient.Execute<NotificationCreateResult>(restRequest);

            if (restResponse.ErrorException != null)
            {
                throw restResponse.ErrorException;
            }
            else if (restResponse.StatusCode != HttpStatusCode.OK && restResponse.Content != null)
            {
                throw new Exception(restResponse.Content);
            }

            return restResponse.Data;
        }
    }
}
