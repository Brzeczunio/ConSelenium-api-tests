using Polly;
using RestSharp;
using System.Text;

namespace ConSelenium.Common.Tools
{
    public static class RetryPolicy
    {
        /// <summary>
        /// Execute REST request and examine the result. If result different than expected, an exception is thrown.
        /// </summary>
        /// <typeparam name="TResponse">Type to be handled. Where is IRestResponse</typeparam>
        /// <param name="request">REST request to be executed</param>
        /// <param name="verifyResult">Function to examine REST response</param>
        /// <param name="retryCount">Indicates how many times the operation should be retried.</param>
        /// <param name="intervalInMilliseconds">Interval between retry attempts.</param>
        /// <param name="errorMessage">Optional error message in case the REST response is not as expected.</param>
        /// <returns>TResponse</returns>
        public static async Task<TResponse> GetRestRequestResult<TResponse>(Func<Task<TResponse>> request, Func<TResponse, bool> verifyResult = null, int retryCount = 6,
            int intervalInMilliseconds = 100, string errorMessage = "") where TResponse : RestResponse
        {
            var policyResult = await GetPolicyResult(request, verifyResult, retryCount, intervalInMilliseconds);

            CheckPolicyResult(policyResult, retryCount, errorMessage);
            return policyResult.Result;
        }

        private static async Task<PolicyResult<TResponse>> GetPolicyResult<TResponse>(Func<Task<TResponse>> request, Func<TResponse, bool> verifyResult = null, int retryCount = 6,
            int intervalInMilliseconds = 100)
        {
            return await Policy
                .HandleResult<TResponse>(result => !verifyResult?.Invoke(result) ?? false)
                .WaitAndRetryAsync(retryCount, retryNumber => TimeSpan.FromMilliseconds(intervalInMilliseconds))
                .ExecuteAndCaptureAsync(request);
        }

        private static string BuildErrorMessageForIRestResponse<TResponse>(PolicyResult<TResponse> policyResult, string customMessage = "") where TResponse : RestResponse
        {
            var message = new StringBuilder("Request failed.");
            if (policyResult?.FinalHandledResult?.ResponseUri != null && !string.IsNullOrWhiteSpace(policyResult?.FinalHandledResult?.ResponseUri.ToString()))
            {
                message.Append(Environment.NewLine + "Url: " + policyResult.FinalHandledResult.ResponseUri);
            }

            if (!string.IsNullOrWhiteSpace(policyResult?.FinalHandledResult?.StatusCode.ToString()))
            {
                message.Append(Environment.NewLine + "Status code: " + policyResult.FinalHandledResult.StatusCode);
            }

            if (!string.IsNullOrWhiteSpace(policyResult?.FinalHandledResult?.ErrorMessage))
            {
                message.Append(Environment.NewLine + "Error message: " + policyResult.FinalHandledResult.ErrorMessage);
            }

            if (!string.IsNullOrWhiteSpace(policyResult?.FinalException?.Message))
            {
                message.Append(Environment.NewLine + "Exception: " + policyResult.FinalException.Message);
            }

            if (!string.IsNullOrWhiteSpace(customMessage))
            {
                message.Append(Environment.NewLine + "Custom message: " + customMessage);
            }

            return message.ToString();
        }

        private static void ThrowException<TResponse>(int retryCount, string errorMessage, PolicyResult<TResponse> policyResult = null)
        {
            var message = new StringBuilder($"Retries exhausted after {retryCount} attempts.");
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                message.Append(Environment.NewLine + "Details: " + errorMessage);
            }

            if (!string.IsNullOrWhiteSpace(policyResult?.FinalException?.Message))
            {
                message.Append(Environment.NewLine + "Exception: " + policyResult.FinalException.Message);
            }

            throw new Exception(message.ToString());
        }

        private static void CheckPolicyResult<TResponse>(PolicyResult<TResponse> policyResult, int retryCount, string errorMessage) where TResponse : RestResponse
        {
            if (policyResult.Outcome != OutcomeType.Successful)
                ThrowException(retryCount, BuildErrorMessageForIRestResponse(policyResult, errorMessage), policyResult);
        }
    }
}
