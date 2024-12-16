using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Common.Security;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Users.CreateUser;

/// <summary>
/// Handler for processing CreateUserCommand requests
/// </summary>
public class CreateUserHandler : IRequestHandler<CreateUserCommand, CreateUserResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IEventLogRepository _eventLogRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ILogger<CreateUserHandler> logger;

    /// <summary>
    /// Initializes a new instance of CreateUserHandler
    /// </summary>
    /// <param name="userRepository">The user repository</param>
    /// <param name="eventLogRepository">The event log repository</param>
    /// <param name="mapper">The AutoMapper instance</param>
    /// <param name="validator">The validator for CreateUserCommand</param>
    public CreateUserHandler(
        IUserRepository userRepository,
        IEventLogRepository eventLogRepository,
        IMapper mapper,
        IPasswordHasher passwordHasher,
        ILogger<CreateUserHandler> logger)
    {
        _userRepository = userRepository;
        _eventLogRepository = eventLogRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
        this.logger = logger;
    }

    /// <summary>
    /// Handles the CreateUserCommand request
    /// </summary>
    /// <param name="command">The CreateUser command</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created user details</returns>
    public async Task<CreateUserResult> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try 
        {
            var validator = new CreateUserCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);
            if (existingUser != null)
                throw new InvalidOperationException($"User with email {command.Email} already exists");

            var user = _mapper.Map<User>(command);
            user.Password = _passwordHasher.HashPassword(command.Password);

            var createdUser = await _userRepository.CreateAsync(user, cancellationToken);
            
            var userRegisteredEvent = new UserRegisteredEvent(user);
            
            //log the event to console
            logger.LogInformation("User Registered: {@Event}", userRegisteredEvent);
            // Log the event to MongoDB
            await _eventLogRepository.LogEventAsync(userRegisteredEvent, cancellationToken);
            
            
            var result = _mapper.Map<CreateUserResult>(createdUser);
            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating user with email {Email}", command.Email);
            throw;
        }
    }
}
