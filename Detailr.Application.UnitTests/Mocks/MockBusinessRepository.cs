namespace Detailr.Application.UnitTests.Mocks
{
    public static class MockBusinessRepository
    {
        public static Mock<IBusinessRepository> GetBusinessRepository()
        {
            var businesses = new List<Business>
            {
                new Business { Id = 1, Name = "Test Business 1", Description = "Test Business 1 Description"},
                new Business { Id = 2, Name = "Test Business 2", Description = "Test Business 2 Description"}
            };

            var mockRepository = new Mock<IBusinessRepository>();

            mockRepository.Setup(r => r.GetAll()).ReturnsAsync(businesses);

            mockRepository.Setup(r => r.Add(It.IsAny<Business>())).ReturnsAsync((Business business) =>
            {
                businesses.Add(business);
                return business;
            });

            return mockRepository;
        }
    }
}


