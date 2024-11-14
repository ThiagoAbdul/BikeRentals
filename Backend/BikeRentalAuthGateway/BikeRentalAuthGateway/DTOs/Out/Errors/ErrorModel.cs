namespace BikeRentalAuthGateway.DTOs.Out.Errors;

public class ErrorModel(string message)
{
    public string Message { get; set; } = message;

    public virtual IResult ToHttpResult()
    {
        return TypedResults.BadRequest(Message);
    }
}
