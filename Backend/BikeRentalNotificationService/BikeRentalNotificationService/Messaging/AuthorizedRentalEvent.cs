﻿namespace Messaging;

public class AuthorizedRentalEvent
{
    public string UserId { get; init; } = string.Empty;
    public string RentalCode { get; init; } = string.Empty;

}