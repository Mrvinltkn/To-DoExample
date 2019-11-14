$(function () {
    //aynı ajaxmı kullanıcağımdan dolayı ajaxsetup oluştuyorum
    $.ajaxSetup({
        type: "post",
        url: "/Customer/Meslek",//controlerımda gidicek olan yerim
        dataType: "json"
    });

    $.extend({
        meslekGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tp": "meslekGetir" },
                success: function (sc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sc.ok) {

                        $.each(sc.text, function (index, item) {
                            var ophtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#meslek").append(ophtml);

                        });

                    } else {
                        $.each(sc.text, function (index, item) {
                            var ophtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#meslek").append(ophtml);

                        });

                    }
                }
            });
        }
    });
    $.meslekGetir();
   
});