﻿namespace QuizBackend.Infrastructure.Authorization;

public static class PolicyNames
{
    public const string User = "User";
    public const string Guest = "Guest";
    public const string QuizOwner = "QuizOwner";
    public const string QuestionOwner = "QuestionOwner";
}