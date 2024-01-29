using Appdoon.Application.Interfaces;
using Appdoon.Application.Services.Rate.Command.CreateRateService;
using Appdoon.Application.Services.RoadMaps.Command.RegisterRoadmapService;
using Appdoon.Common.Dtos;
using Appdoon.Domain.Entities.RoadMaps;
using Dto.Payment;
using Dto.Response.Payment;
using Mapdoon.Common.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using Org.BouncyCastle.Ocsp;
using System;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarinPal.Class;
using ZarinPal.Interface;
using static System.Net.WebRequestMethods;
using Mapdoon.Domain.Entities.Paymnet;
using Appdoon.Domain.Entities.Users;

namespace Mapdoon.Application.Services.PaymentGatway
{
    public interface IPaymentGatewayService : ITransientService
    {
        ResultDto<string> ExecutePayment(int RoadmapId ,  int UserId);
        Task<ResultDto> CheckVerification(int paymentid, string authority, int userId);

    }
    public class PaymentGatewayService : IPaymentGatewayService
    {
        private readonly Payment _payment;
        private readonly Authority _authority;
        private readonly Transactions _transactions;
        private readonly IDatabaseContext _databaseContext;
        private readonly IRegisterRoadmapService _registerRoadmapService;
        public PaymentGatewayService(IDatabaseContext databaseContext , IRegisterRoadmapService registerRoadmapService)
        {
            _databaseContext = databaseContext;
            var expose = new Expose();
            _payment = expose.CreatePayment();
            _authority = expose.CreateAuthority();
            _transactions = expose.CreateTransactions();
            _registerRoadmapService = registerRoadmapService;
        }

        public  ResultDto<string> ExecutePayment(int RoadmapId , int UserId)
        {
            var roadmap = _databaseContext.RoadMaps.FirstOrDefault(r => r.Id == RoadmapId);
            var user = _databaseContext.Users.FirstOrDefault(u => u.Id == UserId);
            var payment = new PaymentRecords()
            {
                UserId = UserId,
                User = user,
                RoadMap = roadmap,
                RoadmapId = RoadmapId,
                IsFinaly = false,
                InsertTime = DateTime.UtcNow,
            };
            _databaseContext.Payments.Add(payment);
            _databaseContext.SaveChanges();
            int paymentId = _databaseContext.Payments.FirstOrDefault(p => p.RoadmapId == RoadmapId && 
                                                                          p.UserId == UserId).Id;
            //var req = _payment.Request(new DtoRequest()
            //{
            //    Amount = roadmap.Price ?? 0,
            //    CallbackUrl = "", // url front method that should be called after payment
            //    Description = "درگاه پرداخت",
            //    Email = "email@gmail.com",
            //    Mobile = "0000000",
            //    MerchantId = "YOUR-ZARINPAL-MERCHANT-CODE"
            //}, ZarinPal.Class.Payment.Mode.sandbox);
            //var url = $"https://sandbox.zarinpal.com/pg/StartPay/{req.Authority}";
            //return url;
            System.Net.ServicePointManager.Expect100Continue = false;
            ZarinpalTest.PaymentGatewayImplementationServicePortTypeClient zp =
                    new ZarinpalTest.PaymentGatewayImplementationServicePortTypeClient();
            string Authority = "";
            var status = zp.PaymentRequest("YOUR-ZARINPAL-MERCHANT-CODE", (int)(roadmap.Price), "درگاه پرداخت",
                "email@gmail.com", "000000000", $"http://localhost:3000/payment/{paymentId}/", out Authority);

            if (status == 100)
            {
                return new ResultDto<string>()
                {
                    IsSuccess = true,
                    Message = "لینک با موفقیت ارسال شد",
                    Data = $"https://sandbox.zarinpal.com/pg/StartPay/{Authority}"
                };

            }
            else
            {
                return new ResultDto<string>()
                {
                    IsSuccess = false,
                    Message = "لینک ارسال نشد",
                    Data = ""
                };
            }

        }

        public async Task<ResultDto> CheckVerification(int paymentid , string authority , int userId)
        {
            var paymentrecord = _databaseContext.Payments.FirstOrDefault(p => p.Id == paymentid);
            var roadmap = _databaseContext.RoadMaps.FirstOrDefault(r => r.Id == paymentrecord.RoadmapId);
            var verification =  await _payment.Verification(new DtoVerification()
            {
                Amount = roadmap.Price ?? 0,
                Authority = authority,
                MerchantId = "YOUR-ZARINPAL-MERCHANT-CODE"
            }, ZarinPal.Class.Payment.Mode.sandbox);

            if (verification.Status == 100)
            {
                _registerRoadmapService.Execute(roadmap.Id, userId);
                _databaseContext.Payments.FirstOrDefault(p => p.Id == paymentid).IsFinaly = true;
                _databaseContext.SaveChanges();
                return new ResultDto()
                {
                    IsSuccess = true,
                    Message = "پرداخت با موفقیت انجام شد"
                };
            }
            else
            {
                return new ResultDto()
                {
                    IsSuccess = false,
                    Message = "پرداخت انجام نشد"
                };
            }
        }


    }
}
