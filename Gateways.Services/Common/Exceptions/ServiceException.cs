
namespace Gateways.Services.Common.Exceptions
{
    /// <summary>
    /// Exception used in all Service related classes
    /// </summary>
    public class ServiceException : Exception
    {
        /// <summary>
        /// Main ctor
        /// </summary>
        /// <param name="message">see Exception message</param>
        /// <param name="innerException">see Exception innerException</param>
        public ServiceException(string? message = null, Exception? innerException = null) : base
            (message, innerException)
        { }
    }
}
