using Microsoft.AspNetCore.Authorization;

namespace QuizStep.Application.AccessHandlers.Requirements;

public record QuizAccessRequirement: IAuthorizationRequirement;