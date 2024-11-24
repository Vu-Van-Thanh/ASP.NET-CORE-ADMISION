let selectedFilesCCCD = []; // Mảng cho khung chứa 1
let selectedFilesBHYT = []; // Mảng cho khung chứa 2
let selectedFilesHB = []; // Mảng cho khung chứa 3
let selectedFilesHK = []; // Mảng cho khung chứa 4

function previewImages(event, previewContainerId, selectedFiles) {
    const files = event.target.files; // Lấy các file đã chọn
    const imagePreview = document.getElementById(previewContainerId); // Sử dụng ID để chọn

    Array.from(files).forEach(file => {
        if (!selectedFiles.some(f => f.name === file.name)) {
            const reader = new FileReader();
            reader.onload = function(e) {
                const imgItem = document.createElement('div');
                imgItem.classList.add('image-item');
                
                imgItem.innerHTML = `
                    <img src="${e.target.result}" alt="Selected Image" class="image" width="100" height="100">
                    <div class="icon-container">
                        <span class="icon zoom" onclick="openModal(event,'${e.target.result}')">🔍</span>
                        <span class="icon delete" onclick="removeImage('${file.name}', this, '${previewContainerId}')"><i class="fas fa-trash"></i></span>
                    </div>
                `;
                imagePreview.appendChild(imgItem);
                selectedFiles.push(file); // Thêm file vào mảng
            };
            reader.readAsDataURL(file);
        }
    });
    
}

function openModal(event,imgSrc) {
    event.stopPropagation(); // Ngăn chặn sự kiện click lan ra bên ngoài
    const modal = document.getElementById("imageModal"); // Sử dụng ID cho modal
    const modalImg = document.getElementById("modalImage"); // Sử dụng ID cho hình ảnh trong modal
    modal.style.display = "flex"; // Sử dụng flex để căn giữa
    modalImg.src = imgSrc; // Gán src cho ảnh trong modal

    // Đóng modal khi click vào modal
    modal.onclick = function() {
        modal.style.display = "none";
    };
}

function removeImage(fileName, element, previewContainerId) {
    const imageItem = element.closest('.image-item');
    if (imageItem) {
        imageItem.remove(); // Xóa ảnh khỏi khung chứa
        
        // Cập nhật mảng, loại bỏ ảnh đã xóa
        if (previewContainerId === 'imagePreviewCCCD') {
            selectedFilesCCCD = selectedFilesCCCD.filter(f => f.name !== fileName);
        } else if (previewContainerId === 'imagePreviewBHYT') {
            selectedFilesBHYT = selectedFilesBHYT.filter(f => f.name !== fileName);
        } else if (previewContainerId === 'imagePreviewHB') {
            selectedFilesBHYT = selectedFilesBHYT.filter(f => f.name !== fileName);
        } else if (previewContainerId === 'imagePreviewHK') {
            selectedFilesHK = selectedFilesHK.filter(f => f.name !== fileName);
        }
    }
}


function chooseImage(selector) {
    let input = document.createElement("input");
    input.type = "file";
    input.accept = "image/*";
    input.onchange = function (event) {
        let file = event.target.files[0];
        if (file) {
            let reader = new FileReader();
            reader.onload = function (e) {
                const imgElement = document.querySelector(selector);
                imgElement.src = e.target.result; // Thay thế src của img

                if (imgElement.classList.contains('iv')) {
                    imgElement.style.display = 'block'; // Hiện ảnh
                    imgElement.nextElementSibling.style.display = 'none'; // Ẩn dấu +
                    imgElement.parentElement.querySelector('.remove-button').style.display = 'block'; // Hiện nút xóa
                    imgElement.parentElement.querySelector('.zoom-icon').style.display = 'block'; // Hiện nút kính lúp
                }
            };
            reader.readAsDataURL(file);
        }
    };
    input.click(); // Kích hoạt chọn file
}

function removeImageAvatar(event,selector) {
    event.stopPropagation();
    const imgElement = document.querySelector(selector);
    const plusSign = imgElement.nextElementSibling; // Dấu +
    const removeButton = imgElement.parentElement.querySelector('.remove-button'); // Nút xóa
    const zoomButton = imgElement.parentElement.querySelector('.zoom-icon'); // Nút xóa
    imgElement.src = ""; // Xóa ảnh
    if (imgElement.classList.contains('iv')) {
        
        imgElement.style.display = 'none'; // Ẩn ảnh
        plusSign.style.display = 'block'; // Hiện dấu +
        removeButton.style.display = 'none'; // Ẩn nút xóa
        zoomButton.style.display = 'none'; // Ẩn nút xóa
    }
}


