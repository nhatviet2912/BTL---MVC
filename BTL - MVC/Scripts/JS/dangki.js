var list = JSON.parse(localStorage.getItem('singin')) || [];


        function dangki(){
            var name = document.getElementById('name').value;
            var sdt = document.getElementById('sdt').value;
            var mail = document.getElementById('mail').value;
            var pass = document.getElementById('pass').value;
            var nhappass = document.getElementById('nhaplaipass').value;
            var number = /^[0-9]+$/;


            if(name == null || name == ""){
                alert("Tên khách hàng không được để trống! Vui lòng nhập tên khách hàng");
                return false;
            }
            else if(sdt == null || sdt == ""){
                alert("Số điện thoại khách hàng không được để trống! Vui lòng nhập số điện thoại");
                return false;
            }
            else if(!sdt.match(number) || sdt.length != 10){
                alert("Số điện thoại phải là kiểu số và có 10 kí tự")
                return false;
            }
            else if(mail == null || mail == ""){
                alert("Email khách hàng không được để trống")
                return false;
            }
            else if(pass == null || pass == ""){
                alert("Password khách hàng không được để trống")
                return false;
            }
            else if(nhappass != pass){
                alert("Password không khớp ! Vui lòng nhập lại")
                return false;
            }

            if(list == null) list = [];
            var customer = {
                name: name,
                sdt: sdt,
                mail: mail,
                pass: pass,
            }
            list.push(customer)
            localStorage.setItem("singin", JSON.stringify(list));
            alert("Đăng kí thành công")
            location.reload();
            

        }