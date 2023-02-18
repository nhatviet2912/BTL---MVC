function addcart(item){
    item.soluong = 1;
    // console.log(item.soluong)
    var list
    if(localStorage.getItem('cart') == null){
        list = [item];
    }
    else{
        list = JSON.parse(localStorage.getItem('cart')) || [];
        let ok = true;
        for (var x of list) {
            if(x.id == item.id) {
                x.soluong += 1;
                ok = false;
                break;
            }
        }
        if(ok){
            list.push(item);
        }
    }
    localStorage.setItem('cart', JSON.stringify(list));
    alert('Thêm thành công vào giỏ hàng');
}

var list = JSON.parse(localStorage.getItem('cart')) || [];
function loaddata(){
    var info = "";
    var tien = "";
    var tt = "" ;
    var sotien = 0;
    for(var x of list){
        sotien += x.price * x.soluong; 
        info += `
        <div class="middlecart1-two">
            <div class="middlecart1_img">
                <img src="${x.img}" alt="">
            </div>
            <div class="middlecart1_content">
                ${x.name} 
            </div>
            <div class="middlecart1_price">
                    <i class="fa-solid fa-trash-can" onclick="closeCart(${x.id})"></i>
                <div class="price_before">
                    21.000.000đ
                </div>
                <div class="price_after">
                    ${x.price}
                </div>
                <div class="product_countor">
                    <div class="countor_minus">
                        <i class="fa-solid fa-minus" onclick="giam(${x.id})"></i>
                    </div>
                    <div class="countor_content">
                        ${x.soluong}
                    </div>
                    <div class="countor_plus">
                        <i class="fa-solid fa-plus" onclick="tang(${x.id})"></i>
                    </div>
                </div>
            </div>
        </div>`; 
    };
    document.getElementById("mycart").innerHTML = info;
    document.getElementById("tien").innerHTML = sotien;
    document.getElementById("tamtinhtien").innerHTML = sotien;
   
};
loaddata();

function closeCart(id){
    if(confirm("Bạn muốn xóa sản phẩm không")){
        var index = list.findIndex(x => x.id == id)
        if(index >= 0){
            list.splice(index, 1);
        }
        localStorage.setItem('cart', JSON.stringify(list));
        location.reload()
        loaddata();
    }
}

function giam(id){
    var index = list.findIndex(x => x.id == id);
    if(index >= 0){
        list[index].soluong -= 1;
        if(list[index].soluong < 1){
            list[index].soluong = 1;
        }
    }
    localStorage.setItem('cart', JSON.stringify(list));

    loaddata();
}
function tang(id){
    var index = list.findIndex(x => x.id == id);
    if(index >= 0){
        list[index].soluong += 1;
    }
    localStorage.setItem('cart', JSON.stringify(list));

    loaddata();
}


function dathang(){
    var tinh = document.getElementById('tinh').value;
    var huyen = document.getElementById('huyen').value;
    var xa = document.getElementById('xa').value;
    var sonha = document.getElementById('sonha').value;
    var hoten = document.getElementById('hoten').value;
    var sdt = document.getElementById('sdt').value;
    var stk = document.getElementById('stk').value;

    if(tinh == null || tinh == ""){
        alert("Tỉnh không được để trống")
        return false;
    }
    else if(huyen == null ||  huyen == ""){
        alert("Huyện không được để trống")
        return false;
    }
    else if(xa == null || xa == ""){
        alert("Xã không được để trống")
        return false;
    }
    else if(sonha == null || sonha == ""){
        alert("Số nhà không được để trống")
        return false
    }
    else if(hoten == null || hoten == ""){
        alert("Họ tên không được để trống")
        return false
    }
    else if(sdt == "" || isNaN(sdt)){
        alert("Số điện thoại  không được để trống và phải là số")
        return false
    }
    else if(stk == "" || isNaN(stk)){
        alert("Số tài khoản không được để trống")
        return false;
    }

    window.print();
}

