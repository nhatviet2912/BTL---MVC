var list = JSON.parse(localStorage.getItem('singin')) || [];

const singin = document.getElementById('login')
singin.onsubmit = function(e){
    const tk  = document.getElementById('taikhoan').value;
    const pass = document.getElementById('matkhau').value;

    for(x of list){
        if((tk == x.sdt && pass == x.pass)){
            var user = {
                name: x.name,
                sdt: x.sdt,
                mail: x.mail,
                pass: x.pass,
            };
            alert("Đăng nhập thành công")
            localStorage.setItem('user', JSON.stringify(user));
            window.location.href = "../HTML/Index.html";
            break;
        }
        else if(tk != x.sdt && pass != x.pass){
            alert("Sai tài khoản or mật khẩu")
            break;
        }
    }
    e.preventDefault();
}