using Admission.Core.Domain.Entities;
using Admission.Core.DTO;
using Admission.Core.ServiceContracts;
using Admission.Core.Services;
using Entities;
using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace Admission.UI.Controllers
{
	public class CheckoutController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly IVnPayService _vnPayService;
		public CheckoutController(ApplicationDbContext db, IVnPayService vpnPayService)
		{
			_db = db;
			_vnPayService = vpnPayService;
		}
		[HttpGet]
		public IActionResult Payment(Guid id)
		{
			var payment = _db.Payments.FirstOrDefault(p => p.Id == id);
			if (payment == null)
			{
				payment = new Payment
				{
					PaymentID = Guid.NewGuid(),
					Id = id,
					InitialExpenses = 300000, // Số tiền mặc định
					CostPaid = 0
				};

				_db.Payments.Add(payment);
				_db.SaveChanges();
			}

			return View(payment);
		}
		[HttpPost]
		public IActionResult Payment(Payment payment)
		{
			var vnPayModel = new VnPaymentRequestDTO
			{
				Amount = 300000, // Lấy số tiền cần thanh toán từ InitialExpenses
				CreatedDate = DateTime.Now,
				Description = "Thanh toán phí đăng ký tuyển sinh",
				/*FullName = payment.Id, // Tên người dùng đăng nhập*/
				OrderId = new Random().Next(1000, 10000)
			};
			return Redirect(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
		}
		public IActionResult PaymentSuccess()
		{
			return View();
		}
		[HttpGet]
		public IActionResult PaymentCallBack(string vnp_ResponseCode, string vnp_TxnRef)
		{
			var response = _vnPayService.PaymentExecute(Request.Query);
			if (response == null || response.VnPayResponseCode != "00")
			{
				TempData["Message"] = $"Lỗi thanh toán VNPAY: {response.VnPayResponseCode}";
				return RedirectToAction("PaymentFail");
			}
			var payment = _db.Payments.FirstOrDefault(p => p.PaymentID.ToString() == vnp_TxnRef);
			if (payment != null)
			{
				payment.CostPaid = payment.InitialExpenses;
				_db.Payments.Update(payment);
				_db.SaveChanges();
			}
			TempData["Message"] = $"Thanh toán VNPAY thành công!!!";
			return RedirectToAction("PaymentSuccess");
		}
	}
}
