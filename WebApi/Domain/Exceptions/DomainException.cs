namespace Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException( string message ) : base( message )
    {
    }
}

public class NotFoundException : DomainException
{
    public NotFoundException( string entityName, Guid id )
        : base( $"{entityName} with id '{id}' not found" )
    {
    }
}

public class ValidationDomainException : DomainException
{
    public ValidationDomainException( string message ) : base( message )
    {
    }
}
