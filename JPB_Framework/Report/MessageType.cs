namespace JPB_Framework.Report
{
    /// <summary>
    /// Enlists all types of message that are logged in report file
    /// </summary>
    public enum MessageType
    {
        AssertionError,   // for assertion log messages
        VerificationError,  // for verification log messages
        Exception,      // for exception thrown log messages
        Message,        // for general purpose messages
        Empty           // Empty string
    }
}