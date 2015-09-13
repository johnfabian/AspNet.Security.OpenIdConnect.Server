/*
 * Licensed under the Apache License, Version 2.0 (http://www.apache.org/licenses/LICENSE-2.0)
 * See https://github.com/aspnet-contrib/AspNet.Security.OpenIdConnect.Server
 * for more information concerning the license and the contributors participating to this project.
 */

using System.Diagnostics.CodeAnalysis;
using Microsoft.Owin;
using Microsoft.Owin.Security.Notifications;

namespace Owin.Security.OpenIdConnect.Server {
    /// <summary>
    /// Base class used for certain event contexts.
    /// </summary>
    public abstract class BaseValidatingContext<TOptions> : BaseNotification<TOptions> {
        /// <summary>
        /// Initializes base class used for certain event contexts.
        /// </summary>
        protected BaseValidatingContext(
            IOwinContext context,
            TOptions options)
            : base(context, options) {
        }

        /// <summary>
        /// True if application code has called any of the
        /// <see cref="Validated"/> methods on this context.
        /// </summary>
        public bool IsValidated { get; private set; }

        /// <summary>
        /// The error argument provided when Rejected was called on this context.
        /// This is eventually returned to the client app as the OAuth2 "error" parameter.
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// The optional description argument provided when Rejected was called on this context.
        /// This is eventually returned to the client app as the OAuth2 "error_description" parameter.
        /// </summary>
        public string ErrorDescription { get; private set; }

        /// <summary>
        /// The optional uri argument provided when Rejected was called on this context.
        /// This is eventually returned to the client app as the OpenIdConnect "error_uri" parameter.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings",
            Justification = "error_uri is a string value in the protocol")]
        public string ErrorUri { get; private set; }

        /// <summary>
        /// Marks this context as validated by the application.
        /// </summary>
        /// <returns>True if the validation has taken effect.</returns>
        public virtual bool Validated() {
            IsValidated = true;
            return true;
        }

        /// <summary>
        /// Marks this context as not validated by the application.
        /// </summary>
        public virtual void Rejected() {
            Rejected(error: null, description: null, uri: null);
        }

        /// <summary>
        /// Marks this context as not validated by the application
        /// and assigns various error information properties.
        /// </summary>
        /// <param name="error">Assigned to the <see cref="Error"/> property.</param>
        public virtual void Rejected(string error) {
            Rejected(error, description: null, uri: null);
        }

        /// <summary>
        /// Marks this context as not validated by the application
        /// and assigns various error information properties.
        /// </summary>
        /// <param name="error">Assigned to the <see cref="Error"/> property.</param>
        /// <param name="description">Assigned to the <see cref="ErrorDescription"/> property.</param>
        public virtual void Rejected(string error, string description) {
            Rejected(error, description, uri: null);
        }

        /// <summary>
        /// Marks this context as not validated by the application
        /// and assigns various error information properties.
        /// </summary>
        /// <param name="error">Assigned to the <see cref="Error"/> property</param>
        /// <param name="description">Assigned to the <see cref="ErrorDescription"/> property</param>
        /// <param name="uri">Assigned to the <see cref="ErrorUri"/> property</param>
        [SuppressMessage("Microsoft.Design", "CA1054:UriParametersShouldNotBeStrings",
            MessageId = "2#", Justification = "error_uri is a string value in the protocol")]
        public virtual void Rejected(string error, string description, string uri) {
            IsValidated = false;
            Error = error;
            ErrorDescription = description;
            ErrorUri = uri;
        }
    }
}