using BussinessLogicLevel.Services;
using DbLevel;
using DbLevel.Interfaces;
using DbLevel.Models;
using Moq;

namespace BussinesLogicLevelTests.ServiceTests
{
    public class PaymentServiceTests
    {
        private readonly Mock<IRepository<ProductStorage>> _mockProductStorageRepo;
        private readonly Mock<IRepository<Cart>> _mockCartRepo;
        private readonly PaymentService _paymentService;
        public PaymentServiceTests()
        {
            _mockProductStorageRepo = new Mock<IRepository<ProductStorage>>();
            _mockCartRepo = new Mock<IRepository<Cart>>();
            _paymentService = new PaymentService(_mockProductStorageRepo.Object, _mockCartRepo.Object);
        }
    }
}
