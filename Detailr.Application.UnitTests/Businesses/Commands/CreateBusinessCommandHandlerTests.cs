using Detailr.Application.Exceptions;

namespace Detailr.Application.UnitTests.Businesses.Commands
{
    public class CreateBusinessCommandHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBusinessRepository> _businessRepository;
        private readonly CreateBusinessDto _createBusinessDto;
        private readonly CreateBusinessCommandHandler _handler;

        public CreateBusinessCommandHandlerTests()
        {
            _businessRepository = MockBusinessRepository.GetBusinessRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
            _handler = new CreateBusinessCommandHandler(_businessRepository.Object, _mapper);


            _createBusinessDto = new CreateBusinessDto
            {
                Name = "Test Business 1",
                Description = "Test Business Description 1"
            };
        }

        [Fact]
        public async Task Valid_Business_Added()
        { 
            var handler = new CreateBusinessCommandHandler(_businessRepository.Object, _mapper);
            var result = await handler.Handle(new CreateBusinessCommand() { CreateBusinessDto = _createBusinessDto }, CancellationToken.None);
            var businesses = await _businessRepository.Object.GetAll();

            result.ShouldBeOfType<BaseCommandResponse>();

            businesses.Count.ShouldBe(3);
        }

        [Fact]
        public async Task InValid_Business_Added()
        {
            _createBusinessDto.Name = null;

            var result = await _handler.Handle(new CreateBusinessCommand() { CreateBusinessDto = _createBusinessDto }, CancellationToken.None);
            var businesses = await _businessRepository.Object.GetAll();

            businesses.Count.ShouldBe(3);

            result.ShouldBeOfType<BaseCommandResponse>();
        }
    }
}
