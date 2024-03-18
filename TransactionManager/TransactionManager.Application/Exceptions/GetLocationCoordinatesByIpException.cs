namespace TransactionManager.Application.Exceptions;

public class GetLocationCoordinatesByIpException : BusinessLogicException
{
    public GetLocationCoordinatesByIpException(string message) : base(message)
    {
    }
}