﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuizBackend.Application.Dtos.Auth;
using QuizBackend.Application.Interfaces.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace QuizBackend.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("signin")]
    [SwaggerOperation(Summary = "User sign-in", Description = "Logs in the user with the provided credentials.")]
    [ProducesResponseType(typeof(JwtAuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> SignIn(LoginDto loginDto)
    {
        var jwtAuthResult = await _authService.LoginAsync(loginDto);
        return Ok(jwtAuthResult);
    }

    [HttpPost("signup")]
    [SwaggerOperation(Summary = "User sign-up", Description = "Registers a new user with the provided details.")]
    [ProducesResponseType(typeof(SignUpResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> SignUp(RegisterRequestDto request)
    {
        var response = await _authService.SignUpAsync(request);
        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    [SwaggerOperation(Summary = "User logout", Description = "Logs out the current user.")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Logout()
    {
        var result = await _authService.LogoutAsync();
        return Ok(result);
    }

    [HttpPost("refresh-token")]
    [SwaggerOperation(
        Summary = "Refresh JWT token",
        Description = "Refreshes the JWT token using a valid refresh token.")]
    [ProducesResponseType(typeof(JwtAuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
    {
        try
        {
            var jwtAuthResult = await _authService.RefreshTokenAsync(refreshTokenRequest.RefreshToken);
            return Ok(jwtAuthResult);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("guests")]
    [SwaggerOperation(
        Summary = "Create guest user and generate tokens",
        Description = "Creates a guest user with a given display name and generates JWT tokens for the guest.")]
    [ProducesResponseType(typeof(JwtAuthResultDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> CreateGuestUser(CreateGuestRequest request)
    {
        var guestUser = await _authService.CreateAndAuthenticateGuest(request.DisplayName);
        return Ok(guestUser);
    }
}