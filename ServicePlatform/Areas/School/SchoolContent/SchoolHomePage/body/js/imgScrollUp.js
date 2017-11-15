// JavaScript Document

var stopscroll = false;
		var scrollContHeight = 300;
		var scrollContWidth = 296;
		var scrollSpeed = 25;
		var scrollcontainer = document.getElementById("scrollcontainer");
		var scrollcontent = document.getElementById("scrollcontent");
		
		do{
			scrollcontainer.appendChild(scrollcontent.cloneNode(true));
		}while(scrollcontainer.offsetHeight < scrollContHeight);
		
		scrollcontainer.style.width = scrollContWidth + "px";
		scrollcontainer.style.height = scrollContHeight + "px";
		//scrollcontainer.noWrap = true;
		
		scrollcontainer.onmouseover = new Function("stopscroll=true");
		scrollcontainer.onmouseout = new Function("stopscroll=false");
		
		function init()
		{
			scrollcontainer.scrollTop = 0;
			setInterval("scrollUp()",scrollSpeed);
		}	
		
		init();
		
		var currtop = 0;
		function scrollUp()
		{
			if(stopscroll==true) return;
			currtop = scrollcontainer.scrollTop;
			scrollcontainer.scrollTop += 1;
			if(currtop == scrollcontainer.scrollTop){
				scrollcontainer.scrollTop = 0;
				scrollcontainer.scrollTop += 1;
			}
		}