// Hàm xử lý sự kiện cho nút chỉnh sửa và lưu
function setupEditAndSaveEvents(icon) {
    icon.addEventListener('click', (event) => {
        const row = event.target.closest('tr');
        const cells = row.querySelectorAll('.editable');

        if (event.target.classList.contains('fa-edit')) {
            // Đổi từ chế độ xem sang chế độ chỉnh sửa
            cells.forEach((cell) => {
                const input = document.createElement('input');
                input.type = 'text';
                input.value = cell.textContent;
                cell.textContent = ''; // Xóa nội dung hiện tại
                cell.appendChild(input); // Thêm input vào ô
            });

            // Thay đổi biểu tượng chỉnh sửa thành lưu
            event.target.classList.remove('fa-edit');
            event.target.classList.add('fa-save');
            event.target.setAttribute('title', 'Lưu');
        } else if (event.target.classList.contains('fa-save')) {
            // Đổi từ chế độ chỉnh sửa sang chế độ xem
            cells.forEach((cell) => {
                const input = cell.querySelector('input');
                if (input) {
                    cell.textContent = input.value; // Cập nhật nội dung ô
                }
            });

            // Đổi lại biểu tượng về chỉnh sửa
            event.target.classList.remove('fa-save');
            event.target.classList.add('fa-edit');
            event.target.setAttribute('title', 'Chỉnh sửa');
        }
    });
}

/*// Xử lý bảng
document.getElementById('add-relation').addEventListener('click', () => {
    console.log("Đa gtheme thành viên");
    const relation = document.getElementById('Relation').value;
    const name = document.getElementById('FullNameRelation').value;
    const birthYear = document.getElementById('DateOfBirthRelation').value;
    const occupation = document.getElementById('Occupation').value;
    const workplace = document.getElementById('Workplace').value;

    if (!relation || !name || !birthYear || !occupation || !workplace) {
        alert('Vui lòng điền đầy đủ thông tin trước khi thêm.');
        return;
    }

    const table = document.getElementById('infoTable').querySelector('tbody');
    const newRow = document.createElement('tr');

    newRow.innerHTML = `
        <td class="editable">${relation}</td>
        <td class="editable">${name}</td>
        <td class="editable">${birthYear}</td>
        <td class="editable">${occupation}</td>
        <td class="editable">${workplace}</td>
        <td class="action-icons">
            <i class="fas fa-edit" title="Chỉnh sửa"></i>
            <i class="fas fa-trash" title="Xóa"></i>
        </td>
    `;

    // Thêm sự kiện cho nút xóa
    newRow.querySelector('.fa-trash').addEventListener('click', (event) => {
        if (confirm('Bạn có chắc chắn muốn xóa không?')) {
            event.target.closest('tr').remove();
        }
    });

    // Thêm sự kiện chỉnh sửa cho nút chỉnh sửa
    const editIcon = newRow.querySelector('.fa-edit');
    setupEditAndSaveEvents(editIcon); // Gọi hàm thiết lập sự kiện cho biểu tượng chỉnh sửa

    table.appendChild(newRow);

    // Xóa nội dung của các trường đầu vào sau khi thêm vào bảng
    document.getElementById('Relation').value = '';
    document.getElementById('FullNameRelation').value = '';
    document.getElementById('DateOfBirthRelation').value = '';
    document.getElementById('Occupation').value = '';
    document.getElementById('Workplace').value = '';
});

// Thiết lập sự kiện cho các biểu tượng chỉnh sửa hiện có (nếu có)
document.querySelectorAll('.fa-edit').forEach(setupEditAndSaveEvents);


// Xử lý sự kiện cho nút xóa chỉ trong bảng infoTable
document.querySelectorAll('#infoTable .fa-trash').forEach(icon => {
    icon.addEventListener('click', (event) => {
        if (confirm('Bạn có chắc chắn muốn xóa không?')) {
            const row = event.target.closest('tr');
            row.remove(); // Xóa hàng
        }
    });
});
*/
// Hàm để chuyển đổi Data URI thành Blob
function dataURItoBlob(dataURI) {
    var byteString = atob(dataURI.split(',')[1]);
    var mimeString = dataURI.split(',')[0].split(':')[1].split(';')[0];
    var arrayBuffer = new ArrayBuffer(byteString.length);
    var intArray = new Uint8Array(arrayBuffer);

    for (var i = 0; i < byteString.length; i++) {
        intArray[i] = byteString.charCodeAt(i);
    }

    return new Blob([intArray], { type: mimeString });
}

