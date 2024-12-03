using ECommerceNextjs.Models;
using ECommerceNextjs.Services;
using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ECommerceNextjs.Controllers
{
    [ApiController]
    public class AccountController: ControllerBase
    {

        public readonly AccountService _accountService = null!;
        public readonly AuthenticationService _authService = null!;

        public AccountController(AccountService accountService, AuthenticationService authService)
        {
            _accountService = accountService;
            _authService = authService;
        }

        [HttpPost]
        [Route("ecommerce/addAddress")]
        public async Task<IActionResult> AddAddress(SavedAddressModel address, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _accountService.addAddress(address);
                return CreatedAtAction("AddAddress", address);
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpGet]
        [Route("ecommerce/addAddress")]
        public async Task<ActionResult<List<SavedAddressModel>>> GetAddresses([FromQuery] string email, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                var addresses = await _accountService.GetAddresses(email);
                if (addresses == null)
                {
                    return NotFound();
                }
                return addresses;
            } else
            {
                return BadRequest("Bad Token");
            }


        }

        [HttpPut]
        [Route("ecommerce/updateAddress")]
        public async Task<IActionResult> UpdateAddress([FromQuery] string id, SavedAddressModel updatedAddress, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _accountService.UpdateAddress(id, updatedAddress);
                return NoContent();
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpPost]
        [Route("ecommerce/setAsDefault")]
        public async Task<IActionResult> SetAsDefault(SetAsDefault defaultValue, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {

                var addresses = await _accountService.GetAddresses(defaultValue.email);
                foreach (var address in addresses)
                {
                    if (address.setAsDefault == true)
                    {
                        await _accountService.SetAsDefault(address._id, "setAsDefault", false);
                    }
                }
                await _accountService.SetAsDefault(defaultValue.id, defaultValue.property, defaultValue.value);
                return CreatedAtAction("SetAsDefault", defaultValue.value);
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpDelete]
        [Route("ecommerce/deleteAddress")]
        public async Task<IActionResult> Delete([FromQuery] string id, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _accountService.DeleteAddress(id);
                return NoContent();
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpPost]
        [Route("ecommerce/addPaymentMethod")]
        public async Task<IActionResult> AddPaymentMethod(PaymentMethodModel paymentMethod, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _accountService.addPaymentMethod(paymentMethod);
                return CreatedAtAction("AddPaymentMethod", paymentMethod);
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpPost]
        [Route("ecommerce/saveAccount")]
        public async Task<IActionResult> SaveAccount(AccountModel account, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {
            var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
            var isValidToken = await _authService.checkAuthToken(authToken);
            if (isValidToken)
            {
                await _accountService.saveAccountDetails(account);
                return CreatedAtAction("SaveAccount", account);
            } else
            {
                return BadRequest("Bad Token");
            }
        }

        [HttpGet]
        [Route("ecommerce/getAccount")]
        public async Task<ActionResult<AccountModel>> GetAccount([FromQuery] string email, [FromHeader(Name = "Authorization")] string customHeaderValue)
        {

                var authToken = customHeaderValue.ToString().Replace("Bearer ", "").Trim();
                var isValidToken = await _authService.checkAuthToken(authToken);
                if (isValidToken)
            {
                var account = await _accountService.getAccountDetails(email);
                if (account == null)
                {
                    return Ok(new { });
                }
                return account;
            } else
            {
                return BadRequest("Bad Token");
            }
        }
    }
}
