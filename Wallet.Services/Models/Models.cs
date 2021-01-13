using FluentValidation;

namespace Wallet.Services.Registration.Models
{
    public class InModel
    {
        public string Name { get; set; }
    }

    public class OutModel
    {
        public long Id { get; set; }

        public OutModel(long id)
        {
            Id = id;
        }
    }

    public class Validator : AbstractValidator<InModel>
    {
        public Validator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}

namespace Wallet.Services.Authentication.Models
{
    public class InModel
    {
        public string Name { get; set; }
    }

    public class OutModel
    {
        public long Id { get; set; }
        public string Name { get; set; }

        public OutModel(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    public class Validator : AbstractValidator<InModel>
    {
        public Validator()
        {
            RuleFor(model => model.Name)
                .NotNull()
                .NotEmpty();
        }
    }
}
