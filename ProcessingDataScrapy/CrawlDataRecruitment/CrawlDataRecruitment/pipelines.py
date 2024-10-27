import json
import os

class ExportToJSON:
    
    def __init__(self, output_file='output.json'):
        self.output_file = output_file
        # Nếu file JSON đã tồn tại, mở nó để đọc dữ liệu cũ
        if os.path.exists(self.output_file):
            with open(self.output_file, 'r', encoding='utf-8') as f:
                self.data = json.load(f)  # Đọc dữ liệu cũ
        else:
            self.data = []  # Khởi tạo danh sách rỗng nếu file không tồn tại

    def process_item(self, item, spider):
        # Thêm item mới vào danh sách dữ liệu
        self.data.append({
            'Web': item['Web'],
            'Major': item['Major'],
            'Link': item['Link'],
            'WorkName': item['WorkName'],
            'Company': item['Company'],
            'Province': item['Province'],
            'Salary': item['Salary'],
            'Type': item['Type'],
            'YOE': item['YOE'],  # Years of Experience
            'Level': item['Level'],
            'Requirement': item['Requirement'],
            'Description': item['Description'],
            'Welfare': item['Welfare'],
            'DeadlineCV': item['DeadlineCV'],
            'Amount': item['Amount'],
            'Img': item['Img']
        })

    def close_spider(self, spider):
        # Ghi dữ liệu vào file JSON khi đóng spider
        with open(self.output_file, 'w', encoding='utf-8') as f:
            json.dump(self.data, f, ensure_ascii=False, indent=4)  # Ghi dữ liệu ra file JSON
