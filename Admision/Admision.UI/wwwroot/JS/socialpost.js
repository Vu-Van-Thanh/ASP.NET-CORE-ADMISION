// post 
let Items = [];
let currentIndex = 0;

// render Post
const serverData = [
    {
        postId: "5cd8fdd6-fa7c-46b5-8f9a-8157118483d9",
        author: "Hóng Hớt Gen Z",
        timestamp: "2024-01-01T12:00:00Z",
        content: "Đây là nội dung bài đăng từ server.",
        mediaItems: [
            { type: "image", src: "../Data/hang_quoc_te.png" },
            { type: "image", src: "../Data/Sách.png" }
        ],
        likes: 10,
        comments: 5,
        shares: 2
    },
    {
        postId: "02000193-143f-7775-284b-2cd08ff64b9b",
        author: "Hóng Hớt Gen Z",
        timestamp: "2024-01-01T12:00:00Z",
        content: "Đây là nội dung bài đăng từ server.",
        mediaItems: [
            { type: "image", src: "../Data/hang_quoc_te.png" },
            { type: "image", src: "../Data/Đồng hồ.png" },
            { type: "image", src: "../Data/Đồ chơi.png" },
            { type: "image", src: "../Data/logo.png" },
            { type: "image", src: "../Data/task2.png" }

        ],
        likes: 10,
        comments: 5,
        shares: 2
    },
];

function renderPost(data) {
    const postList = document.querySelector('.post-list');
    let postsHTML = ''; // Khởi tạo HTML cho tất cả bài đăng

    // Duyệt qua từng bài đăng trong dữ liệu từ server
    data.forEach(post => {
        const postId = post.postId;
        const postContent = post.content;
        const author = post.author;
        const timestamp = formatTime(new Date(post.timestamp));
        const mediaItems = post.mediaItems;

        // Tạo HTML cho từng bài đăng
        let newPost = `
            <div class="post" id="post-${postId}">
                <div class="post-header">
                    <img src="https://via.placeholder.com/40" alt="Profile">
                    <div class="post-info">
                        <span class="post-author">${author}</span><br>
                        <span class="post-time">${timestamp}</span>
                    </div>
                </div>

                <div class="post-content">
                    ${postContent} <br>
                    <span style="color: #1877f2;">#honghotgenz</span>
                    <div id="post-preview-${postId}" class="post-preview" style="display: grid; gap: 5px;">
                        <!-- Nội dung media sẽ được thêm vào đây -->
                    </div>
                </div>

                <div class="post-interactions">
                    <div class="interaction-icons">
                        <i class="fas fa-thumbs-up"></i>
                        <span>${post.likes}</span>
                    </div>
                    <span>${post.comments} bình luận · ${post.shares} lượt chia sẻ</span>
                </div>

                <div class="post-actions">
                    <button class="action-btn like-btn" onclick="toggleLike('${postId}')">
                        <i class="far fa-thumbs-up"></i> Thích
                    </button>
                    <button class="action-btn" onclick="openCommentModal('${postId}')">
                        <i class="far fa-comment"></i> Bình luận
                    </button>
                    <button class="action-btn">
                        <i class="far fa-share-square"></i> Chia sẻ
                    </button>
                </div>
            </div>
        `;

        postsHTML += newPost; // Thêm HTML của từng bài đăng vào postsHTML
    });

    // Chèn tất cả bài đăng vào danh sách bài viết
    postList.innerHTML = postsHTML;

    // Gọi hàm hiển thị nội dung media cho từng bài đăng
    data.forEach(post => {
        displayMediaContentHTML(post.mediaItems, post.postId);
    });
}
renderPost(serverData);




// Hàm để mở form tạo bài viết
function openPostForm() {
    document.getElementById('postForm').style.display = 'flex';
}

// Hàm để đóng form tạo bài viết
function closePostForm() {
    document.getElementById('postForm').style.display = 'none';
}

// Hàm để toggle group con
function toggleGroup(element, groupID) {
    document.querySelectorAll('.group-box').forEach(box => box.classList.remove('active'));
    element.classList.toggle('active');

    const idPostElement = document.getElementById('GroupIDPost');
    if (idPostElement) {
        idPostElement.setAttribute('value', groupID);
    }
}

// bình luận
// Mở modal bình luận
let currentReplyCommentId = null;
function openCommentModal(postId) {
    var commentModal = new bootstrap.Modal(document.getElementById('commentModal'));
    commentModal.show();

    const postElement = document.getElementById(`post-${postId}`);
    document.getElementById('modalPostContent').innerHTML = postElement ? postElement.innerHTML : "<p>Bài viết không tồn tại.</p>";
    document.getElementById("commentPostID").setAttribute("data-comment-post-id", postId);
    document.getElementById('modalCommentSection').innerHTML = '';

    const comments = [
        { id: 1, Imgurl: "../Data/Bách hóa online.png", author: "Pham Hoang Nam", time: "4 ngày trước", text: "Xin chào, đây là bình luận!", avatar: "https://via.placeholder.com/40", level: 0, parentID: null },
        { id: 2, Imgurl: "../Data/task2.png", author: "Huyen Anh", time: "20 phút trước", text: "Tôi cũng muốn góp ý!", avatar: "https://via.placeholder.com/40", level: 0, parentID: null },
        { id: 3, Imgurl: "../Data/Thiết bị điện tử.png", author: "Lê Thị Hoa", time: "2 giờ trước", text: "Bình luận của tôi.", avatar: "https://via.placeholder.com/40", level: 1, parentID: 1 }
    ];

    comments.forEach(comment => {
        renderComment(comment);
    })
}

