import scrapy
import math
from CrawlDataRecruitment.items import Job_Item
from datetime import date

class TopcvSpider(scrapy.Spider):
    name = "topcv"

    def start_requests(self):
        # Bắt đầu quá trình crawl từ trang TopCV
        start_url = "https://www.topcv.vn/tim-viec-lam-moi-nhat"
        yield scrapy.Request(start_url, callback=self.parse)

    def parse(self, response):
        # Lấy số lượng công việc và tính toán số trang
        job_count = response.css('h1[class="search-job-heading"]::text').get()
        words = job_count.split()
        for word in words:
            if word.replace('.', '').isdigit():
                job_count = int(word.replace('.', ''))
                break
        max_page = math.ceil(job_count / 50)
        max_page = min(max_page, 200)  # Giới hạn số trang tối đa là 200

        # Duyệt qua từng trang và gửi request
        for page_number in range(1, max_page + 1):
            page_url = f"https://www.topcv.vn/tim-viec-lam-moi-nhat?page={page_number}"
            yield scrapy.Request(page_url, callback=self.job_parse)

    def job_parse(self, response):
        # Lấy danh sách các công việc từ trang hiện tại
        job_list_url = response.css('div.job-item-search-result.job-ta h3.title a::attr(href)').extract()
        for job_url in job_list_url:
            # Loại bỏ các URL không hợp lệ hoặc URL của các brand
            if "https://www.topcv.vn/brand/" not in job_url:
                yield scrapy.Request(job_url, callback=self.it_parse)

    def it_parse(self, response):
        # Lấy thông tin chi tiết về công việc
        Web = 'TopCV'
        Img = response.css('.company-logo img::attr(src)').get()
        Nganh = ", ".join(response.css('.box-category-tags a::text').getall()) or "Không có"
        Link = response.url
        TenCV = "".join(response.css('h1.job-detail__info--title ::text').extract()).strip()
        CongTy = "".join(response.css('.company-name-label ::text').extract()).strip()

        # Lấy các thông tin như lương, địa điểm, kinh nghiệm
        Luong = TinhThanh = KinhNghiem = "Không có"
        for section in response.css('.job-detail__info--section-content'):
            title_text = section.css('.job-detail__info--section-content-title::text').get()
            value_text = section.css('.job-detail__info--section-content-value::text').get(default="Không có").strip()

            if title_text == 'Mức lương':
                Luong = value_text
            elif title_text == 'Địa điểm':
                TinhThanh = value_text
            elif title_text == 'Kinh nghiệm':
                KinhNghiem = value_text

        # Lấy thông tin cấp bậc, số lượng tuyển, loại hình làm việc
        CapBac = SoLuong = LoaiHinh = "Không có"
        for info_item in response.css('.box-general-group-info'):
            title_text = info_item.css('.box-general-group-info-title::text').get()
            value_text = info_item.css('.box-general-group-info-value::text').get(default="Không có").strip()

            if title_text == 'Cấp bậc':
                CapBac = value_text
            elif title_text == 'Số lượng tuyển':
                SoLuong = value_text
            elif title_text == 'Hình thức làm việc':
                LoaiHinh = value_text

        # Hạn nộp hồ sơ
        deadline_text = response.css('.job-detail__info--deadline ::text').getall()
        HanNopCV = deadline_text[-1].strip() if deadline_text else date.today().strftime('%Y-%m-%d')

        # Lấy các phần mô tả, yêu cầu, phúc lợi
        MoTa = YeuCau = PhucLoi = "Không có"
        for item in response.css('.job-description__item'):
            title = item.css('h3::text').get().lower()  # Chuyển thành chữ thường để so sánh
            content = " ".join(text.strip() for text in item.css('::text').extract())

            if "mô tả" in title:
                MoTa = content
            elif "yêu cầu" in title:
                YeuCau = content
            elif "quyền lợi" in title:
                PhucLoi = content

        # Tạo đối tượng IT_Item và yield
        item = Job_Item(
            Web=Web,
            Major=Nganh,
            Link=Link,
            WorkName=TenCV,
            Company=CongTy,
            Province=TinhThanh,
            Salary=Luong,
            Type=LoaiHinh,
            YOE=KinhNghiem,
            Level=CapBac,
            Requirement=YeuCau,
            Description=MoTa,
            Welfare=PhucLoi,
            DeadlineCV=HanNopCV,
            Amount=SoLuong,
            Img=Img
        )
        yield item
