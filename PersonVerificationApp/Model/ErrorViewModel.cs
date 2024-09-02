namespace PersonVerificationApp.Model
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorMessage { get; set; }

        public string StackTrace { get; set; }

        public int ErrorCode { get; set; }

        // Default constructor
        public ErrorViewModel() { }

        // Constructor to initialize with error message and code
        public ErrorViewModel(string errorMessage, int errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        // Constructor to initialize with error message, code, and stack trace
        public ErrorViewModel(string errorMessage, int errorCode, string stackTrace)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            StackTrace = stackTrace;
        }
    }

}