function renderComment(comment) {
    const marginLeft = comment.level * 30; // Thụt vào theo level

    // Tạo HTML cho comment-media nếu Imgurl không phải là null
    const mediaHtml = comment.Imgurl !== null
        ? `<div class="comment-media">
                <div class="media-wrapper">
                    <img src="${comment.Imgurl}" alt="Comment Image" class="comment-img">
                    <div class="icon-overlay">
                        <span class="zoom-icon" onclick="zoomMedia('${comment.Imgurl}','image')">
                            <i class="fas fa-search-plus"></i> <!-- Biểu tượng phóng to -->
                        </span>
                        <span class="delete-icon" onclick="deleteMedia(this)">
                            <i class="fas fa-trash-alt"></i> <!-- Biểu tượng xóa -->
                        </span>
                    </div>
                </div>
            </div>`
        : ''; // Không có Imgurl thì mediaHtml sẽ là chuỗi rỗng

    // Tạo HTML cho bình luận
    const commentHtml = `
        <div class="comment-container" id="comment-${comment.id}" style="margin-left: ${marginLeft}px; margin-top: 10px;">
            <img src="${comment.avatar}" alt="Avatar" class="comment-avatar">
            <div class="comment-content">
                <div class="comment-author">${comment.author} <span class="comment-time">${comment.time}</span></div>
                <div class="comment-text">${comment.text}</div>
                ${mediaHtml} <!-- Chỉ hiển thị khi mediaHtml có giá trị -->
                <div class="comment-actions">
                    <span class="like-btn">Thích</span>
                    <span class="reply-btn" onclick="replyToComment('${comment.id}','${comment.author}',${comment.level})">Trả lời</span>
                </div>
            </div>
        </div>
    `;

    // Tìm phần tử cha dựa trên parentID
    if (comment.parentID === null) {
        document.getElementById('modalCommentSection').insertAdjacentHTML('beforeend', commentHtml);
    } else {
        const parentComment = document.getElementById(`comment-${comment.parentID}`);
        if (parentComment) {
            parentComment.insertAdjacentHTML('afterend', commentHtml);
        }
    }
}

// Hiển thị ô nhập cho bình luận con ngay bên dưới comment cha

function replyToComment(parentId, parentAuthor, parentLevel) {
    currentReplyCommentId = parentId;
    document.querySelectorAll('.reply-input-container').forEach(container => container.remove());

    const newLevel = parentLevel + 1;
    const marginLeftValue = newLevel * 30;
    document.getElementById(`comment-${parentId}`).insertAdjacentHTML('afterend', `
        <div class="reply-container" style="margin-left: ${marginLeftValue}px;" >
        <div class="reply-input-container" >
            <img src="https://via.placeholder.com/40" alt="Profile" class="comment-profile-pic">
            <div class="reply-tools">
                <input id="reply-input" type="text" class="form-control reply-input" placeholder="Viết trả lời của bạn..." onkeydown="handleEnter(event, '${parentId}', ${newLevel})"/>
                <div class="tool-buttons">
                    <button id="reply-state" onclick="showEmojiPicker(event,'reply-state','reply-input')"><i class="fas fa-smile"></i></button>
                    <label for="upload-image" class="upload-label"><i class="fas fa-camera"></i></label>
                    <input type="file" id="upload-image" class="upload-image" accept="image/*, video/*" onchange="handleImageUpload(event,'${parentId}')" style="display: none;" />
                    <button onclick="submitReply('${parentId}', ${newLevel})">
                    <i class="fas fa-paper-plane"></i>
                    </button>
                    
                </div>
                
            </div>
            
        </div>
        <div id="media-preview-${parentId}" class="media-preview" style="display: none;"></div>
        </div>
        
    `);
}


