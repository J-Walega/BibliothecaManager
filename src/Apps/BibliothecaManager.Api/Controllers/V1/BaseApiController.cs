﻿using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace BibliothecaManager.Api.Controllers.V1;

/// <summary>
/// Base api controller
/// </summary>
[ApiController]
[Route("v1/[controller]")]
public abstract class BaseApiController : ControllerBase
{
    private ISender _mediator;

    /// <summary>
    /// Mediator sender
    /// </summary>
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
}