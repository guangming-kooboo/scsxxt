  window.onload = function () {
       var container = document.getElementById('containter');
       var list = document.getElementById('list');
       var buttons = document.getElementById('buttons').getElementsByTagName('span');
       var prev = document.getElementById('prev');
       var next = document.getElementById('next');
       var index = 1;

       function showButton() {
            for (var i = 0; i < buttons.length; i++) {
                if (buttons[i].className == 'on') {
                     buttons[i].className = '';
                }
            }
            buttons[index - 1].className = 'on';

       }
       
       function animate(offset){
           var newleft =parseInt(list.style.left) + offset;
      
           list.style.left = newleft + 'px';
           if (newleft > -1600) {list.style.left = -8400 + 'px';}
           if (newleft < -8400) {list.style.left = -1600 + 'px';}
           
       

       }



          
       next.onclick = function () {
               if (index ==4 ) {
                   index = 1;
               }
               else {
                 index += 1;
               }
               showButton();
               animate(-1600);

       }

       prev.onclick = function () {
               if (index ==1 ) {
                   index = 4;
               }
               else {
                 index -= 1;
               }
               
               showButton();
               animate(+1600);
       }

       for (var i = 0; i < buttons.length; i++) {
         buttons[i].onclick = function () {
              if (this.className == 'on') {return;}
              var myIndex = parseInt(this.getAttribute('index'));
              var offset = -1600 * (myIndex - index);
              index = myIndex;
              animate(offset);
              showButton();
         }
       }
  }