function submitComment(event) {
    event.preventDefault(); 
    console.log('Submit comment triggered');
    const commentText = document.getElementById('text-input').value;
    const previewContainer = document.getElementById('image-previewSelf');
    const previewImg = document.getElementById('preview-imgSelf');
    const imageInput = document.getElementById('image-uploadSelf');
    const postId = document.getElementById("commentPostID").getAttribute("data-comment-post-id");

    const user = document.querySelector('.comment-profile-pic');
    const authorCommentId = user.dataset.authorcommentid;

    const currentDate = new Date();
    const dateCreated = currentDate.toISOString();
    // Kiểm tra xem có văn bản hoặc ảnh nào để gửi không
    if (commentText.trim() === '' && (!previewImg.src || previewImg.src === '')) return;
    const postForm = document.getElementById('comment-form');
    const formData = new FormData(postForm);
    formData.append("commentText", commentText);
    formData.append("postID", postId);
    formData.append("CreatedDate", dateCreated);
    formData.append("AuthorID", authorCommentId);
    if (imageInput && imageInput.files.length > 0) {
        formData.append('image', imageInput.files[0]);
    }

    
    console.log([...formData]);
    fetch(postForm.action, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            console.log("đã vào");
            if (response.ok) {
                return response.json();
            }
            else {
                throw new Error("Có lỗi xảy ra khi gửi bình luận")
            }
        })
        .then(data => {
            // Tạo HTML cho phần media (nếu có ảnh)
            let mediaHtml = '';
            const newCommentId = data.commentID;
            const userAvatar = document.getElementById('profile-pic-post').src;

            if (imageInput.files && imageInput.files[0]) {
                const newImg = document.createElement('img');
                newImg.src = URL.createObjectURL(imageInput.files[0]);
                mediaHtml = `<div class="media-wrapper">${newImg.outerHTML}
            <div class="icon-overlay">
                <span class="zoom-icon" onclick="zoomMedia('${newImg.src}','image')">
                    <i class="fas fa-search-plus"></i> <!-- Biểu tượng phóng to -->
                </span>
                <span class="delete-icon" onclick="deleteMedia(this)">
                    <i class="fas fa-trash-alt"></i> <!-- Biểu tượng xóa -->
                </span>
            </div>
        </div>`;
            }
            // Thêm bình luận mới vào phần bình luận
            document.getElementById('modalCommentSection').innerHTML += `
        <div class="comment-container" id="comment-${newCommentId}">
            <img src="${userAvatar}" alt="Avatar" class="comment-avatar">
            <div class="comment-content">
                <div class="comment-author">Bạn <span class="comment-time">Vừa xong</span></div>
                <div class="comment-text">${commentText}</div>
                ${mediaHtml ? `<div class="comment-media">${mediaHtml}</div>` : ''}
                <div class="comment-actions">
                    <span class="like-btn">Thích</span>
                    <span class="reply-btn" onclick="replyToComment('${newCommentId}','Bạn',0)">Trả lời</span>
                </div>
            </div>
        </div>
        `;

            // Xóa nội dung nhập và reset ảnh xem trước
            document.getElementById('text-input').value = '';
            previewImg.src = ''; // Xóa ảnh xem trước bằng cách đặt src thành rỗng
            previewContainer.style.display = 'none'; // Ẩn phần xem trước ảnh
            imageInput.value = ''; // Đặt lại input file để có thể chọn ảnh khác


        })


}

// ô nhập 
const inputField = document.getElementById('text-input');
inputField.addEventListener('keydown', function (event) {

    if (event.key === 'Enter' && event.shiftKey) {
        // Ngăn sự kiện Enter mặc định
        event.preventDefault();

        // Thêm '\n' vào vị trí con trỏ trong ô input
        const cursorPosition = inputField.selectionStart;
        const text = inputField.value;
        inputField.value = text.slice(0, cursorPosition) + '\n' + text.slice(cursorPosition);

        // Đặt lại con trỏ sau ký tự xuống dòng
        inputField.setSelectionRange(cursorPosition + 1, cursorPosition + 1);
    }
});

function submitReply(parentId, level) {
    const replyInput = document.querySelector('.reply-input');
    const replyText = replyInput.value;
    const previewContainer = document.getElementById(`media-preview-${parentId}`);

    if (replyText.trim() === '' && (!previewContainer || previewContainer.children.length === 0)) return;

    const userAvatar = "https://via.placeholder.com/40";
    const newCommentId = generateGuid(); // Tạo ID mới cho comment trả lời

    // Xây dựng HTML cho phần media (nếu có ảnh/video)
    let mediaHtml = '';
    if (previewContainer && previewContainer.children.length > 0) {
        Array.from(previewContainer.children).forEach(media => {
            mediaHtml += `<div class="media-wrapper">${media.outerHTML}</div>`;
        });
    }

    // Render bình luận trả lời ngay dưới bình luận cha
    document.getElementById(`comment-${parentId}`).insertAdjacentHTML('afterend', `
        <div class="comment-container" id="comment-${newCommentId}" style="margin-left: ${level * 30}px;">
            <img src="${userAvatar}" alt="Avatar" class="comment-avatar">
            <div class="comment-content">
                <div class="comment-author">Bạn <span class="comment-time">Vừa xong</span></div>
                <div class="comment-text">${replyText}</div>
                ${mediaHtml ? `<div class="comment-media">${mediaHtml}</div>` : ''}
                <div class="comment-actions">
                    <span class="like-btn">Thích</span>
                    <span class="reply-btn" onclick="replyToComment('${newCommentId}', 'Bạn', ${level})">Trả lời</span>
                </div>
            </div>
        </div>
    `);

    // Xóa nội dung trong ô nhập và media-preview sau khi gửi bình luận
    replyInput.value = '';
    document.querySelector('.reply-input-container').remove();
    if (previewContainer) {
        previewContainer.innerHTML = '';
        previewContainer.style.display = 'none'; // Ẩn lại `media-preview` sau khi gửi
    }
}


function generateGuid() {
    const timestamp = Date.now().toString(16);

    return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
        const r = Math.random() * 16 | 0;
        const v = c === 'x' ? r : (r & 0x3 | 0x8);
        return v.toString(16);
    }).replace(/^.{13}/, timestamp); // Thay thế phần đầu bằng timestamp để tạo GUID duy nhất
}

// icon

