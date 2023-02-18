var modals = document.querySelector('.modal')
        var view = document.querySelector('.js-xemthem')
        var btnDelete = document.querySelector('.modal__inner-icon')
        var overlay = document.querySelector('.js_overlay')

        view.addEventListener('click', function(){
            modals.classList.add('open')
        })
        btnDelete.addEventListener('click', function(){
            modals.classList.remove('open')
        })
        overlay.addEventListener('click', function(){
            modals.classList.remove('open')
        })

        const imgs = document.querySelectorAll('.container__product-img1')
        const img = document.querySelector('.container__product-img')
        var currenindex = 0

        imgs.forEach(function(value, index){
            value.addEventListener('click', function(e){
                currenindex  = index
                img.src = imgs[currenindex].src
            });
        })