using FluentValidation;

namespace QuizBackend.Application.Queries.Quizzes.GetQuizzes
{
    public class GetPagedQuizzesValidator : AbstractValidator<GetPagedQuizzesQuery>
    {
        private const int MaxPageSize = 10;
        public GetPagedQuizzesValidator() 
        {
            RuleFor(r => r.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(MaxPageSize)
                .WithMessage($"Page size must be less than or equal to [{string.Join(",", MaxPageSize)}]"); ;
        }
    }
}