function handleImageUpload(event, parentId) {
    const file = event.target.files[0];
    if (file) {
        const previewContainer = document.getElementById(`media-preview-${parentId}`);

        if (!previewContainer) {
            console.error(`Không tìm thấy 'media-preview-${parentId}' để hiển thị ảnh.`);
            return;
        }

        const fileType = file.type.split('/')[0];
        previewContainer.style.display = 'flex';

        const reader = new FileReader();
        reader.onload = function (e) {
            const mediaHtml = `
                <div class="media-wrapper">
                    ${fileType === 'image' ? `<img src="${e.target.result}" alt="Ảnh đính kèm" class="attached-media">` :
                    `<video src="${e.target.result}" controls class="attached-media"></video>`}
                    <div class="icon-overlay">
                        <span class="zoom-icon" onclick="zoomMedia('${e.target.result}', '${fileType}')"><i class="fas fa-search-plus"></i></span>
                        <span class="delete-icon" onclick="deleteMedia(this)"><i class="fas fa-trash-alt"></i></span>
                    </div>
                </div>
            `;
            previewContainer.insertAdjacentHTML('beforeend', mediaHtml);
        };
        reader.readAsDataURL(file);
    }
}


// media comment modal
function zoomMedia(mediaUrl, type) {
    const zoomModal = document.getElementById('zoomModal');
    const zoomModalContent = document.getElementById('zoomModalContentComment');

    // Xóa nội dung cũ
    zoomModalContent.innerHTML = '';

    // Kiểm tra loại file (image hoặc video) và tạo thẻ tương ứng
    if (type === 'image') {
        zoomModalContent.innerHTML = `<img src="${mediaUrl}" alt="Phóng to ảnh" class="zoom-image">`;
    } else if (type === 'video') {
        zoomModalContent.innerHTML = `<video src="${mediaUrl}" controls class="zoom-video"></video>`;
    }

    // Hiển thị modal
    zoomModal.style.display = 'block';
}

function closeZoomModal() {
    document.getElementById('zoomModal').style.display = 'none';
}

function deleteMedia(element) {
    // Xóa phần tử media-wrapper khỏi DOM
    element.closest('.media-wrapper').remove();
}

function handleEnter(event, parentId, level) {
    if (event.key === "Enter") {
        event.preventDefault(); // Ngăn chặn hành động mặc định của Enter (xuống dòng)
        submitReply(parentId, level);
    }
}


function triggerFileInput() {
    document.getElementById('image-uploadSelf').click();
}

function previewImage(event) {
    const preview = document.getElementById('image-previewSelf');
    const img = document.getElementById('preview-imgSelf');
    img.src = URL.createObjectURL(event.target.files[0]);
    preview.style.display = 'block';
}

function zoomImageComment() {
    const previewImg = document.getElementById('preview-imgSelf');
    const zoomModal = document.getElementById('zoomModal');
    const zoomModalContent = document.getElementById('zoomModalContentComment');

    // Hiển thị modal và ảnh trong modal
    zoomModal.style.display = 'block';
    zoomModalContent.innerHTML = `<img src="${previewImg.src}" style="width:100%; max-height: 90vh; object-fit: contain;" />`;
}

function removeImageComment() {
    const previewDiv = document.getElementById('image-previewSelf');
    previewDiv.style.display = 'none';
    document.getElementById('image-uploadSelf').value = ''; // Reset the file input

    // Xóa ảnh trong modal nếu có
    const zoomModalContent = document.getElementById('zoomModalContentComment');
    zoomModalContent.innerHTML = ''; // Xóa nội dung modal
}



/*function createPost() {
    const postList = document.querySelector('.post-list');
    const postContent = document.getElementById('postContent').value;
    const mediaItems = Items.slice(); // Lấy tất cả các media từ mảng Items
    console.log("Media items:", mediaItems);

    if (postContent === '' && mediaItems.length === 0) {
        alert("Vui lòng nhập nội dung hoặc thêm ảnh trước khi đăng bài!");
        return; // Dừng hàm nếu không có nội dung hoặc ảnh
    }
    // Tạo ID cho bài post mới
    const postId = generateGuid();

    // Tạo cấu trúc bài post mới
    const newPost = `
        <div class="post" id="post-${postId}">
            <div class="post-header">
                <img src="https://via.placeholder.com/40" alt="Profile">
                <div class="post-info">
                    <span class="post-author">Hóng Hớt Gen Z</span><br>
                    <span class="post-time">${formatTime(new Date())}</span>
                </div>
            </div>
    
            <div class="post-content">
                ${postContent} <br>
                <span style="color: #1877f2;">#honghotgenz</span>
                <div id="post-preview-${postId}" class="post-preview" style="display: grid; gap: 5px;">
                    <!-- Nội dung media sẽ được thêm vào đây -->
                </div>
            </div>
    
            <div class="post-interactions">
                <div class="interaction-icons">
                    <i class="fas fa-thumbs-up"></i>
                    <span>0</span>
                </div>
                <span>0 bình luận · 0 lượt chia sẻ</span>
            </div>
    
            <div class="post-actions">
                <button class="action-btn" onclick="likePost('post-${postId}')">
                    <i class="far fa-thumbs-up"></i> Thích
                </button>
                <button class="action-btn" onclick="openCommentModal('post-${postId}')">
                    <i class="far fa-comment"></i> Bình luận
                </button>
                <button class="action-btn">
                    <i class="far fa-share-square"></i> Chia sẻ
                </button>
            </div>
        </div>
    `;

    // Chèn bài post mới lên đầu danh sách bài post
    postList.insertAdjacentHTML('afterbegin', newPost);

    // Gọi hàm hiển thị nội dung media
    displayMediaContentHTML(mediaItems, postId);

    // Xóa nội dung trong form sau khi đăng
    *//*document.getElementById('postContent').value = '';
Items.length = 0; // Xóa media items sau khi đăng
document.getElementById('mediaPreview').innerHTML = '';*//*
}*/

