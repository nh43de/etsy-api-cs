namespace EtsyApi
{
    public class EtsyApiAuth
    {
        /// <summary>
        /// Only required field - can be used alone to access the Etsy API, "unauthenticated". I.e. no mutating effects, can query listings.
        /// Generated from Etsy API developer page.
        /// </summary>
        public string ConsumerKey { get; set; }
        
        /// <summary>
        /// Generated from Etsy API developer page.
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// Temporary access token.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Temporary token secret.
        /// </summary>
        public string TokenSecret { get; set; }

        /// <summary>
        /// Token verifier. Obtained via callback/textbox on etsy site after authorizing the login.
        /// </summary>
        public string VerifierSecret { get; set; }

        
        /// <summary>
        /// Etsy account-specific OAuth token that grants access to private info and side-effecting operations.
        /// </summary>
        public string OAuthTokenSecret { get; set; }

        /// <summary>
        /// Etsy account-specific OAuth token that grants access to private info and side-effecting operations.
        /// </summary>
        public string OAuthToken { get; set; }
    }
}
