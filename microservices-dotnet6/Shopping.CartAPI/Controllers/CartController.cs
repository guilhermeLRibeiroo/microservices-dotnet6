using Microsoft.AspNetCore.Mvc;
using Shopping.CartAPI.Data.ValueObjects;
using Shopping.CartAPI.Messages;
using Shopping.CartAPI.RabbitMQSender;
using Shopping.CartAPI.Repositories;

namespace Shopping.CartAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class CartController
        : ControllerBase
    {
        private ICartRepository _cartRepository;
        private IRabbitMQMessageSender _rabbitMQMessageSender;

        public CartController(ICartRepository cartRepository, IRabbitMQMessageSender rabbitMQMessageSender)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _rabbitMQMessageSender = rabbitMQMessageSender ?? throw new ArgumentNullException(nameof(rabbitMQMessageSender));
        }

        [HttpGet("find-cart/{id}")]
        public async Task<ActionResult<CartVO>> FindCart(string id)
        {
            var cart = await _cartRepository.FindCartByUserId(id);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPost("add-cart")]
        public async Task<ActionResult<CartVO>> AddCart(CartVO cartVO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartVO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpPut("update-cart")]
        public async Task<ActionResult<CartVO>> UpdateCart(CartVO cartVO)
        {
            var cart = await _cartRepository.SaveOrUpdateCart(cartVO);
            if (cart == null) return NotFound();
            return Ok(cart);
        }

        [HttpDelete("remove-cart/{cartDetailId}")]
        public async Task<ActionResult<CartVO>> RemoveCart(int cartDetailId)
        {
            var status = await _cartRepository.RemoveFromCart(cartDetailId);
            if (!status) return BadRequest();
            return Ok(status);
        }

        [HttpPost("apply-coupon")]
        public async Task<ActionResult<CartVO>> ApplyCoupon(CartVO cartVO)
        {
            var status = await _cartRepository.ApplyCoupon(cartVO.CartHeader.UserId, cartVO.CartHeader.CouponCode);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpDelete("remove-coupon/{userId}")]
        public async Task<ActionResult<CartVO>> RemoveCoupon(string userId)
        {
            var status = await _cartRepository.RemoveCoupon(userId);
            if (!status) return NotFound();
            return Ok(status);
        }

        [HttpPost("checkout")]
        public async Task<ActionResult<CheckoutHeaderVO>> Checkout(CheckoutHeaderVO checkoutHeader)
        {
            if(checkoutHeader?.UserId == null) return BadRequest();

            var cart = await _cartRepository.FindCartByUserId(checkoutHeader.UserId);
            if (cart == null) return NotFound();

            checkoutHeader.CartDetails = cart.CartDetails;
            checkoutHeader.DateTime = DateTime.Now;

            _rabbitMQMessageSender.Send(checkoutHeader, "checkoutqueue");

            return Ok(checkoutHeader);
        }
    }
}