async function createPost(event) {
    event.preventDefault();  // Ngừng việc gửi form mặc định

    const postForm = document.getElementById('postForm');
    const formData = new FormData(postForm); // Tạo FormData từ form

    const postContent = document.getElementById('postContent').value;
    const groupId = document.getElementById('GroupIDPost').value;  // Lấy GroupID từ hidden input
    const userID = document.getElementById('UserIDPost').value;
    const userName = document.getElementById('authorName').innerHTML;
    console.log(userName);
    const profilePicSrc = document.getElementById('profile-pic-post').src;
    const currentDate = new Date();
    const dateCreated = currentDate.toISOString();

    // Thêm PostText và GroupID vào FormData
    formData.append('PostText', postContent);
    formData.append('GroupID', groupId);
    formData.append('AuthorID', userID);
    formData.append('AuthorName', userName);
    formData.append('DateCreated', dateCreated);


    // Duyệt qua tất cả các media items từ preview
    const mediaItems = document.querySelectorAll('.media-itemPost');

    // Duyệt qua tất cả media items và thêm chúng vào FormData
    for (const item of mediaItems) {
        const img = item.querySelector('.preview-img');
        const video = item.querySelector('.preview-video');

        if (img) {
            const file = img.src;
            const response = await fetch(file);  // lấy file
            const blob = await response.blob();  //response => Blob (tệp ảnh)
            formData.append('mediaFiles', blob, 'image.png');
        }

        if (video) {
            const file = video.src;
            const response = await fetch(file);
            const blob = await response.blob();
            formData.append('mediaFiles', blob, 'video.mp4');
        }
    }

    // Gửi formData qua fetch hoặc XMLHttpRequest
    fetch(postForm.action, {
        method: 'POST',
        body: formData
    })
        .then(response => response.json())
        .then(data => {
            console.log("Post đã được gửi lên server:", data);
            // Nếu thành công, tạo bài post mới trên trang
            const postList = document.querySelector('.post-list');
            const postId = data.postID;
            console.log("Post id la : ", postId);
            const mediaItems = Items.slice(); // Lấymedia (Items)
            const newPost = `
            <div class="post" id="post-${postId}">
                <div class="post-header">
                    <img src="${profilePicSrc}" alt="Profile">
                    <div class="post-info">
                        <span class="post-author">${userName}</span><br>
                        <span class="post-time">${formatTime(new Date(dateCreated))}</span>
                    </div>
                </div>
        
                <div class="post-content">
                    ${postContent} <br>
                    <span style="color: #1877f2;">#honghotgenz</span>
                    <div id="post-preview-${postId}" class="post-preview" style="display: grid; gap: 5px;">
                        <!-- Nội dung media sẽ được thêm vào đây -->
                    </div>
                </div>
        
                <div class="post-interactions">
                    <div class="interaction-icons">
                        <i class="fas fa-thumbs-up"></i>
                        <span>0</span>
                    </div>
                    <span>0 bình luận · 0 lượt chia sẻ</span>
                </div>
        
                <div class="post-actions">
                    <button class="action-btn like-btn" onclick="toggleLike('${postId}')">
                        <i class="far fa-thumbs-up"></i> Thích
                    </button>
                    <button class="action-btn" onclick="openCommentModal('${postId}')">
                        <i class="far fa-comment"></i> Bình luận
                    </button>
                    <button class="action-btn">
                        <i class="far fa-share-square"></i> Chia sẻ
                    </button>
                </div>
            </div>
        `;

            // Chèn bài post mới lên đầu danh sách bài post
            postList.insertAdjacentHTML('afterbegin', newPost);

            // Gọi hàm hiển thị nội dung media
            displayMediaContentHTML(mediaItems, postId);
            document.getElementById('postContent').value = '';  // Xóa nội dung textarea
            Items.length = 0;
            document.getElementById('mediaPreview').innerHTML = '';  // Xóa preview media

        })
        .catch(error => {
            console.error("Có lỗi xảy ra:", error);

            // Nếu có lỗi trong phân tích JSON, in chi tiết thông báo
            if (error.message) {
                console.error("Thông điệp lỗi:", error.message);
            }
            if (error.stack) {
                console.error("Stack trace lỗi:", error.stack);
            }

            alert("Có lỗi xảy ra khi đăng bài.");
        });
    closePostForm();
}


// Chuyển dataURL thành Blob
function dataURLtoBlob(dataURL) {
    const byteString = atob(dataURL.split(',')[1]);
    const mimeString = dataURL.split(',')[0].split(':')[1].split(';')[0];
    const ab = new ArrayBuffer(byteString.length);
    const ia = new Uint8Array(ab);
    for (let i = 0; i < byteString.length; i++) {
        ia[i] = byteString.charCodeAt(i);
    }
    return new Blob([ab], { type: mimeString });
}

