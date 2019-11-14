$(function () {
    //aynı ajaxmı kullanıcağımdan dolayı ajaxsetup oluştuyorum
    $.ajaxSetup({
        type: "post",
        url: "/Customer/IlIlce",//controlerımda gidicek olan yerim
        dataType: "json"
    });

    $.extend({
        ilGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tip": "ilGetir" },
                success: function (sonuc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sonuc.ok) {

                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#il").append(optionhtml);

                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#il").append(optionhtml);

                        });

                    }
                }
            });
        },
        ilceGetir: function (ilID) {

            $.ajax({
                //ekstra olarak functionumaza gelen ilID mizi gönderiyoruz
                data: { "ilID": ilID, "tip": "ilceGetir" },
                success: function (sonuc) {
                    //bir önceki kayıtları temizliyorum
                    $("#ilce option").remove();
                    if (sonuc.ok) {
                        //disabled true yapıyorum
                        $("#ilce").prop("disabled", false);
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#ilce").append(optionhtml);
                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#ilce").append(optionhtml);

                        });
                    }
                }
            });
        },
        meslekGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tip": "meslekGetir" },
                success: function (sonuc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sonuc.ok) {

                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#meslek").append(optionhtml);

                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#meslek").append(optionhtml);

                        });

                    }
                }
            });
        },
        medenihaliGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tip": "medenihaliGetir" },
                success: function (sonuc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sonuc.ok) {

                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#medenihali").append(optionhtml);

                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#medenihali").append(optionhtml);

                        });

                    }
                }
            });
        },
        cinsiyetGetir: function () {
            $.ajax({
                //datamızı gönderiyoruz
                data: { "tip": "cinsiyetGetir" },
                success: function (sonuc) {
                    //gelen sonucumuz kontrol ediyoruz ona göre selectimze append işlemi gerçekleştiyoruz
                    if (sonuc.ok) {

                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#cinsiyet").append(optionhtml);

                        });

                    } else {
                        $.each(sonuc.text, function (index, item) {
                            var optionhtml = '<option value="' + item.Value + '">' + item.Text + '</option>';
                            $("#cinsiyet").append(optionhtml);

                        });

                    }
                }
            });
        }
    });
    //ilgetirimizi çalıştıyoruz
    $.cinsiyetGetir();
    $.medenihaliGetir();
    $.meslekGetir();
    $.ilGetir();
    //il selectimizde değişiklik olduğunda çalışacak func
    $("#il").on("change", function () {
        //seçili olan id alıyoruz
        var ilID = $(this).val();
        //seçili olan idmizi ilceGetir'imze gönderiyoruz
        $.ilceGetir(ilID);
    });
});
