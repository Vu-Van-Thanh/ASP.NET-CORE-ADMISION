$(document).ready(function(){
    $("#btnLogIn").click(function(event){
      event.preventDefault(); // Ngăn chặn hành động mặc định của thẻ a
      // Chuyển hướng đến trang chủ khi nhấn vào nút "Login"
      window.location.href = "../HTML code/home.html";
    });
  });