function displayMediaContentHTML(mediaItems, postId) {
    const mediaPreview = document.getElementById(`post-preview-${postId}`);
    let mediaHTML = '';

    // Thiết lập bố cục dựa trên số lượng media
    if (mediaItems.length === 1) {
        mediaPreview.style.gridTemplateColumns = "1fr";
        mediaPreview.style.gridTemplateRows = "1fr";
        // Nếu chỉ có 1 ảnh, hiển thị ảnh toàn bộ
        mediaHTML += `<div class="media-itemBlog">${generateMediaHTML(mediaItems[0], postId)}</div>`;
    } else if (mediaItems.length === 2) {
        // Nếu có 2 ảnh, hiển thị 2 cột
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr";
        mediaHTML += `
            <div class="media-itemBlog">${generateMediaHTML(mediaItems[0], postId)}</div>
            <div class="media-itemBlog">${generateMediaHTML(mediaItems[1], postId)}</div>
        `;
    } else if (mediaItems.length === 3) {
        // Nếu có 3 ảnh, bố cục 1 ảnh lớn ở trên, 2 ảnh nhỏ chia đều ở dưới
        mediaPreview.style.display = "grid";
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr 1fr";
        mediaPreview.style.gridTemplateAreas = `
            "item1 item1"
            "item2 item3"
        `;

        mediaHTML += `
            <div class="media-itemBlog" style="grid-area: item1;">${generateMediaHTML(mediaItems[0], postId)}</div>
            <div class="media-itemBlog" style="grid-area: item2;">${generateMediaHTML(mediaItems[1], postId)}</div>
            <div class="media-itemBlog" style="grid-area: item3;">${generateMediaHTML(mediaItems[2], postId)}</div>
        `;
    } else {
        // Bố cục cho 4 ảnh trở lên, với overlay trên ảnh thứ 4 nếu có hơn 4 ảnh
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr 1fr";

        mediaItems.forEach((item, index) => {
            if (index < 4) {
                // Hiển thị 4 ảnh đầu tiên
                if (index === 3 && mediaItems.length > 4) {
                    // Ảnh thứ 4 có overlay nếu có hơn 4 ảnh
                    mediaHTML += `
                        <div class="media-itemBlog" style="position: relative;">
                            ${generateMediaHTML(item, postId)}
                            <div class="overlay" onclick="openModalBlog('${postId}')" style="position: absolute; top: 0; left: 0; right: 0; bottom: 0; background-color: rgba(0, 0, 0, 0.6); color: #fff; font-size: 18px; display: flex; align-items: center; justify-content: center;">
                                +${mediaItems.length - 4}
                            </div>
                        </div>`;
                } else {
                    // Các ảnh từ 1 đến 3
                    mediaHTML += `<div class="media-itemBlog">${generateMediaHTML(item, postId)}</div>`;
                }
            } else {
                // Các ảnh từ thứ 5 trở đi sẽ được thêm vào mediaHTML nhưng ẩn đi
                mediaHTML += `<div class="media-itemBlog" style="display: none;">${generateMediaHTML(item, postId)}</div>`;
            }
        });
    }

    mediaPreview.innerHTML = mediaHTML;
}


// Hàm tạo HTML cho từng phần tử media (ảnh hoặc video)
function generateMediaHTML(item, postId) {
    if (item.type === "image") {
        return `<img src="${item.src}" alt="Post Image" style="width: 100%; height: 100%; object-fit: cover;" onclick="openModalBlog('${postId}')">`;
    } else if (item.type === "video") {
        return `<video src="${item.src}" controls style="width: 100%; height: 100%; object-fit: cover;" onclick="openModalBlog('${postId}')"></video>`;
    }
    return '';
}




function formatTime(date) {
    const hours = date.getHours();
    const minutes = date.getMinutes();
    const ampm = hours >= 12 ? 'PM' : 'AM';
    const formattedHours = hours % 12 || 12;
    const formattedMinutes = minutes < 10 ? '0' + minutes : minutes;
    return `${formattedHours}:${formattedMinutes} ${ampm}`;
}




// emoji
let emojis = {};
let emojiPickerVisible = false;
let targetInputId = ""; // Biến để lưu ID của ô input hiện tại

// Hàm lấy emoji từ file JSON
function loadEmojis() {
    fetch("/AnotherData/emojiList.json")
        .then(response => {
            if (!response.ok) throw new Error("Không tìm thấy file emojiList.json");
            return response.json();
        })
        .then(data => {
            emojis = data;
            console.log("Dữ liệu emoji đã được tải:", emojis);
            showEmojis("smileys");
        })
        .catch(error => console.error("Lỗi tải emoji:", error));
}

// Hàm hiển thị các emoji trong danh mục đã chọn
function showEmojis(category) {
    const emojiGrid = document.getElementById("emoji-list");
    if (!emojiGrid) {
        console.error("Không tìm thấy phần tử emoji-list");
        return;
    }
    emojiGrid.innerHTML = ""; // Xóa các emoji cũ

    if (emojis[category]) {
        emojis[category].forEach(emoji => {
            const emojiButton = document.createElement("button");
            emojiButton.textContent = emoji;
            emojiButton.classList.add("emoji-button");
            emojiButton.onclick = () => addEmojiToInput(emoji, targetInputId); // Sử dụng targetInputId
            emojiGrid.appendChild(emojiButton);
        });
    } else {
        console.error("Không tìm thấy danh mục emoji:", category);
    }
}

