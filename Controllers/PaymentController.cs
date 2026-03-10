using Microsoft.AspNetCore.Mvc;
using Stripe;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PaymentController : Controller
    {
        private readonly IRepositoryWrapper _repository;

        public PaymentController(
        IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetPromoCode(string? coupon_code)
        {

            var service = new CouponService();

            try
            {

                var getCoupon = service.Get(coupon_code);

                if (getCoupon != null && getCoupon.Valid)
                {
                    return Ok(getCoupon);
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return BadRequest("Wrong coupon code");

        }
    }
}
