namespace Message;

public class UnauthorizedRentalEvent
{
    public string UserId { get; init; } = string.Empty;

    public UnauthorizedRentalEvent()
    {
        
    }

    public UnauthorizedRentalEvent(string userId)
    {
        UserId = userId;
    }
}
