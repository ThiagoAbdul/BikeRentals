﻿namespace BikeRentalAuthGateway.DTOs.In;

public record SignUpRequest(string FullName, string Email, string Password);