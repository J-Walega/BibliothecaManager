using System;
using BibliothecaManager.Application.Common.Interfaces;

namespace BibliothecaManager.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
