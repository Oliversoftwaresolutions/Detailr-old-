using Detailr.Application.UnitTests.Mocks;

namespace Detailr.Application.UnitTests.Businesses.Queries
{
    public class GetBusinessListRequestHandlerTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IBusinessRepository> _mockRepository;

        public GetBusinessListRequestHandlerTests()
        {
            _mockRepository = MockBusinessRepository.GetBusinessRepository();

            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetBusinessListTest()
        {
            var handler = new GetBusinessListRequestHandler(_mockRepository.Object, _mapper);
            var result = await handler.Handle(new GetBusinessListRequest(), CancellationToken.None);

            result.ShouldBeOfType<List<BusinessDto>>();
            result.Count.ShouldBe(2);
        }
    }
}
