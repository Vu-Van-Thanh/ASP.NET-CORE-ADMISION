document.addEventListener("DOMContentLoaded", function () {
    const searchBox = document.getElementById("search-box");
    const courseItems = document.querySelectorAll(".course-item");
    const showMoreButton = document.getElementById("show-more");

    // Hàm đặt lại trạng thái hiển thị mặc định
    const resetToDefaultView = () => {
        const currentVisible = parseInt(showMoreButton.getAttribute("data-visible"));
        courseItems.forEach((item, index) => {
            if (index < currentVisible) {
                item.classList.remove("hidden");
                item.style.display = "";
            } else {
                item.classList.add("hidden");
                item.style.display = "";
            }
        });

        // Hiển thị nút "Xem thêm" nếu còn ngành bị ẩn
        showMoreButton.style.display = currentVisible < courseItems.length ? "" : "none";
    };

    // Hàm xử lý tìm kiếm
    searchBox.addEventListener("input", function () {
        const query = searchBox.value.toLowerCase();

        if (query) {
            // Lọc danh sách dựa trên từ khóa
            courseItems.forEach(item => {
                const name = item.getAttribute("data-name").toLowerCase();
                if (name.includes(query)) {
                    item.classList.remove("hidden");
                    item.style.display = "";
                } else {
                    item.style.display = "none";
                }
            });


            showMoreButton.style.display = "none";
        } else {

            resetToDefaultView();
        }
    });

    // Xử lý khi nhấn nút "Xem thêm"
    showMoreButton.addEventListener("click", function () {
        const currentVisible = parseInt(showMoreButton.getAttribute("data-visible"));
        const newVisible = currentVisible + 5;

        courseItems.forEach((item, index) => {
            if (index < newVisible) {
                item.classList.remove("hidden");
                item.style.display = "";
            }
        });

        showMoreButton.setAttribute("data-visible", newVisible);

        // Ẩn nút nếu đã hiển thị hết
        if (newVisible >= courseItems.length) {
            showMoreButton.style.display = "none";
        }
    });

    // Xử lý khi nhấn vào ngành học
    courseItems.forEach(item => {
        item.addEventListener("click", (e) => {
            e.preventDefault();

            // Xóa trạng thái "active" khỏi tất cả ngành
            courseItems.forEach(el => el.classList.remove("active"));
            // Đánh dấu ngành hiện tại là "active"
            item.classList.add("active");

            // Lấy dữ liệu từ mục được nhấn
            const name = item.getAttribute("data-name");
            const description = item.getAttribute("data-description");
            const majorId = item.getAttribute("data-id");

            // Cập nhật nội dung hiển thị
            document.getElementById("course-title").textContent = name;
            document.getElementById("course-description").textContent = description;
            const registerButton = document.getElementById("register-button");
            registerButton.style.display = "block";
            registerButton.href = `/Major/ProgramDetail/${majorId}`;
        });
    });

    // Đặt trạng thái mặc định khi trang tải
    resetToDefaultView();
});


async function handleMajorClick(programId) {
    const yearSelect = document.getElementById('year-select');
    const iframe = document.getElementById('course-description-iframe');
    const year = yearSelect.value;

    try {
        const response = await fetch(`/Major/ProgramDetail?ProgramID=${programId}&year=${year}`);
        if (!response.ok) throw new Error('Không thể lấy dữ liệu ngành học');
        const data = await response.json();

        // Gán trực tiếp nội dung HTML vào iframe
        const iframeDoc = iframe.contentDocument || iframe.contentWindow.document;
        iframeDoc.open();
        iframeDoc.write(data.content);
        iframeDoc.close();
        const registerButton = document.getElementById("register-button");
        registerButton.style.display = "block";
        registerButton.href = `/Major/ProgramDetail/${programId}`;
    } catch (error) {
        console.error('Lỗi khi fetch dữ liệu ngành học:', error);
        alert('Không thể tải chi tiết ngành học. Vui lòng thử lại sau.');
    }
}


