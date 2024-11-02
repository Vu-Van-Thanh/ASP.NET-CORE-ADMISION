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

// Xử lý bảng
document.getElementById('add-relation').addEventListener('click', () => {
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
