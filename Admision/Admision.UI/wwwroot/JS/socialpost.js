// post 
let Items = [];
let currentIndex = 0;

let page = 2; 
const pageSize = 10; 
let isLoading = false; 

// render Post
let serverData = [
    {
        postId: "5cd8fdd6-fa7c-46b5-8f9a-8157118483d9",
        author: "Hóng Hớt Gen Z",
        timestamp: "2024-01-01T12:00:00Z",
        content: "Đây là nội dung bài đăng từ server.",
        mediaItems: [
            { type: "image", src: "/PostMedia/1f600afb-42d3-4e65-8910-0ded8114fedf/image_1.png" },
            { type: "image", src: "/PostMedia/1f600afb-42d3-4e65-8910-0ded8114fedf/image_1.png" },
            { type: "image", src: "/PostMedia/1f600afb-42d3-4e65-8910-0ded8114fedf/image_1.png" }
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
            { type: "image", src: "/PostMedia/1f600afb-42d3-4e65-8910-0ded8114fedf/image_1.png" },
            { type: "image", src: "/PostMedia/1f600afb-42d3-4e65-8910-0ded8114fedf/image_1.png" }


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
                    <span>${post.comments} bình luận</span>
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

function renderPostAfterFetch(data) {
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
                    <span>${post.comments} bình luận</span>
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

        postsHTML += newPost; 
    });

    // Chèn tất cả bài đăng vào danh sách bài viết
    postList.innerHTML += postsHTML;

    // Gọi hàm hiển thị nội dung media cho từng bài đăng
    data.forEach(post => {
        displayMediaContentHTML(post.mediaItems, post.postId);
    });
}

// fetch post
function fetchPosts() {
    if (isLoading) return;
    console.log("đang fetch");
    // Lấy groupID từ thuộc tính data-group-id của post-list
    const postList = document.querySelector('.post-list');
    const groupID = postList ? postList.getAttribute('data-group-id') : null;

    // Kiểm tra xem groupID có hợp lệ hay không
    if (!groupID) {
        console.error('Không có groupID!');
        return;
    }

    isLoading = true;
    document.getElementById('loadingIndicator').style.display = 'block';
    loadingIndicator.textContent = 'Đang tải...'; 

    // Fetch bài viết từ server với groupID
    fetch(`/BlogChat/GetPostByGroup?groupID=${groupID}&page=${page}&pageSize=${pageSize}`)
        .then(response => {
            if (!response.ok) throw new Error('Lỗi khi tải bài viết');
            return response.json();
        })
        .then(data => {
            if (Array.isArray(data) && data.length === 0) {
                console.log('Không có bài viết mới.');
                loadingIndicator.textContent = 'Không có bài viết mới';
                return; 
                isLoading = false;
            }
            console.log(data);
            renderPostAfterFetch(data);
            page++;
            isLoading = false;
            document.getElementById('loadingIndicator').style.display = 'none';
        })
        .catch(err => console.error("Lỗi khi tải dữ liệu:", err))
}
/*document.querySelector('.post-list').addEventListener('scroll', function () {
    const postList = document.querySelector('.post-list');
    const scrollHeight = postList.scrollHeight;
    const scrollTop = postList.scrollTop;
    const clientHeight = postList.clientHeight;

    // Kiểm tra nếu người dùng đã cuộn đến cuối
    if (scrollHeight - scrollTop <= clientHeight + 5) {
        fetchPosts();
    }
});*/
document.querySelector('.post-list').addEventListener('scroll', function () {
    const postList = document.querySelector('.post-list');

    const scrollTop = postList.scrollTop; // Vị trí cuộn hiện tại
    const scrollHeight = postList.scrollHeight; // Tổng chiều cao nội dung có thể cuộn
    const clientHeight = postList.clientHeight; // Chiều cao khung nhìn (phần nhìn thấy)

    // Kiểm tra nếu còn cách 10px tới cuối
    if (scrollHeight - (scrollTop + clientHeight) <= 10) {
        console.log("Còn cách 10px, bắt đầu fetch...");
        fetchPosts();
    }
});

// Hàm để toggle group con
function toggleGroup(element, groupID) {
    const postList = document.querySelector('.post-list');
    postList.setAttribute('data-group-id', groupID);
    document.querySelectorAll('.group-box').forEach(box => box.classList.remove('active'));
    element.classList.toggle('active');

    const idPostElement = document.getElementById('GroupIDPost');
    if (idPostElement) {
        idPostElement.setAttribute('value', groupID);
    }

    // fetch data
    const url = `/BlogChat/GetPostByGroup?groupID=${groupID}&page=1&pageSize=10`;
    fetch(url)
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('Dữ liệu nhận được:', data);
            serverData = data;
            console.log('Dữ liệu sau khi nhận được:', serverData);
            renderPost(serverData);
        })
        .catch(error => {
            console.error('Có lỗi xảy ra:', error);
        });

}


