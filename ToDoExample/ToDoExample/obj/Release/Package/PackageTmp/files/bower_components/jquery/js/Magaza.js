$(function () {
    //aynı ajaxmı kullanıcağımdan dolayı ajaxsetup oluştuyorum
    $.ajaxSetup({
        type: "post",
        url: "/ParaPuan/Magaza",//controlerımda gidicek olan yerim
        dataType: "json"
    });

    $.extend({
        MagazaGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tip": "MagazaGetir" },
                success: function (sonuc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sonuc.ok) {

                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#magaza").append(optionhtml);

                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#magaza").append(optionhtml);

                        });

                    }
                }
            });
        }
    });
    //ilgetirimizi çalıştıyoruz
    $.MagazaGetir();
});
