using Demo.Dtos.Book;
using FluentValidation;

namespace Demo.DtosValidations
{
    public class AddBookDtoValidations : AbstractValidator<AddBookDto>
    {
        public AddBookDtoValidations()
        {
            RuleFor(x => x.Title).Length(10, 100).NotEmpty();
        }
    }
}