// Hàm để mở form tạo bài viết
function openPostForm() {
    document.getElementById('postForm').style.display = 'flex';
}

// Hàm để đóng form tạo bài viết
function closePostForm() {
    document.getElementById('postForm').style.display = 'none';
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
    /*document.getElementById('modalCommentSection').innerHTML = '';*/
    const commentContainers = document.querySelectorAll('#modalCommentSection .comment-container');
    commentContainers.forEach(container => container.remove());
    let comments = [
        { id: 1, imgurl: "../Data/Bách hóa online.png", author: "Pham Hoang Nam", time: "4 ngày trước", text: "Xin chào, đây là bình luận!", avatar: "https://via.placeholder.com/40", level: 0, parentID: null },
        { id: 2, imgurl: "../Data/task2.png", author: "Huyen Anh", time: "20 phút trước", text: "Tôi cũng muốn góp ý!", avatar: "https://via.placeholder.com/40", level: 0, parentID: null },
        { id: 3, imgurl: "../Data/Thiết bị điện tử.png", author: "Lê Thị Hoa", time: "2 giờ trước", text: "Bình luận của tôi.", avatar: "https://via.placeholder.com/40", level: 1, parentID: 1 }
    ];
    const urlComment = `/BlogChat/GetCommentByPost?postId=${postId}&page=1&pageSize=10`;
    fetch(urlComment)
        .then(response => {
            if (!response.ok) {
                throw new Error('Có lỗi khi lấy dữ liệu bình luận');
                
            }
            return response.json();
        })
        .then(data => {
            console.log("Dữ liệu comment nhận được : ", data);

            comments = data;
            // Sắp xếp comments theo level từ thấp đến cao
            comments = data.sort((a, b) => a.level - b.level);
            console.log("Dữ liệu comment sau khi sắp xếp: ", comments);
            
            comments.forEach(comment => {
                renderComment(comment);
            })

        })
        .catch(error => {
            console.error('Có lỗi xảy ra:', error);
        });
    
}

