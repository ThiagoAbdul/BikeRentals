namespace BikeRentalAuthGateway.DTOs.Out.Errors;

public class EmailAlreadyRegisteredErrorModel() : ErrorModel("Email já cadastrado")
{
    public override IResult ToHttpResult()
    {
        return TypedResults.BadRequest(Message);
    }
}
