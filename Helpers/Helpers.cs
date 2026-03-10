using Microsoft.IdentityModel.Tokens;
using Stripe;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;
using TheStartupBuddyV3.Repository;

namespace TheStartupBuddyV3.Helpers
{
    public class Helpers : IHelpers
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _accessor;

        public Helpers(
        IRepositoryWrapper repository,
        IConfiguration configuration,
        IHttpContextAccessor accessor
        )
        {
            _configuration = configuration;
            _repository = repository;
            _accessor = accessor;
        }

        public string GetPlanIdStartup(short membership_type, short membership_duration)
        {
            //=== Bootstrap Membership
            if (membership_type == 2)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQUnGtWfUWHiUxH2EkUERF";
                    //}
                    //else
                    //{

                    //}

                    return "price_1HXcGxGtWfUWHiUxfuivpfDV";

                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQUnGtWfUWHiUx6TuQy1Y2";
                    //}
                    //else
                    //{

                    //}

                    return "price_1HXcGyGtWfUWHiUxJNyYFClC";

                }
            }
            ////=== Seed Membership
            //else if (membership_type == 3)
            //{
            //    //=== Monthly
            //    if (!membership_duration)
            //    {
            //        if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
            //        {
            //            return "price_1HjQVSGtWfUWHiUxoPGFjIuc";
            //        }
            //        else
            //        {
            //            return "price_1HXcKUGtWfUWHiUxvN1Mwvlw";
            //        }

            //    }
            //    // Yearly
            //    else
            //    {
            //        if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
            //        {
            //            return "price_1HjQVSGtWfUWHiUxLxkuMClX";
            //        }
            //        else
            //        {
            //            return "price_1HXcKVGtWfUWHiUxewUUHQhX";
            //        }

            //    }
            //}
            //=== Seed Stage Membership
            else if (membership_type == 4)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQVhGtWfUWHiUxDhUEHVPM";
                    //}
                    //else
                    //{

                    //}

                    return "price_1HXd73GtWfUWHiUxhCKoiX48";

                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQVhGtWfUWHiUxncw5HVmo";
                    //}
                    //else
                    //{

                    //}

                    return "price_1HXd74GtWfUWHiUxPQoQUxch";

                }
            }
            //=== Growth Membership
            else if (membership_type == 5)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1Is4EfGtWfUWHiUx57b7C52i";
                    //}
                    //else
                    //{

                    //}

                    return "price_1Is55uGtWfUWHiUxxQjrvBKX";

                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1Is4EfGtWfUWHiUxv4CURkYU";
                    //}
                    //else
                    //{

                    //}

                    return "price_1Is55uGtWfUWHiUxHzdJeAMP";

                }
            }

            return "The API unavailable";

        }

        public string GetPlanIdInvestor(short membership_type, short membership_duration)
        {
            //=== Venture Capital Membership
            if (membership_type == 2)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQWTGtWfUWHiUxPsLlnJvL";
                    //}
                    //else
                    //{

                    //}
                    return "price_1HZt3SGtWfUWHiUxifE7TJhR";
                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1HjQWTGtWfUWHiUxGHqPkoxj";
                    //}
                    //else
                    //{

                    //}
                    return "price_1HZt3SGtWfUWHiUxkF7Qkc9s";

                }

            }
            //=== Accelerator Membership
            else if (membership_type == 4)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1L1P8QGtWfUWHiUxD0hSIMYT";
                    //}
                    //else
                    //{

                    //}

                    return "price_1L7UrLGtWfUWHiUxNGEwhE5E";

                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1L1P8QGtWfUWHiUxvg7YihBZ";
                    //}
                    //else
                    //{

                    //}

                    return "price_1L7UrLGtWfUWHiUxY2jFWc3z";

                }
            }
            //=== Enterprise Membership
            else if (membership_type == 5)
            {
                //=== Monthly
                if (membership_duration == 0)
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1L1PAzGtWfUWHiUxO652p7az";
                    //}
                    //else
                    //{

                    //}

                    return "price_1L7UuTGtWfUWHiUxKpdrNQU5";

                }
                // Yearly
                else
                {
                    //if (System.Configuration.ConfigurationManager.AppSettings["Stage"] == "PRODUCTION")
                    //{
                    //    return "price_1L1PAzGtWfUWHiUxBQWrvix1";
                    //}
                    //else
                    //{

                    //}

                    return "price_1L7UuTGtWfUWHiUxSWGsJ6Xv";

                }
            }

            return "The API unavailable";

        }

        public void StripeCreateSubscription(string role, string userid, string? token, short membership_type, string? coupon, short membership_duration, string email, int count_subscription, bool skip_trial = false)
        {

            bool isStartup = role.Contains("0") ? true : false;
            bool isInvestor = role.Contains("1") ? true : false;

            var CustomerService = new CustomerService();
            var SubscriptionService = new SubscriptionService();
            var ChargeService = new ChargeService();
            var subscription = new Subscription();
            var Charge = new Charge();
            var PriceService = new PriceService();
            var ProductService = new ProductService();

            // ===== 1. Stripe API create customer
            var CustomerData = new CustomerCreateOptions
            {
                Email = email,
                Source = token
            };

            Customer customer = CustomerService.Create(CustomerData);

            // ===== 2. Retrieve the customer's plan based on the chosen plan
            string planId = "";


            if (isStartup)
            {
                planId = GetPlanIdStartup(membership_type, membership_duration);
            }
            else if (isInvestor)
            {

                planId = GetPlanIdInvestor(membership_type, membership_duration);

            }

            var items = new List<SubscriptionItemOptions> {
                 new SubscriptionItemOptions {
                     Plan = planId
                 }
            };

            var CreateSubscriptionData = new SubscriptionCreateOptions
            {

                Customer = customer.Id,
                Items = items,

            };

            // ===== 3. subscription trial
            if (count_subscription == 0 && !skip_trial)
            {

                CreateSubscriptionData.TrialPeriodDays = 14;

            }

            // ===== 4. If any applied coupon

            if (coupon != "")
            {

                CreateSubscriptionData.Coupon = coupon;

            }

            subscription = SubscriptionService.Create(CreateSubscriptionData);

            if (membership_type > 1 && isStartup)
            {

                var newSubscriber = new Models.SubscriptionStartup
                {
                    Subscriptionid = subscription.Id,
                    Userid = userid,
                    Status = 0,
                    Starttime = DateTime.Now,
                    Plantype = (short)membership_type,
                    Enddate = null,
                    Duration = (short)membership_duration,
                    Defaultplan = 1
                };

                _repository.SubscriptionStartup.CreateSubscription(newSubscriber);
                _repository.SaveAsync();

            }
            else if (membership_type > 1 && isInvestor)
            {
                var newSubscriber = new Models.SubscriptionInvestor
                {
                    Subscriptionid = subscription.Id,
                    Userid = userid,
                    Status = 0,
                    Starttime = DateTime.Now,
                    Plantype = (short)membership_type,
                    Enddate = null,
                    Duration = (short)membership_duration,
                    Defaultplan = 1
                };

                _repository.SubscriptionInvestor.CreateSubscription(newSubscriber);
                _repository.SaveAsync();
            }

            var InvoiceOptions = new InvoiceListOptions
            {
                Customer = subscription.CustomerId,
                Status = "paid"
            };

            var InvoiceService = new InvoiceService();

            StripeList<Invoice> invoices = InvoiceService.List(
                InvoiceOptions
            );

        }

        public string GenerateToken(string email, string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(15),
                claims: claims,
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GetBaseUrl()
        {
            var request = _accessor.HttpContext.Request;

            var baseUrl = $"{request.Scheme}://{request.Host}";

            return baseUrl;
        }

        public string RegexFilter(string FileName)
        {
            var fileNameFiltered = Regex.Replace(FileName.ToLower(), "[^a-zA-Z0-9]", String.Empty);
            return fileNameFiltered;
        }
    }
}
