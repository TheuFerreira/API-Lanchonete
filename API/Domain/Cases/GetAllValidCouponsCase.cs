using API.Domain.Entities;
using API.Domain.Repositories;
using API.Domain.Services;
using API.Presenters.Cases;
using API.Presenters.Responses;

namespace API.Domain.Cases
{
    public class GetAllValidCouponsCase : IGetAllValidCouponsCase
    {
        private readonly ICouponRepository couponRepository;
        private readonly IFileService fileService;

        public GetAllValidCouponsCase(ICouponRepository couponRepository, IFileService fileService)
        {
            this.couponRepository = couponRepository;
            this.fileService = fileService;
        }

        public IEnumerable<GetAllValidCouponsResponse> Execute()
        {
            IEnumerable<Coupon> coupons = couponRepository.GetAllNotExpired();
            IEnumerable<GetAllValidCouponsResponse> response = coupons.Select(x =>
            {
                string path = string.Format("{0}//Photos//Coupons//{1}", Directory.GetCurrentDirectory(), x.Photo);
                string image = fileService.FileToBase64(path);

                return new GetAllValidCouponsResponse
                {
                    CouponId = x.CouponId,
                    Image = image,
                };
            });

            return response;
        }
    }
}