// fetch gửi form
async function submitFormWithImages(event) {
    event.preventDefault(); 
   
    const form = document.getElementById('studentInfo');
    const formData = new FormData(form); // Lấy dữ liệu form
    const cccdItems = document.querySelectorAll('#imagePreviewCCCD .image-item');
    const bhytItems = document.querySelectorAll('#imagePreviewBHYT .image-item');
    console.log(cccdItems);
    // thêm cccd
    for (const item of cccdItems) {
        const img = item.querySelector('.image');
       
        console.log(img);
        if (img) {
            const file = img.src;
            const response = await fetch(file);  // lấy file
            const blob = await response.blob();  //response => Blob (tệp ảnh)
            formData.append('CCCDImage', blob, 'image.png');
        }
    }
    // thêm bhyt
    for (const item of bhytItems) {
        const img = item.querySelector('.image');

        console.log(img);
        if (img) {
            const file = img.src;
            const response = await fetch(file);  // lấy file
            const blob = await response.blob();  //response => Blob (tệp ảnh)
            formData.append('BHYTImage', blob, 'image.png');
        }
    }
    formData.forEach((value, key) => console.log(`${key}: ${value}`));
    fetch(form.action, {
        method: 'POST',
        body:formData

    })
        .then(response => {
            if (!response.ok) {

                throw new Error("Không cập nhật được thông tin");
            }
            response.json();
        })
        .then(data => {

            
        })
}

async function SubmitFamiliar(event) {

    event.preventDefault();
    const form = document.getElementById('familyInfo');
    const formData = new FormData(form);
    const HKItems = document.querySelectorAll('#imagePreviewHK .image-item');
    console.log(HKItems);
    for (const item of HKItems) {
        const img = item.querySelector('.image');

        console.log(img);
        if (img) {
            const file = img.src;
            const response = await fetch(file);  // lấy file
            const blob = await response.blob();  //response => Blob (tệp ảnh)
            formData.append('HKImage', blob, 'image.png');
        }
    }

    // dữ liệu từ table

    // Xử lý dữ liệu từ bảng #infoTable (Relative)
    const tableRows = document.querySelectorAll('#infoTable tbody tr');
    const relatives = []; // Danh sách RelativeDTO

    for (const row of tableRows) {
        const cells = row.querySelectorAll('td.editable');
        const rowData = {
            RelativeType: cells[0]?.textContent.trim() || "",
            RelativeName: cells[1]?.textContent.trim() || "",
            DateOfBirth: cells[2]?.textContent.trim() || "",
            Career: cells[3]?.textContent.trim() || "",
            PlaceOfJob: cells[4]?.textContent.trim() || "",
        };
        relatives.push(rowData);
    }

    // Append danh sách RelativeDTO dưới dạng JSON
    formData.append('Relative', JSON.stringify(relatives));
    formData.forEach((value, key) => console.log(`${key}: ${value}`));

    fetch(form.action, {
        method: 'POST',
        body : formData
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Không cập nhật được");
            }
            return response.json();
        })
        
}

async function SubmitStudy(event) {

    event.preventDefault();
    const form = document.getElementById('studyProcessInfo');
    const formData = new FormData(form);
    const HBItems = document.querySelectorAll('#imagePreviewHB .image-item');
    console.log(HBItems);
    for (const item of HBItems) {
        const img = item.querySelector('.image');

        console.log(img);
        if (img) {
            const file = img.src;
            const response = await fetch(file);  // lấy file
            const blob = await response.blob();  //response => Blob (tệp ảnh)
            formData.append('HBImage', blob, 'image.png');
        }
    }

   
    formData.forEach((value, key) => console.log(`${key}: ${value}`));

    fetch(form.action, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                throw new Error("Không cập nhật được");
            }
            return response.json();
        })

}

async function SubmitAnother(event) {
    event.preventDefault(); // Ngăn chặn form gửi mặc định

    const form = document.getElementById('anotherGCNInfo');
    const formData = new FormData(form); // Khởi tạo FormData từ form

    // Lấy danh sách các thẻ img có src khác null
    const images = document.querySelectorAll('.ivTN, .ivKS, .ivCD, .ivNVQS');
    const validImages = Array.from(images).filter(img => img.src && img.src.trim() !== "");

    console.log("Ảnh có src hợp lệ:", validImages);

    // Xử lý các ảnh hợp lệ
    for (const img of validImages) {
        const src = img.src;
        try {
            const response = await fetch(src);
            const blob = await response.blob();

            // Thêm ảnh vào FormData với tên tương ứng
            if (img.classList.contains('ivTN')) {
                formData.append('TN', blob, 'TN_image.png');
            } else if (img.classList.contains('ivKS')) {
                formData.append('KS', blob, 'KS_image.png');
            } else if (img.classList.contains('ivCD')) {
                formData.append('CD', blob, 'CD_image.png');
            } else if (img.classList.contains('ivNVQS')) {
                formData.append('NVQS', blob, 'NVQS_image.png');
            }
        } catch (error) {
            console.error(`Lỗi khi tải ảnh từ ${src}:`, error);
        }
    }

    // Hiển thị FormData để kiểm tra
    formData.forEach((value, key) => console.log(`${key}:`, value));

    // Gửi form lên server
    try {
        const response = await fetch(form.action, {
            method: 'POST',
            body: formData
        });

        if (!response.ok) {
            throw new Error("Không cập nhật được");
        }

        const data = await response.json();
        console.log("Dữ liệu đã gửi thành công", data);
    } catch (error) {
        console.error("Lỗi khi gửi dữ liệu:", error);
    }
}

