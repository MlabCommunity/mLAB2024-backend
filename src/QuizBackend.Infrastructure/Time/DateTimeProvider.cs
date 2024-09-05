﻿using QuizBackend.Application.Interfaces;

namespace QuizBackend.Infrastructure.Time;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}