// Hàm mở hộp chọn emoji
function showEmojiPicker(event, iconId, inputId) {
    event.stopPropagation(); // Ngăn sự kiện click lan ra ngoài

    targetInputId = inputId; // Gán ID của ô input vào biến toàn cục
    const emojiPicker = document.getElementById("emoji-selector");
    const icon = document.getElementById(iconId);

    if (!emojiPicker) {
        console.error("Không tìm thấy phần tử emoji-selector");
        return;
    }
    if (!icon) {
        console.error("Không tìm thấy biểu tượng với ID:", iconId);
        return;
    }

    const iconPosition = icon.getBoundingClientRect();
    const modalContent = icon.closest('.modal-content');
    const modalPosition = modalContent.getBoundingClientRect();
    // Tính vị trí của emoji picker dựa trên modal và icon
    const top = iconPosition.bottom - modalPosition.top - 80; // khoảng cách 5px
    const left = iconPosition.left - modalPosition.left;

    emojiPicker.style.position = "absolute";
    emojiPicker.style.top = `${top}px`;
    emojiPicker.style.left = `${left}px`;

    if (!emojiPickerVisible) {
        loadEmojis();
        emojiPickerVisible = true;
        emojiPicker.style.display = "block";
    } else {
        closeEmojiPicker();
    }
}

// Hàm thêm emoji vào ô input
function addEmojiToInput(emoji, inputId) {
    const textInput = document.getElementById(inputId);
    if (!textInput) {
        console.error("Không tìm thấy phần tử input với ID:", inputId);
        return;
    }
    textInput.value += emoji;
}

// Thay đổi danh mục khi nhấn vào biểu tượng danh mục
document.querySelectorAll('.category-button').forEach(button => {
    button.addEventListener('click', () => {
        const category = button.getAttribute("data-category");
        showEmojis(category);
    });
});

// Đóng hộp chọn emoji khi nhấn ra ngoài
document.addEventListener("click", function (event) {
    const emojiPicker = document.getElementById("emoji-selector");
    if (emojiPickerVisible && !emojiPicker.contains(event.target) && !event.target.classList.contains("fa-smile")) {
        closeEmojiPicker();
    }
});

function closeEmojiPicker() {
    const emojiPicker = document.getElementById("emoji-selector");
    emojiPicker.style.display = "none";
    emojiPickerVisible = false;
}






function previewPostMedia(event) {
    const files = event.target.files;
    const mediaPreview = document.getElementById("mediaPreview");

    // Hiển thị khung nếu có file
    if (files.length > 0) {
        mediaPreview.style.display = "grid";
    }

    Array.from(files).forEach((file) => {
        const reader = new FileReader();
        reader.onload = function (e) {
            if (file.type.startsWith("image/")) {
                Items.push({ type: "image", src: e.target.result });
            } else if (file.type.startsWith("video/")) {
                Items.push({ type: "video", src: e.target.result });
            }
            updateMediaPreview();
        };
        reader.readAsDataURL(file); // Đọc file dưới dạng URL
    });
}

function updateMediaPreview() {
    const mediaPreview = document.getElementById("mediaPreview");
    mediaPreview.innerHTML = ""; // Xóa nội dung cũ

    // Thiết lập bố cục dựa trên số lượng media
    if (Items.length === 1) {
        mediaPreview.style.gridTemplateColumns = "1fr";
        mediaPreview.style.gridTemplateRows = "1fr";
    } else if (Items.length === 2) {
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr";
    } else if (Items.length === 3) {
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr 1fr";
        mediaPreview.style.gridTemplateAreas = `
            "item1 item2"
            "item1 item3"
        `;
    } else {
        mediaPreview.style.gridTemplateColumns = "1fr 1fr";
        mediaPreview.style.gridTemplateRows = "1fr 1fr";
        mediaPreview.style.gridTemplateAreas = "";
    }

    Items.forEach((file, index) => {
        const mediaItem = document.createElement("div");
        mediaItem.classList.add("media-itemPost");

        // Thêm ảnh hoặc video vào media item
        if (file.type === "image") {
            const img = document.createElement("img");
            img.src = file.src;
            img.classList.add("preview-img");
            img.onclick = () => openModal(index);
            mediaItem.appendChild(img);
        } else if (file.type === "video") {
            const video = document.createElement("video");
            video.src = file.src;
            video.controls = true;
            video.classList.add("preview-video");
            mediaItem.appendChild(video);
        }

        // Đặt vị trí các phần tử media
        if (Items.length === 3) {
            if (index === 0) {
                mediaItem.style.gridArea = "item1";
            } else if (index === 1) {
                mediaItem.style.gridArea = "item2";
            } else if (index === 2) {
                mediaItem.style.gridArea = "item3";
            }
        }

        // Hiển thị icon xóa
        const deleteIcon = document.createElement("span");
        deleteIcon.classList.add("deletePost-icon");
        deleteIcon.textContent = "×";
        deleteIcon.onclick = (e) => {
            e.stopPropagation();
            deleteImage(index);
        };
        mediaItem.appendChild(deleteIcon);
        // Ẩn các phần tử có chỉ số lớn hơn 3
        if (index >= 4) {
            mediaItem.style.display = "none"; // Ẩn các phần tử thứ 5 trở đi
        }

        // Hiển thị overlay nếu có hơn 4 media items
        if (index === 3 && Items.length > 4) {
            const overlayDiv = document.createElement("div");
            overlayDiv.classList.add("overlay");
            overlayDiv.textContent = `+${Items.length - 4}`;
            overlayDiv.onclick = () => openModal(4); // Mở modal cho các mục còn lại
            mediaItem.appendChild(overlayDiv);
        }

        mediaPreview.appendChild(mediaItem);
    });
}

