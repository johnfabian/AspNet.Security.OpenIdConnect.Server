/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using System.Collections.Generic;
using AspNet.Security.OpenIdConnect.Primitives;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Notifications;
using Owin.Security.OpenIdConnect.Extensions;

namespace Owin.Security.OpenIdConnect.Server
{
    /// <summary>
    /// Represents the context class associated with the
    /// <see cref="OpenIdConnectServerProvider.SerializeRefreshToken"/> event.
    /// </summary>
    public class SerializeRefreshTokenContext : BaseNotification<OpenIdConnectServerOptions>
    {
        /// <summary>
        /// Creates a new instance of the <see cref="SerializeRefreshTokenContext"/> class.
        /// </summary>
        public SerializeRefreshTokenContext(
            IOwinContext context,
            OpenIdConnectServerOptions options,
            OpenIdConnectRequest request,
            OpenIdConnectResponse response,
            AuthenticationTicket ticket)
            : base(context, options)
        {
            Request = request;
            Response = response;
            Ticket = ticket;
        }

        /// <summary>
        /// Gets the OpenID Connect request.
        /// </summary>
        public new OpenIdConnectRequest Request { get; }

        /// <summary>
        /// Gets the OpenID Connect response.
        /// </summary>
        public new OpenIdConnectResponse Response { get; }

        /// <summary>
        /// Gets the authentication ticket.
        /// </summary>
        public AuthenticationTicket Ticket { get; }

        /// <summary>
        /// Gets or sets the presenters associated with the authentication ticket.
        /// </summary>
        public IEnumerable<string> Presenters
        {
            get => Ticket.GetPresenters();
            set => Ticket.SetPresenters(value);
        }

        /// <summary>
        /// Gets or sets the data format used to serialize the authentication ticket.
        /// </summary>
        public ISecureDataFormat<AuthenticationTicket> DataFormat { get; set; }

        /// <summary>
        /// Gets or sets the refresh token returned to the client application.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