function renderComment(comment) {
    const marginLeft = comment.level * 30; 
    const mediaHtml = comment.imgurl !== null
        ? `<div class="comment-media">
                <div class="media-wrapper">
                    <img src="${comment.imgurl}" alt="Comment Image" class="comment-img">
                    <div class="icon-overlay">
                        <span class="zoom-icon" onclick="zoomMedia('${comment.imgurl}','image')">
                            <i class="fas fa-search-plus"></i> 
                        </span>
                        <span class="delete-icon" onclick="deleteMedia(this)">
                            <i class="fas fa-trash-alt"></i> 
                        </span>
                    </div>
                </div>
            </div>`
        : ''; 

    
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

// tinh chỉnh
let pageComment = 2;
const pageSizeComment = 10;
let isLoadingComment = false;
function fetchComments() {
    if (isLoadingComment) return;
    console.log("đang fetch comment");
    // Lấy groupID từ thuộc tính data-group-id của post-list
    const postInfo = document.getElementById("commentPostID");
    const postID = postInfo ? postInfo.getAttribute('data-comment-post-id') : null;

    // Kiểm tra xem groupID có hợp lệ hay không
    if (!postID) {
        console.error('Không có postID!');
        return;
    }

    isLoadingComment = true;
    document.getElementById('loadingCommentIndicator').style.display = 'block';
    loadingCommentIndicator.textContent = 'Đang tải...';

    // Fetch bài viết từ server với groupID
    fetch(`/BlogChat/GetCommentByPost?postId=${postID}&page=${pageComment}&pageSize=${pageSizeComment}`)
        .then(response => {
            if (!response.ok) throw new Error('Lỗi khi tải bình luận');
            return response.json();
        })
        .then(data => {
            console.log("Dữ liệu comment", data);
            if (Array.isArray(data) && data.length === 0) {
                console.log('Không có bình luận mới.');
                loadingCommentIndicator.textContent = 'Không có bình luận mới';
                return;
                isLoading = false;
            }
            
            data.forEach(data => {
                renderComment(data);
            })
            page++;
            isLoading = false;
            document.getElementById('loadingCommentIndicator').style.display = 'none';
        })
        .catch(err => console.error("Lỗi khi tải dữ liệu:", err))
}
document.getElementById('modalCommentSection').addEventListener('scroll', function () {
    const modal = document.getElementById('modalCommentSection');

    const scrollTop = modal.scrollTop; // Vị trí cuộn hiện tại
    const scrollHeight = modal.scrollHeight; // Tổng chiều cao nội dung có thể cuộn
    const clientHeight = modal.clientHeight; // Chiều cao khung nhìn (phần nhìn thấy)

    // Kiểm tra nếu còn cách 10px tới cuối
    if (scrollHeight - (scrollTop + clientHeight) <= 10) {
        console.log("Còn cách 10px, bắt đầu fetch...");
        fetchComments();
    }
});

// Hiển thị ô nhập cho bình luận con ngay bên dưới comment cha

function replyToComment(parentId, parentAuthor, parentLevel) {
    console.log("ĐẦU VÀO replyToComment", parentId, parentAuthor, parentLevel);
    const newLevel = parentLevel + 1;
    const marginLeftValue = newLevel * 30;

    const replyForm = document.getElementById('replyFormComment');
    document.getElementById('parentCommentId').value = parentId;
    document.getElementById('commentLevel').value = newLevel;
    document.getElementById('parentAuthor').value = parentAuthor;
    replyForm.querySelector('.reply-container').style.marginLeft = `${marginLeftValue}px`;

    replyForm.querySelector('#reply-input').setAttribute('onkeydown', `handleEnter(event, '${parentId}', ${newLevel})`);
    replyForm.querySelector('#upload-image').setAttribute('onchange', `handleImageUpload(event, '${parentId}')`);
    replyForm.querySelector('#submitreply').setAttribute('onclick', `submitReply(event, '${parentId}', ${newLevel})`);
    const mediaPreview = replyForm.querySelector('.media-preview');
    if (mediaPreview) {
        mediaPreview.id = `media-preview-${parentId}`;
    }
    // chèn form dưới phần tử có parentId
    const parentElement = document.getElementById(`comment-${parentId}`);
    parentElement.insertAdjacentElement('afterend', replyForm);
    replyForm.style.display = 'block';

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

function submitReply(event,parentId, level) {
    event.preventDefault();
    const replyInput = document.querySelector('.reply-input');
    const replyText = replyInput.value;
    const previewContainer = document.getElementById(`media-preview-${parentId}`);
    const userAvatar = document.getElementById('profile-pic-post').src;
    const postId = document.getElementById("commentPostID").getAttribute("data-comment-post-id");
    const user = document.getElementById("commentPostID");
    const authorCommentId = user.dataset.authorid;
    console.log("user", authorCommentId);
    if (replyText.trim() === '' && (!previewContainer || previewContainer.children.length === 0)) return;
    const currentDate = new Date();
    const dateCreated = currentDate.toISOString();
    // lấy form 
    const replyform = document.getElementById('replyFormComment');
    const formdata = new FormData(replyform);
    formdata.append("commentText", replyText);
    formdata.append("parrentCommentID", parentId);
    formdata.append("CreatedDate", dateCreated);
    formdata.append("postID", postId);
    formdata.append("AuthorID", authorCommentId);
    formdata.append("level", level);
    const image = document.getElementById('upload-image');
    if (image && image.files.length > 0) {
        formdata.append("image", image.files[0]);
    }
    console.log("url", replyform.action);
    console.log([...formdata]);

    fetch(replyform.action, {
        method: 'POST',
        body: formdata
    })
        .then(response => {
            if (response.ok) {
                return response.json();
            }
            else {
                throw new Error("Có lỗi xảy ra khi gửi bình luận")
            }
        })
        .then(data => {
            // Xây dựng HTML cho phần media (nếu có ảnh/video)
            let mediaHtml = '';
            const newCommentId = data.commentID;
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
            replyform.style.display = 'none';
            replyform.style.display = 'none';
            if (previewContainer) {
                previewContainer.innerHTML = '';
                previewContainer.style.display = 'none'; 
            }
        })

    
}

function submitComment(event) {
    event.preventDefault();

    const commentText = document.getElementById('text-input').value;
    const previewContainer = document.getElementById('image-previewSelf');
    const previewImg = document.getElementById('preview-imgSelf');
    const imageInput = document.getElementById('image-uploadSelf');
    const postId = document.getElementById("commentPostID").getAttribute("data-comment-post-id");
    const user = document.getElementById("commentPostID");
    const authorCommentId = user.dataset.authorid;
    console.log("SubmitComment User", user);

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
    formData.append("level", 0);
    if (imageInput && imageInput.files.length > 0) {
        formData.append('image', imageInput.files[0]);
    }


    console.log([...formData]);
    fetch(postForm.action, {
        method: 'POST',
        body: formData
    })
        .then(response => {

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
    console.log("đã vào display Media ");
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
    if (item.type === "image") {    // sửa laij type
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
            emojiButton.onclick = () => addEmojiToInput(event,emoji, targetInputId); // Sử dụng targetInputId
            emojiGrid.appendChild(emojiButton);
        });
    } else {
        console.error("Không tìm thấy danh mục emoji:", category);
    }
}

// Hàm mở hộp chọn emoji
function showEmojiPicker(event, iconId, inputId) {
    event.stopPropagation(); // Ngăn sự kiện click lan ra ngoài
    event.preventDefault();
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
function addEmojiToInput(event,emoji, inputId) {
    event.preventDefault();

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
// Hàm xử lý like/unlike bài viết

function toggleLike(postId) {
    // Tìm phần tử bài đăng
    const postElement = document.getElementById(`post-${postId}`);

    if (!postElement) {
        console.error(`Không tìm thấy bài đăng với id post-${postId}`);
        return;
    }

    // Tìm nút Like
    const likeButton = postElement.querySelector(".like-btn");
    const interactionIcons = postElement.querySelector(".interaction-icons span");

    if (!interactionIcons) {
        console.error("Không tìm thấy phần tử span trong interaction-icons");
        return;
    }

    // Lấy số lượt thích hiện tại
    let currentCount = parseInt(interactionIcons.innerText);

    // Xác định hành động (like hoặc unlike)
    const likeChange = likeButton.classList.contains("liked") ? -1 : 1;

    const likeForm = document.getElementById("likeForm");
    const postIdInput = document.getElementById("likepostIdInput");
    const likeInput = document.getElementById("likepostInput");
    postIdInput.value = postId;
    likeInput.value = likeChange;
    const formData = new FormData(likeForm);
    formData.append("postId", postId);
    formData.append("like", likeChange);
    console.log(formData);
    fetch(likeForm.action, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(err => Promise.reject(err));
            }
            return response.json();
        })
        .then(data => {
            console.log("Response from server:", data);

            // Cập nhật giao diện dựa trên phản hồi từ server
            if (data.postId === postId) {
                if (likeChange === 1) {
                    likeButton.classList.add("liked");
                } else {
                    likeButton.classList.remove("liked");
                }

                // Cập nhật lại số lượt thích
                interactionIcons.innerText = currentCount + likeChange;
            }
        })
        .catch(error => {
            console.error("Error:", error);
        });
}