function deleteImage(index) {
    Items.splice(index, 1); // Remove from images array
    updateMediaPreview(); // Re-render preview
}
function openModal(index) {
    currentIndex = index;
    const modal = document.getElementById("imageModalPost");
    const modalContent = document.getElementById("modal-postContent");
    modal.style.display = "flex";

    // Kiểm tra loại phương tiện và cập nhật nội dung modal
    const item = Items[index];
    if (item.type === "image") {
        modalContent.innerHTML = `<img src="${item.src}" alt="Image">`;
    } else if (item.type === "video") {
        modalContent.innerHTML = `<video src="${item.src}" controls autoplay style="width: 80%; max-height: 80vh;"></video>`;
    }
}

function closeModalPost() {
    const modal = document.getElementById("imageModalPost");
    modal.style.display = "none";
}

function changeImagePost(direction) {
    currentIndex += direction;
    if (currentIndex < 0) currentIndex = Items.length - 1;
    if (currentIndex >= Items.length) currentIndex = 0;

    const modalContent = document.getElementById("modal-postContent");

    // Kiểm tra loại phương tiện và cập nhật nội dung modal
    const item = Items[currentIndex];
    if (item.type === "image") {
        modalContent.innerHTML = `<img src="${item.src}" alt="Image">`;
    } else if (item.type === "video") {
        modalContent.innerHTML = `<video src="${item.src}" controls autoplay style="width: 80%; max-height: 80vh;"></video>`;
    }
}



// Blog
let currentMediaItems = []; // Mảng chứa tất cả media của bài đăng
let currentMediaIndex = 0;  // Vị trí hiện tại trong modal

// Hàm mở modal và hiển thị media
function openModalBlog(postId) {
    // Lấy tất cả các media-itemBlog của bài đăng có postId này
    const postElement = document.getElementById(`post-${postId}`);
    const mediaItems = Array.from(postElement.querySelectorAll(".media-itemBlog"));

    // Tạo danh sách các media từ các phần tử media-itemBlog
    currentMediaItems = mediaItems.map(item => {
        const img = item.querySelector("img");
        const video = item.querySelector("video");
        return img ? { type: "image", src: img.src } : { type: "video", src: video.src };
    });

    currentMediaIndex = 0; // Đặt vị trí ban đầu
    showMediaInModal(currentMediaIndex); // Hiển thị media đầu tiên
    document.getElementById("modalBlog").style.display = "flex"; // Hiển thị modal
}

// Hàm đóng modal
function closeModalBlog() {
    document.getElementById("modalBlog").style.display = "none";
    currentMediaItems = []; // Xóa mảng media sau khi đóng modal
}

// Hàm hiển thị media trong modal theo index
function showMediaInModal(index) {
    const modalImg = document.getElementById("modalBlogImage");
    const modalVideo = document.getElementById("modalBlogVideo");
    const media = currentMediaItems[index];

    // Ẩn cả ảnh và video trước, sau đó hiển thị loại media phù hợp
    modalImg.style.display = "none";
    modalVideo.style.display = "none";

    if (media.type === "image") {
        modalImg.src = media.src;
        modalImg.style.display = "block";
    } else if (media.type === "video") {
        modalVideo.src = media.src;
        modalVideo.style.display = "block";
    }
}

// Hàm chuyển media trong modal
function changeMediaBlog(direction) {
    currentMediaIndex += direction;
    if (currentMediaIndex < 0) {
        currentMediaIndex = currentMediaItems.length - 1;
    } else if (currentMediaIndex >= currentMediaItems.length) {
        currentMediaIndex = 0;
    }
    showMediaInModal(currentMediaIndex);
}



// sự kiện nút like
function toggleLike(postId) {
    // Thêm "post-" vào trước id để tìm phần tử
    const postElement = document.getElementById(`post-${postId}`);

    // Kiểm tra phần tử bài đăng có tồn tại
    if (!postElement) {
        console.error(`Không tìm thấy bài đăng với id post-${id}`);
        return;
    }

    // Tìm đến nút Like của bài đăng
    const likeButton = postElement.querySelector(".like-btn");

    // Tìm đến phần tử span bên trong interaction-icons
    const interactionIcons = postElement.querySelector(".interaction-icons span");

    // Kiểm tra phần tử interactionIcons có tồn tại
    if (!interactionIcons) {
        console.error("Không tìm thấy phần tử span trong interaction-icons");
        return;
    }

    // Lấy số lượt thích hiện tại
    let currentCount = parseInt(interactionIcons.innerText);

    // Kiểm tra nếu người dùng đã thích (dựa trên lớp 'like-btn liked' trên nút)
    if (likeButton.classList.contains("liked")) {
        // Nếu đã thích, hủy like
        currentCount -= 1;
        likeButton.classList.remove("liked");
    } else {
        // Nếu chưa thích, thêm like
        currentCount += 1;
        likeButton.classList.add("liked");
    }

    // Cập nhật lại số lượt thích
    interactionIcons.innerText = currentCount;
}