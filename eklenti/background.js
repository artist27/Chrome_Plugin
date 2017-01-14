function getSelectionText() {
    var text = "";
    if (window.getSelection) {
        text = window.getSelection().toString();
    } else if (document.selection && document.selection.type != "Control") {
        text = document.selection.createRange().text;
    }
    return text;
}
var metin = $("body").text();
//alert(metin);
$.ajax({
		type: "post",
		url: "http://localhost:64681/ZemberekServis.asmx/EnYuksek5FrekansliKelime",
		data : "{metin: '" + escape(metin) + "'}",
		contentType:  "application/json; charset=utf-8",
		dataType : "json",
		success : function(data)
		{
		    //Burada highlight kodunu yaz
		    var gelenData = data.d;
		    for (i = 0; i < gelenData.length; i++)
		        $("body").highlight(gelenData[i]);
		},
		error : function(){alert(" baslangicta hata olustu");}
	});




$("body").on("mouseup", function()
{
	var cumle = getSelectionText();
	//alert(cumle);
	$.ajax({
		type: "post",
		url: "http://localhost:64681/ZemberekServis.asmx/CumleAyristir",
		data : "{cumle: '" + escape(cumle) + "'}",
		contentType:  "application/json; charset=utf-8",
		dataType : "json",
		success : function(data)
		{
		  			//Burada birsey yazmana gerek yok
			//var gelenData = JSON.parse(data.d);
			//alert(gelenData);
		},
		error : function(){ alert("mouseup ta hata olustu") }
	});
});