		function aaa(){
			$(".feather-refresh-cw").click();
			
			//get data
			const number = $(".deal")[0]['outerText'];
			console.log(number);
		
			const title1 = $(".astock-name").text();
			
			//百分比
			const pers = $(".chg-rate").html();
			
			const title2 = number+title1+pers;
			
			$("title").text(title2);

			//0.5秒刷新
			setTimeout(function(){
				aaa();
			},500);
		}

		aaa();
